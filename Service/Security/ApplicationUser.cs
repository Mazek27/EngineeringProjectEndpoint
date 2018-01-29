using Microsoft.AspNetCore.Identity;

namespace Engineering_Project.Service.Security
{
    public class ApplicationUser : IdentityUser
    {
        public string Locale { get; set; }
    }
}