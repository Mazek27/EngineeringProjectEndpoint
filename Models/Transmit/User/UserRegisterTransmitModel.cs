using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Transmit
{
    public class UserRegisterTransmitModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        public string ApplicationRoleName { get; set; }
        public string Locale { get; set; }
    }
}