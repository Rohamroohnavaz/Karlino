using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class ChangeRequestStatusDto
    {
        public Guid RequestResumeId { get; set; }

        public RequestStatus Status { get; set; }
    }
}
