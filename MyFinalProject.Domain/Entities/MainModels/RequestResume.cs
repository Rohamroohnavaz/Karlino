using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class RequestResume : BaseEntity
    {
        private RequestResume()
        {
            
        }

        public RequestResume(string jobSeekerName ,string jobSeekerLastName ,string skill 
            ,string province ,string city)
        {
            JobSeekerName = jobSeekerName;
            JobSeekerLastName = jobSeekerLastName;
            Skill = skill;
            Province = province;
            City = city;
            Validation();
        }

        public string JobSeekerName { get; private set; }
        public string JobSeekerLastName { get; private set; }
        public string Skill { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public RequestStatus Status { get; set; }
        public User? User { get; private set; }
        public Guid? UserId { get; private set; }
        public Advertisement Advertisement { get; private set; }
        public Guid? AdvertisementId { get; private set; }
        public Guid? AttachmentId { get; private set; }
        public Attach? Attach { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpireDate { get; private set; }

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
            if (UserId == Guid.Empty)
                throw new InvalidGuidException("UserId is empty !!");

            if (AttachmentId == Guid.Empty)
                throw new InvalidGuidException("AttachmentId is empty !!");

            if (AdvertisementId == Guid.Empty)
                throw new InvalidGuidException("AdvertisementId is empty !!");

            if (string.IsNullOrWhiteSpace(JobSeekerName))
                throw new Exception("Name is null !!");

            if (string.IsNullOrWhiteSpace(JobSeekerLastName))
                throw new Exception("LastName is null !!");

            if (string.IsNullOrWhiteSpace(Skill))
                throw new Exception("Skill is null !!");
        }
    }
}
