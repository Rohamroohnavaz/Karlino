using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Areas.JobSeeker.ViewModels
{
    public class ResumeViewModel
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name = "عنوان شغلی")]
        public string JobTitle { get; set; }

        [Display(Name = "درباره من")]
        public string AboutMe { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "لینک لینکدین")]
        public string LinkedInUrl { get; set; }

        [Display(Name = "لینک گیت‌هاب")]
        public string GitHubUrl { get; set; }



        [Display(Name = "مقطع تحصیلی")]
        public string EducationDegree { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string EducationField { get; set; }

        [Display(Name = "دانشگاه")]
        public string University { get; set; }

        [Display(Name = "سال شروع")]
        public int? EducationStartYear { get; set; }

        [Display(Name = "سال پایان")]
        public int? EducationEndYear { get; set; }



        [Display(Name = "عنوان شغلی قبلی")]
        public string WorkTitle { get; set; }

        [Display(Name = "نام شرکت")]
        public string CompanyName { get; set; }

        [Display(Name = "توضیحات شغلی")]
        public string WorkDescription { get; set; }

        [Display(Name = "سال شروع کار")]
        public int? WorkStartYear { get; set; }

        [Display(Name = "سال پایان کار")]
        public int? WorkEndYear { get; set; }

        [Display(Name = "مهارت‌ها")]
        public string Skills { get; set; }

        [Display(Name = "زبان‌ها")]
        public string Languages { get; set; }

        public string ResumeFilePath { get; set; }
    }
}