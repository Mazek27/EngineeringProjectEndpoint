using System.ComponentModel.DataAnnotations;

namespace Engineering_Project.Models.Account.ViewModels
{
    public class AdminChangeRoleViewModel
    {
        [Required]
        public string UserName{get; set;}
        [Required]
        public string role{get;set;}
    }
}