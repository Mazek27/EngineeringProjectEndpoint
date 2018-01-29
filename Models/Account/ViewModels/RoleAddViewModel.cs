using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Account.ViewModels
{
    public class RoleAddViewModel
    {
        [Required]
        public string Id { get; set; } 
        [Required]
        public string RoleName { get; set; } 
    }
}