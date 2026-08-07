using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class CreateAdvertisementViewModel
    {
        [Required(ErrorMessage = "Title is required !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required !")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Salary is required !")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "CompanyName is required !")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Province is required !")]
        public string Province { get; set; }

        [Required(ErrorMessage = "City is required !")]
        public string City { get; set; }

        [Required(ErrorMessage = "CompanyId is required !")]
        public string CompanyId { get; set; }
    }
}