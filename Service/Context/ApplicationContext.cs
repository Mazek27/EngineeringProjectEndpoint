using System;
using System.Net.Mime;
using System.Spatial;
using Engineering_Project.Models.Entity;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Engineering_Project.Service.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
//    public class Context : DbContext
    {
        public DbSet<Training> Trainings { get; set; }
//        public DbSet<Localization> Localizations { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
//            builder.Entity<IdentityUser>().ToTable("MyUsers").Property(p => p.Id).HasColumnName("UserId");
//            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").Property(p => p.Id).HasColumnName("UserId");
//            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetRoles");
//            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
//            builder.Entity<IdentityUserClaim<string>>().Property("Id").UseNpgsqlSerialColumn();
//            builder.Entity<IdentityRole>().ToTable("AspNetRoles");
            
//            builder.Entity<Training>().HasAlternateKey(p => p.Id);
//            builder.Entity<Training>().HasAlternateKey(p => p.Id);
////            builder.Entity<Training>().HasKey(t => new
////            {
////                t.Id, t.UserId
////            });
            
            
        }
    }
}