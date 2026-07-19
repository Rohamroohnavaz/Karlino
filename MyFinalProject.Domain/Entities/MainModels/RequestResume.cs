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

        public RequestResume(string jobSeekerName ,string jobSeekerLastName 
            ,string province ,string city ,DateTime startDate ,DateTime expireDate 
            ,Guid userId ,Guid advertisementId ,Guid attachmentId)
        {
            JobSeekerName = jobSeekerName;
            JobSeekerLastName = jobSeekerLastName;
            Province = province;
            City = city;
            StartDate = startDate;
            ExpireDate = expireDate;
            UserId = userId;
            AdvertisementId = advertisementId;
            AttachmentId = attachmentId;
            Validation();
        }

        public string JobSeekerName { get; private set; }
        public string JobSeekerLastName { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public RequestStatus Status { get; set; }
        public User? User { get; private set; }
        public Guid UserId { get; private set; }
        public Advertisement Advertisement { get; private set; }
        public Guid AdvertisementId { get; private set; }
        public Guid AttachmentId { get; private set; }
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

        public void ChangeProvince(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                throw new Exception("Province is required !!");

            Province = province;
        }

        public void ChangeCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new Exception("City is required !!");

            City = city;
        }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(JobSeekerName))
                throw new Exception("Name is null !!");

            if (string.IsNullOrWhiteSpace(JobSeekerLastName))
                throw new Exception("LastName is null !!");

            if (string.IsNullOrWhiteSpace(Province))
                throw new Exception("Province is null !!");

            if (string.IsNullOrWhiteSpace(City))
                throw new Exception("City is null !!");
        }
    }
}
