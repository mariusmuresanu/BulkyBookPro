using BulkyBookWebPro.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWebPro.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
