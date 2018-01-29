using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Account.ViewModels
{
    public class UserChangePasswordViewModel
    {
        [Required]
        public string OldPassword{get; set;}

        [Required]
        public string NewPassword{get; set;}
    }
}