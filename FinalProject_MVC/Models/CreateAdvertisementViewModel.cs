using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class CreateAdvertisementViewModel
    {
        [Required(ErrorMessage = "عنوان آگهی الزامی است")]
        public string Title { get; set; }

        [Required(ErrorMessage = "توضیحات الزامی است")]
        public string Description { get; set; }

        [Required(ErrorMessage = "حقوق الزامی است")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "نام شرکت الزامی است")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "استان الزامی است")]
        public string Province { get; set; }

        [Required(ErrorMessage = "شهر الزامی است")]
        public string City { get; set; }

        [Required(ErrorMessage = "Company ID الزامی است")]
        public Guid CompanyId { get; set; }
    }
}