using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.DTO
{
    public class RawUserResult
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
    }
}
