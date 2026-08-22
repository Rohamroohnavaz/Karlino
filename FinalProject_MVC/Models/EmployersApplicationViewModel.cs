namespace FinalProject_MVC.Models
{
        public class EmployerApplicationsViewModel
        {
            public Guid RequestId { get; set; }
            public string JobSeekerName { get; set; }
            public string JobSeekerLastName { get; set; }
            public string JobSeekerFullName => $"{JobSeekerName} {JobSeekerLastName}";
            public string City { get; set; }
            public string Title { get; set; }
            public string Skills { get; set; }
            public string ResumeFilePath { get; set; }
            public DateTime AppliedDate { get; set; }
            public string Status { get; set; }
            public string StatusBadgeClass { get; set; }
            public int StatusValue { get; set; }
            public Guid? AdvertisementId { get; set; }
            public string AdvertisementTitle { get; set; }
        }

        public class UpdateApplicationStatusViewModel
        {
            public Guid RequestId { get; set; }
            public int NewStatus { get; set; }
        }
}

