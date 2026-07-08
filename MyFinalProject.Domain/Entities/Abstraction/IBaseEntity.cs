using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Abstraction
{
    public interface IBaseEntity
    {
        public DateTime CreatedAt { get; protected set; }

        public DateTime? ModifiedAt { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }

        public void SetDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
