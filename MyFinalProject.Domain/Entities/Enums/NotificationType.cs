using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Enums
{
    public enum NotificationType
    {
        [EnumMember(Value = "Email")]
        Email = 1,
        [EnumMember(Value = "Sms")]
        Sms = 2
    }
}
