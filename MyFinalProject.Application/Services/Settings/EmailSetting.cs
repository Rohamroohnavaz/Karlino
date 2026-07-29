using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.Settings
{
    public class EmailSetting
    {
        public string SectionName { get; set; } = "EmailSettings";

        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }

        public bool UseSsl { get; set; }

        public bool DefaultHtml { get; set; }
    }
}
