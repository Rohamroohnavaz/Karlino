using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class ChangeRequestStatusDto
    {
        [Required(ErrorMessage = "RequestStatusId is required !!")]
        public Guid RequestResumeId { get; set; }

        public RequestStatus Status { get; set; }
    }
}
