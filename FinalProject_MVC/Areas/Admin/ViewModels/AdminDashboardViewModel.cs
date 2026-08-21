namespace FinalProject_MVC.Areas.Admin.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalEmployers { get; set; }
        public int TotalJobs { get; set; }
        public int PendingJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int RejectedJobs { get; set; }
        public int TotalVisits { get; set; }
    }
}
