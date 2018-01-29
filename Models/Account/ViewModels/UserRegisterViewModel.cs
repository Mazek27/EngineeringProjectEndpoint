using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Account.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        [Required]
        public string ApplicationRoleName { get; set; } 
        [Required]
        public string Locale { get; set; }
    }
}