using MyFinalProject.Application.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class AddUserRequestDto
    {
        [Required(ErrorMessage = "First name is required.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Firstname should be at least 3 characters long.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "First Name is required.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Last Name should be at least 3 characters long.")]
        public string LastName { get; set; }

        
        public string PhoneNumber { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }

        public RegisterUserCommand ToCommand()
        {
            return new RegisterUserCommand(FirstName, LastName, PhoneNumber, Email, Password);
        }
    }
}
