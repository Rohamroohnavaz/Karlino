using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Enums
{
    public enum RequestStatus
    {
        None = 0,
        Pending = 1,
        CurrentlyViewing = 2,
        Interview = 3,
        Success = 4,
        Fail = 5
    }
}
