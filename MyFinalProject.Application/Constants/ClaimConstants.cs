using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Constants
{
    public static class ClaimConstants
    {
        public readonly static Claim VipEmployer = new Claim("Level" ,"VIP");
    }
}
