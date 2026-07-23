using MyFinalProject.Application.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class AddEmployerRequestDto
    {
        [Required(ErrorMessage = "First name is required.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Firstname should be at least 3 characters long.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Last Name should be at least 3 characters long.")]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Company Name is required.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Company Name should be at least 3 characters long.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Description is required.", AllowEmptyStrings = false)]
        [MinLength(10, ErrorMessage = "Company Description should be at least 10 characters long.")]
        public string CompanyDescription { get; set; }

        [Required(ErrorMessage = "Company Location is required.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Company Location should be at least 3 characters long.")]
        public string CompanyLocation { get; set; }

    }
}
