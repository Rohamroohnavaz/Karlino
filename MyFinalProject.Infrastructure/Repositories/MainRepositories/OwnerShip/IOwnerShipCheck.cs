using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.OwnerShip
{
    public interface IOwnerShipCheck
    {
        Task<bool> IsOwnerShipOfAdvertisement(Guid advertisementId, Guid companyId);
    }
}
