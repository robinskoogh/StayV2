using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Core.Models.Enums
{
    public enum PropertyType
    {
        [Display(Name = "Lägenhet")]
        Apartment,

        [Display(Name = "Hus")]
        House,

        [Display(Name = "Radhus")]
        RowHouse,

        [Display(Name = "Tomt")]
        Lot,

        [Display(Name = "Sommarstuga")]
        HolidayHome,

        [Display(Name = "Gård")]
        Farm,

        [Display(Name = "Annat")]
        Other
    }
}