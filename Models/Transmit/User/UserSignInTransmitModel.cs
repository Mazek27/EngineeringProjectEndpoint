using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Transmit
{
    public class UserSignInTransmitModel
    {
        [Required] 
        public string UserName { get; set; } 
        [Required] 
        public string Password { get; set; } 
    }
}
