
using System;
using Microsoft.AspNetCore.Identity;

namespace Engineering_Project.Service.Security
{
    public class ApplicationUser : IdentityUser<Guid> 
    {
        public string Locale { get; set; }
    }
}