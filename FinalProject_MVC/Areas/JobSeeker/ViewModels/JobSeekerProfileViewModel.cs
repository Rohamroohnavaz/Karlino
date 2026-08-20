using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Areas.JobSeeker.ViewModels
{
    public class JobSeekerProfileViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً نام را وارد کنید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً نام خانوادگی را وارد کنید")]
        public string LastName { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        //[Display(Name = "جنسیت")]
        //public string Gender { get; set; }

        [Display(Name = "شماره موبایل")]
        [Phone(ErrorMessage = "شماره موبایل معتبر نیست")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string Email { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        //[Display(Name = "عنوان شغلی")]
        //public string JobTitle { get; set; }

        //[Display(Name = "درباره من")]
        //public string AboutMe { get; set; }

        //[Display(Name = "لینک لینکدین")]
        //public string LinkedInUrl { get; set; }

        //[Display(Name = "لینک گیت‌هاب")]
        //public string GitHubUrl { get; set; }

        //public string ProfileImageUrl { get; set; }
    }
}