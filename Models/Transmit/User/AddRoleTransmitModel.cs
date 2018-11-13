using System;
using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Transmit
{
    public class AddRoleTransmitModel
    {
        public Guid Id { get; set; } 
        [Required]
        public string RoleName { get; set; } 
    }
}