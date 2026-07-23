using MyFinalProject.Application.DTOs;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface IAttachService
    {
        Task<Attach> CreateAttachAsync(UploadAttachDto dto);

        Task DeleteAttachAsync(Guid attachId);
    }
}
