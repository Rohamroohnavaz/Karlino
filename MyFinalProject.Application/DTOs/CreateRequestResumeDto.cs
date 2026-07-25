using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class CreateRequestResumeDto
    {
        public string JobSeekerName { get; set; }
        public string JobSeekerLastName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid UserId { get; set; }
        public Guid AdvertisementId { get; set; }
        public Guid AttachmentId { get; set; }
    }
}
