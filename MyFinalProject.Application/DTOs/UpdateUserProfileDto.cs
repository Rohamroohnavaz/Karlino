using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class UpdateUserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
    }
}
