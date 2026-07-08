using Microsoft.AspNetCore.Identity;
using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public User()
        {

        }

        public User(string firstName, string lastName)
        {
            Id = new SequentialGuid.SequentialGuid();
            FirstName = firstName;
            LastName = lastName;
            UserValidation();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public ICollection<RequestResume> RequestResumes { get; set; } = new List<RequestResume>();
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void UpdateInfo(string firstName ,string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void UserValidation()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new Exception("FirstName is null !!");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new Exception("LastName is null !!");
        }
    }
}
