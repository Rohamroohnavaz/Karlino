using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Results
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string MainToken { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        //string AccessToken,
        //double ExpiresIn,
        //bool IsSuccess,
        //LoginResult? MainToken,
        //string Username,
        //string Role
    }
}
