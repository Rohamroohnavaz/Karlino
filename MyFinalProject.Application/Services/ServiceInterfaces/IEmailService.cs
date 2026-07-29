using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body,
            bool isHtml, CancellationToken cancellationToken);
    }
}
