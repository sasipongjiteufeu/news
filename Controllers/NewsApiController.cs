using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using news.Data;
using news.Models;

namespace news.Controllers
{
    // try something i lean from read doc
    [Route("api/[controller]")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
       private readonly ApplicationDbContext _context;
        
        public NewsApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> test()
        {
            var log = await _context.infoNews.ToListAsync();
            return Ok(log);
        }

        [HttpGet]
        [Route ("test_form")]
        public async Task<ActionResult<infoNews>> GetAllNews()
        {
            var All = _context.infoNews.ToList();
            
            return Ok(All);
        }

        [HttpPost]
        [Route("Add News")]
        public async Task<ActionResult<List<infoNews>>> PostNewstest(infoNews detli) { 

            _context.infoNews.Add(detli);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(test), new {id = detli.Id}, detli);
        }
    }
}
