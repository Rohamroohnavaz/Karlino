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

        public string JobSeekerName { get; private set; }
        public string JobSeekerLastName { get; private set; }
        public string Skill { get; private set; }
        public RequestStatus Status { get; set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public Advertisement Advertisement { get; private set; }
        public Guid? AttachmentId { get; private set; }
        public Attach Attach { get; private set; }

        public void ChangeJobSeekerName(string jobSeekerName)
        {
            if (string.IsNullOrWhiteSpace(jobSeekerName))
                throw new Exception("JobSeekerName is required !!");

            JobSeekerName = jobSeekerName;
        }

        public void ChangeJobSeekerLastName(string jobSeekerLastName)
        {
            if (string.IsNullOrWhiteSpace(jobSeekerLastName))
                throw new Exception("JobSeekerLastName is required !!");

            JobSeekerLastName = jobSeekerLastName;
        }

        public void ChangeSkill(string newSkill)
        {
            if (string.IsNullOrWhiteSpace(newSkill))
                throw new Exception("Skill is required !!");

            Skill = newSkill;
        }

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
