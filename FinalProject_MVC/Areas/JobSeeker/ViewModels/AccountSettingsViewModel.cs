using System.ComponentModel.DataAnnotations;

public class AccountSettingsViewModel
{
    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = "نام و نام خانوادگی الزامی است")]
    public string FullName { get; set; }

    [Display(Name = "ایمیل")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "شماره تماس")]
    [Phone(ErrorMessage = "شماره تماس معتبر نیست")]
    public string PhoneNumber { get; set; }
}

public class ChangePasswordViewModel
{
    [Display(Name = "رمز عبور فعلی")]
    [Required(ErrorMessage = "رمز عبور فعلی الزامی است")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "رمز عبور جدید")]
    [Required(ErrorMessage = "رمز عبور جدید الزامی است")]
    [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "تکرار رمز عبور جدید")]
    [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
    [Compare("NewPassword", ErrorMessage = "رمز عبور و تکرار آن مطابقت ندارند")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}