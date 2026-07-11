using Microsoft.AspNetCore.Identity;
using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        private User()
        {

        }

        public User(string firstName, string lastName ,string phoneNumber ,string email)
        {
            Id = new SequentialGuid.SequentialGuid();
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            UserValidation();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole Role { get; set; }
        public Company? Company { get; private set; }
        public Guid? CompanyId { get; private set; }
        public ICollection<RequestResume> RequestResumes { get; set; } = new List<RequestResume>();
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public DateTime CreatedAt { get;private set; }
        public DateTime? ModifiedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public void UpdateInfo(string firstName ,string lastName ,string phoneNumber ,string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void ChangeFirstName(string newFirstName)
        {
            if (string.IsNullOrEmpty(newFirstName))
                throw new Exception("FirstName is required");

            FirstName = newFirstName;
        }

        public void ChangeLastName(string newLastName)
        {
            if (string.IsNullOrEmpty(newLastName))
                throw new Exception("LastName is required");

            LastName = newLastName;
        }

        public void UserValidation()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new Exception("FirstName is null !!");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new Exception("LastName is null !!");
        }

        public void SetDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
