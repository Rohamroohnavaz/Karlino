using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Areas.JobSeeker.ViewModels
{
    public class JobsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
       //public string JobType { get; set; }
        public string Salary { get; set; }
        public DateTime PostedDate { get; set; }
        public string Description { get; set; }
        //public string[] Requirements { get; set; }
        public bool HasApplied { get; set; }
    }

    public class JobSearchViewModel
    {
        [Display(Name = "عنوان شغل")]
        public string JobTitle { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "نوع همکاری")]
        public string JobType { get; set; }

        public List<JobsViewModel> Jobs { get; set; }
    }
}