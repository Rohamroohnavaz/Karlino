using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Notifier
{
    public interface INotifier
    {
        NotificationType GetNotificationType();
        void Send(string message);
    }
}
