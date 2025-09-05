using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using news.Data;
using news.Models;
using news.ViewModels;
using System.Diagnostics;

namespace news.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var allNews = _context.infoNews
                                  .OrderByDescending(n => n.PublishedDate)
                                  .ToList();

            return View(allNews);
        }
        public IActionResult Deletelist()
        {
            // Fetch all news, ordered from newest to oldest, just like the index page.
            var allNews = _context.infoNews
                                  .OrderByDescending(n => n.PublishedDate)
                                  .ToList();
            return View(allNews);
        }

        // POST: /Home/Delete/5
        // This action handles the actual deletion.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // 1. Find the news item to delete, including its related images.
            var newsToDelete = await _context.infoNews
                                             .Include(n => n.Images)
                                             .FirstOrDefaultAsync(n => n.Id == id);

            if (newsToDelete == null)
            {
                return NotFound(); // If news with that ID doesn't exist.
            }

            // 2. Delete the physical image files from the server.
            // Delete the headline image
            if (!string.IsNullOrEmpty(newsToDelete.ImgTitle))
            {
                var headlinePath = Path.Combine(_webHostEnvironment.WebRootPath, newsToDelete.ImgTitle.TrimStart('/'));
                if (System.IO.File.Exists(headlinePath))
                {
                    System.IO.File.Delete(headlinePath);
                }
            }

            // Delete the "other" images
            foreach (var image in newsToDelete.Images)
            {
                if (!string.IsNullOrEmpty(image.ImagePath))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, image.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }

            // 3. Remove the news record from the database.
            // EF Core will automatically handle deleting the related NewsImage records
            // due to the one-to-many relationship (cascade delete).
            _context.infoNews.Remove(newsToDelete);

            // 4. Save the changes to the database.
            await _context.SaveChangesAsync();

            // 5. Redirect the user back to the list of news to delete.
            return RedirectToAction(nameof(Deletelist));
        }
        public IActionResult detailNews(int id)
        {
            var newsDetail = _context.infoNews.Include(n => n.Images).FirstOrDefault(n => n.Id == id);
            if (newsDetail == null)
            {
                return NotFound();
            }
            return View(newsDetail);
        }

        [HttpGet]
        public IActionResult add_news()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> add_news(AddNewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Create the main news object WITHOUT images first.
                var newsItem = new infoNews
                {
                    Title = model.Head_news,
                    Content = model.comments,
                    PublishedDate = DateTime.Now,
                    Images = new List<NewsImage>() // Still initialize the list
                };

                // 2. Handle the headline picture and assign its path to the newsItem.
                if (model.NewsHeadlinePics != null)
                {
                    newsItem.ImgTitle = await UploadFile(model.NewsHeadlinePics);
                }

                // 3. IMPORTANT: Add the newsItem to the context BEFORE handling child images.
                // This makes EF Core start tracking the parent object.
                _context.infoNews.Add(newsItem);

                // 4. Now, handle the collection of other pictures.
                if (model.otherPics != null && model.otherPics.Any())
                {
                    foreach (var file in model.otherPics)
                    {
                        // We can add a check to make sure the file isn't empty
                        if (file != null && file.Length > 0)
                        {
                            var imagePath = await UploadFile(file);
                            // Create the child image and add it to the parent's collection.
                            // Because newsItem is already being tracked, EF will now correctly
                            // see this new child object and associate it.
                            newsItem.Images.Add(new NewsImage { ImagePath = imagePath });
                        }
                    }
                }

                // 5. Save everything at once. EF will save the parent and all children.
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "news");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return "/images/news/" + uniqueFileName;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
