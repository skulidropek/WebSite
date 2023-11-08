using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSite.Models;
using WebSite.Pages.ResourceAnalyze;

namespace WebSite.Services
{
    public class DataBaseContextService : IdentityDbContext<IdentityUser>
    {
        public DbSet<ResourceModel> Resources { get; set; }
        public DbSet<ResourceAnalyzeModel> ResourcesAnalyze { get; set; }

        public DataBaseContextService(DbContextOptions<DataBaseContextService> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
