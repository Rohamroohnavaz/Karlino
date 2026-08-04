namespace FinalProject_MVC.Models
{
    public class AdvertisementViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public string CompanyName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
