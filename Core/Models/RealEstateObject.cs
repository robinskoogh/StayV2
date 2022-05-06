using Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class RealEstateObject
    {
        public int Id { get; set; }

        [Display(Name = "Adress")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Postnummer")]
        public int ZipCode { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; } = string.Empty;

        [Display(Name = "Pris")]
        public int Price { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Bostadstyp")]
        public PropertyType PropType { get; set; }

        [Display(Name = "Upplåtelseform")]
        public ContractType ContractType { get; set; }

        [Display(Name = "Antal rum")]
        public int NrOfRooms { get; set; }

        [Display(Name = "Boarea")]
        public double LivingArea { get; set; }

        [Display(Name = "Biarea")]
        public double GrossArea { get; set; }

        [Display(Name = "Tomtarea")]
        public double PlotSize { get; set; }

        [Display(Name = "Byggår")]
        public int? ConstructionYear { get; set; }

        [Display(Name = "Balkong?")]
        public bool Balcony { get; set; } = false;

        [DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH/mm/åååå}")]
        [Display(Name = "Visningsdatum")]
        public DateTime DateForViewing { get; set; }

        [Display(Name = "Intresseanmälningar")]
        public List<User> InterestedUsers { get; set; } = new List<User>();

        [Display(Name = "Bilder")]
        public List<Image> Images { get; set; } = new List<Image>();

        [DataType(DataType.Date)]
        [Display(Name = "Uppladdningsdatum")]
        public DateTime? DateUploaded { get; set; }

        [Display(Name = "Longitud")]
        public double Longitude { get; set; }

        [Display(Name = "Latitud")]
        public double Latitude { get; set; }

        public string Sublocality { get; set; } = string.Empty;
        public string PostalTown { get; set; } = string.Empty;

        public int NrOfViews { get; set; }

        public List<User> UsersFavorited { get; set; } = new List<User>();
        public User? Realtor { get; set; }

        public RealEstateObject()
        {
        }
    }
}