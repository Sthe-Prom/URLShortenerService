using System.Threading.Tasks;
using URLShortenerService.Models.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace URLShortenerService.Models.EF
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }


        //Propeties - [T A B L E S]
        public DbSet<MetroURL> MetroURL { get; set; }

        //Set max to accommodate long Url
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MetroURL>()
                .Property(e => e.LongUrl)
                .HasMaxLength(2048);
        }

    }
}