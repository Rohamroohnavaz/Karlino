using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Abstraction
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = new SequentialGuid.SequentialGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public abstract void Validation(); 
    }
}
