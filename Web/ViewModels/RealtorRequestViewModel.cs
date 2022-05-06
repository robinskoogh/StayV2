using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class RealtorRequestViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}