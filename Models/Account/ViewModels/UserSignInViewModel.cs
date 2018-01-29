using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Account.ViewModels
{
    public class UserSignInViewModel
    {
        [Required] 
        public string UserName { get; set; } 
        [Required] 
        public string Password { get; set; } 
    }
}