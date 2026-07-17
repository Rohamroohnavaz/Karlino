using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Notifier.MessagesType
{
    public class EmailNotif : INotifier
    {
        public NotificationType GetNotificationType() => NotificationType.Email;

        public void Send(string message)
        {
            Console.WriteLine($"(Email Notifier) (Message : {message}) SENT ");
        }
    }
}
