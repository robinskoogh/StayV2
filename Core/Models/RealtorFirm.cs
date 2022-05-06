namespace Core.Models
{
    public class RealtorFirm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? WebsiteURL { get; set; }
        public string? RealtorLogoPath { get; set; }

        public ICollection<User> Employees { get; set; } = new List<User>();
        public ICollection<RealEstateObject> FirmListings { get; set; } = new List<RealEstateObject>();
    }
}