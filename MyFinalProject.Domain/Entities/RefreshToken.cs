using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

        
        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidTextException("Have not any token !!");
        }
    }
}
