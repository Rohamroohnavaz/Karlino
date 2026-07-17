using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Notifier.MessagesType
{
    public class SmsNotif : INotifier
    {
        public NotificationType GetNotificationType() => NotificationType.Sms;
        
        public void Send(string message)
        {
            Console.WriteLine($"(Sms Notifier) (Message : {message}) SENT ");
        }
    }
}
