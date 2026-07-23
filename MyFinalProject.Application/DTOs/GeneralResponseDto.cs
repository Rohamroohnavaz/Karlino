using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class GeneralResponseDto
    {
        public GeneralResponseDto(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; set; }
        public string Code { get; set; }
    }
}
