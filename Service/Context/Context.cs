using System.Net.Mime;
using Engineering_Project.Models.Entity;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Engineering_Project.Service.Context
{
    public class Context : IdentityDbContext<ApplicationUser, ApplicationRole, string>
//    public class Context : DbContext
    {
        public DbSet<Training> Trainings { get; set; }
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Training>().HasAlternateKey(p => p.Id);
            builder.Entity<Training>().HasAlternateKey(p => p.Id);
//            builder.Entity<Training>().HasKey(t => new
//            {
//                t.Id, t.UserId
//            });
            
            base.OnModelCreating(builder);
        }
    }
}