using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Transmit
{
    public class DeleteUserTransmitModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}