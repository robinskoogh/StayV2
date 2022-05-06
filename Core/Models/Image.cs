using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Image
    {
        [Key, Column(Order = 0)]
        public RealEstateObject RealEstateObject { get; set; }

        [Key, Column(Order = 1)]
        public string FileName { get; set; }

        public Image()
        {
        }
    }
}