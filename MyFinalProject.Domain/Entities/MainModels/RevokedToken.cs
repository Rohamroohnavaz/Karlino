using MyFinalProject.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class RevokedToken
    {
        public Guid RevokeId { get; set; }
        public string Jti { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime RevokedAtUtc { get; set; }
    }
}
