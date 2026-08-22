namespace FinalProject_MVC.Areas.JobSeeker.ViewModels
{
    public class MyApplicationsViewModel
    {
        public Guid Id { get; set; }
        public Guid? AdvertisementId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public DateTime AppliedDate { get; set; }
        public string Status { get; set; }
        public string StatusBadgeClass { get; set; }
    }
}
