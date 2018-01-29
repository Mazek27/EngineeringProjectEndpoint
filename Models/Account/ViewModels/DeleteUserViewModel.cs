using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Account.ViewModels
{
    public class DeleteUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}