using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Transmit
{
    public class AddRoleTransmitModel
    {
        [Required]
        public string Id { get; set; } 
        [Required]
        public string RoleName { get; set; } 
    }
}