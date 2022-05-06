namespace Core.Models
{
    public class RealtorRequest
    {
        public string? Id { get; set; }
        public List<User> UserRequests { get; set; } = new List<User>();
    }
}