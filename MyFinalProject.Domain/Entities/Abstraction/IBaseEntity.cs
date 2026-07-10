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

        public void SetDeleted();
    }
}
