using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;
        public DateTime ExpiresAt { get; private set; }
        public bool IsRevoked { get; private set; }

        
        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidTextException("Have not any token !!");
        }
    }
}
