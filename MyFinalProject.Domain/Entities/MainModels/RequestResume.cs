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
            Validation();
        }

        public string JobSeekerName { get; set; }
        public string JobSeekerLastName { get; set; }
        public string Skill { get; set; }
        public RequestStatus Status { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Advertisement Advertisement { get; set; }
        public Guid? AttachmentId { get; set; }
        public Attach Attach { get; set; }

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
