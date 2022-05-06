using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Core.Models.Enums
{
    public enum ContractType
    {
        [Display(Name = "Hyreskontrakt")]
        Rental,

        [Display(Name = "Köpeskontrakt")]
        Purchase
    }
}