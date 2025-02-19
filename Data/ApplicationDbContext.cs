using Microsoft.EntityFrameworkCore;
using ObjectDetection.Models;

namespace ObjectDetection.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Detection> Detections { get; set; }
    }
}
