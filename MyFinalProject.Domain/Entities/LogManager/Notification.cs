using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.LogManager
{
    public class Notification : BaseEntity
    {
        private Notification()
        {
            
        }

        public Notification(string title, string message)
        {
            Title = title;
            Message = message;
            Validation();
        }

        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationStatus Status { get; set; }
        public User? User { get; set; }
        public Guid? UserId { get; private set; }
        public Company? Company { get; set; }
        public Guid? CompanyId { get; private set; }

        public void ChangeNotificationTitle(string newTitle)
        {
            if (string.IsNullOrEmpty(newTitle))
                throw new InvalidTextException("NewTitle is invalid !");

            Title = newTitle;
        }

        public void ChangeNotificationMessage(string newMessage)
        {
            if (string.IsNullOrEmpty(newMessage))
                throw new InvalidTextException("NewMessage is invalid !");

            Message = newMessage;
        }

        public override void Validation()
        {
            if (UserId == Guid.Empty)
                throw new Exception("Invalid UserId !!");

            if (CompanyId == Guid.Empty)
                throw new InvalidGuidException("Invalid ComapnyId !!");

            if (string.IsNullOrWhiteSpace(Title))
                throw new InvalidTextException("Title is required !!");

            if (string.IsNullOrWhiteSpace(Message))
                throw new InvalidTextException("Message is required !!");
        }
    }
}
