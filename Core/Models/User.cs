using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Efternamn")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Adress")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Postnummer")]
        public int ZipCode { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; } = string.Empty;

        [PersonalData]
        [DataType(DataType.Date)]
        [Display(Name = "Födelsedatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/åååå}")]
        public DateTime DateOfBirth { get; set; }

        public RealtorFirm? RealtorFirm { get; set; }
        public ICollection<RealEstateObject> Favorites { get; set; } = new List<RealEstateObject>();
        public List<RealEstateObject> InterestingListings { get; set; } = new List<RealEstateObject>();
        public List<RealtorRequest> RealtorRequests { get; set; } = new List<RealtorRequest>();
        public List<RealEstateObjectViewModel> SearchHistory { get; set; } = new List<RealEstateObjectViewModel>();
        public List<RealEstateObject> UploadedRealEstateObjects { get; set; } = new List<RealEstateObject>();

        [NotMapped]
        public RealEstateObjectViewModel SearchPreference { get; set; } = new RealEstateObjectViewModel();
    }
}