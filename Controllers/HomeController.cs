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
            var allNews = _context.infoNews.ToList();
            return View(allNews);
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
                string headlineImagePath = null;
                if (model.NewsHeadlinePics != null)
                {
                    headlineImagePath = await UploadFile(model.NewsHeadlinePics);
                }

                var newsItem = new infoNews
                {
                    Title = model.Head_news,
                    Content = model.comments,
                    ImgTitle = headlineImagePath,
                    PublishedDate = DateTime.Now,
                    Images = new List<NewsImage>()
                };

                if (model.otherPics != null && model.otherPics.Any())
                {
                    foreach (var file in model.otherPics)
                    {
                        var imagePath = await UploadFile(file);
                        newsItem.Images.Add(new NewsImage { ImagePath = imagePath });
                    }
                }

                _context.infoNews.Add(newsItem);
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
