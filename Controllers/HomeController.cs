using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using news.Data;
using news.Models;

namespace news.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult detailNews()
        {
            return View();
        }
        
        public IActionResult add_news()
        {
            return View();
        }

    
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
