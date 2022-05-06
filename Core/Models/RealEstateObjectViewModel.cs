using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    // TODO Should not be in here, but won't build otherwise
    public class RealEstateObjectViewModel
    {
        public int Id { get; set; }
        public string? SearchString { get; set; } = string.Empty;
        public string? MinRooms { get; set; } = string.Empty;
        public string? MaxRooms { get; set; } = string.Empty;
        public string? MinLivingArea { get; set; } = string.Empty;
        public string? MaxLivingArea { get; set; } = string.Empty;
        public string? PlotSize { get; set; } = string.Empty;
        public string? MinPrice { get; set; } = string.Empty;
        public string? MaxPrice { get; set; } = string.Empty;
        public string? NewConstruction { get; set; } = string.Empty;
        public string? Balcony { get; set; } = string.Empty;
        public string? DescriptionString { get; set; } = string.Empty;
        public bool HouseCheckBox { get; set; } = false;
        public bool RowHouseCheckBox { get; set; } = false;
        public bool ApartmentCheckbox { get; set; } = false;
        public bool HolidayHomeCheckBox { get; set; } = false;
        public bool LotCheckBox { get; set; } = false;
        public bool FarmCheckBox { get; set; } = false;
        public bool OtherCheckBox { get; set; } = false;
        public string SortOrder { get; set; } = "newFirst";
        public User User { get; set; }

        [DataType(DataType.Date)]
        public DateTime SearchTime { get; set; } = DateTime.Now;

        public List<RealEstateObject> RealEstateObjectsList { get; set; } = new();
    }
}