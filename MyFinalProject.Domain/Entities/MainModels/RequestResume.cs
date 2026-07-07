using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class RequestResume : BaseEntity
    {
        public RequestResume()
        {
            
        }

        public RequestResume(string jobSeekerName ,string jobSeekerLastName ,string skill)
        {
            JobSeekerName = jobSeekerName;
            JobSeekerLastName = jobSeekerLastName;
            Skill = skill;
        }

        public string JobSeekerName { get; set; }
        public string JobSeekerLastName { get; set; }
        public string Skill { get; set; }
        public RequestStatus Status { get; set; }
        public User User { get; set; }
        public UserRole Role { get; set; } = UserRole.JobSeeker;

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(JobSeekerName))
                throw new Exception("Name is null !!");

            if (string.IsNullOrWhiteSpace(JobSeekerLastName))
                throw new Exception("LastName is null !!");

            if (string.IsNullOrWhiteSpace(Skill))
                throw new Exception("Skill is null !!");
        }
    }
}
