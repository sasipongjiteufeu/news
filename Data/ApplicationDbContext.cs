using Microsoft.EntityFrameworkCore;
using news.Models;
namespace news.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<infoNews> infoNews { get; set; }
    }
}
