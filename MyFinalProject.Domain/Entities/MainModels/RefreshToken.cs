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
        private RefreshToken() { }


        public RefreshToken(string token, Guid userId, DateTime expiresAt)
        {
            Token = token;
            UserId = userId;
            ExpiresAt = expiresAt;
            IsRevoked = false;
            Validation();
        }

        public string Token { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? RevokeReason { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsActive => !IsRevoked && !IsExpired;

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidTextException("Have not any token !!");
        }
    }
}
