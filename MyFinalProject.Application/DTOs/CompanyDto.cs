using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class CompanyDto
    {
        [Required(ErrorMessage = "CompanyId is required !!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CompanyName is required !!",AllowEmptyStrings = false)]
        [MinLength(8 ,ErrorMessage = "CompanyName should be at least 8 characters long.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "CompanyLocation is required !!", AllowEmptyStrings = false)]
        [MinLength(8, ErrorMessage = "CompanyName should be at least 8 characters long.")]
        public string CompanyLocation { get; set; }


        [Required(ErrorMessage = "Province is required !!", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Province should be at least 3 characters long.")]
        public string Province { get; set; }


        [Required(ErrorMessage = "City is required !!", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "City should be at least 3 characters long.")]
        public string City { get; set; }
    }
}
