using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام الزامی است")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "شماره تلفن الزامی است")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تایید رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور و تایید آن مطابقت ندارند")]
        public string ConfirmPassword { get; set; }

        public bool IsEmployer { get; set; } = false;

        public string? CompanyName { get; set; }

        public string? CompanyLocation { get; set; }

        public string? Province { get; set; }

        public string? City { get; set; }
    }
}