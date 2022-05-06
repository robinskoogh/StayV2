using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Namn")]
        public string RoleName { get; set; }
    }
}