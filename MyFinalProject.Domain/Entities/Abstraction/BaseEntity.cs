using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Abstraction
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; private set; } = new SequentialGuid.SequentialGuid();
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Guid? CreateById { get; private set; }
        public User? Creater { get; private set; }
        public Guid? ModifiedById { get; private set; }
        public User? Modifier { get; private set; }
        public Guid? DeletedById { get; private set; }
        public User? Deleter { get; private set; }

        public void SetDeleted(Guid id)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedById = id;
        }

        public abstract void Validation();
    }
}
