using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Transmit
{
    public class AdminChangeRoleTransmitModel
    {
        [Required]
        public string UserName{get; set;}
        [Required]
        public string Role{get;set;}
    }
}