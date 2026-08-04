using MyFinalProject.Application.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class CreateAdvertisementDto
    {
        [Required(ErrorMessage = "Title is required !!", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Title should be at least 6 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required !!", AllowEmptyStrings = false)]
        [MinLength(15, ErrorMessage = "Description should be at least 15 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Salary is required !!", AllowEmptyStrings = false)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "CompanyName is required !!", AllowEmptyStrings = false)]
        [MinLength(8, ErrorMessage = "CompanyName should be at least 8 characters long.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Province is required !!", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Province should be at least 3 characters long.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "City is required !!", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "City should be at least 3 characters long.")]
        public string City { get; set; }

        public Guid CategoryId { get; private set; }

        public Guid CompanyId { get; private set; }
    }
}
