using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Requests
{
    public class SendEmailRequest
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string To { get; set; }

        [Required(ErrorMessage = "Subject Is Required")]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Body Is Required")]
        public string Body { get; set; }

        public bool? isHtml { get; set; }
    }
}
