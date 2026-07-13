using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Abstraction
{
    public interface IBaseEntity
    {
        public DateTime CreatedAt { get; }

        public DateTime? ModifiedAt { get; }

        public bool IsDeleted { get; }

        public DateTime? DeletedAt { get; }

        public Guid? CreateById { get; }
        public User? Creater { get; }
        public Guid? ModifiedById { get; }
        public User? Modifier { get; }
        public Guid? DeletedById { get; }
        public User? Deleter { get; }

        public void SetDeleted(Guid id);
    }
}
