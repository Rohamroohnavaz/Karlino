using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Commands.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ResumesDto
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name = "نام کارجو")]
        public string JobSeekerName { get; set; }

        [Display(Name = "نام خانوادگی کارجو")]
        public string JobSeekerLastName { get; set; }

        [Display(Name = "عنوان شغلی")]
        [Required(ErrorMessage = "عنوان شغلی الزامی است")]
        public string JobTitle { get; set; }

        [Display(Name = "درباره من")]
        public string AboutMe { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        [Phone(ErrorMessage = "شماره تماس معتبر نیست")]
        public string PhoneNumber { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "استان")]
        public string Province { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "لینکدین")]
        [Url(ErrorMessage = "آدرس لینک معتبر نیست")]
        public string LinkedInUrl { get; set; }

        [Display(Name = "گیت‌هاب")]
        [Url(ErrorMessage = "آدرس لینک معتبر نیست")]
        public string GitHubUrl { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public string EducationDegree { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string EducationField { get; set; }

        [Display(Name = "دانشگاه")]
        public string University { get; set; }

        [Display(Name = "سال شروع تحصیل")]
        public int? EducationStartYear { get; set; }

        [Display(Name = "سال پایان تحصیل")]
        public int? EducationEndYear { get; set; }

        [Display(Name = "عنوان شغلی (سابقه کار)")]
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

        [Display(Name = "مسیر فایل رزومه")]
        public string ResumeFilePath { get; set; }
    }
}

