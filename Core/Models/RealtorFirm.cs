namespace Core.Models
{
    public class RealtorFirm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string WebsiteURL { get; set; } = string.Empty;
        public string RealtorLogoPath { get; set; } = string.Empty;

        public ICollection<User> Employees { get; set; } = new List<User>();
        public ICollection<RealEstateObject> FirmListings { get; set; } = new List<RealEstateObject>();
    }
}