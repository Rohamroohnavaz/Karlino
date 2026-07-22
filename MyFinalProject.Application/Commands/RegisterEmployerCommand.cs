using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Commands
{
    public record RegisterEmployerCommand
    (
        string Firstname,
        string Lastname,
        string Phonenumber,
        string Email,
        string Username,
        string Password
    );
}
