using System.Net.Mime;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Engineering_Project.Service.Context
{
    public class Context : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}