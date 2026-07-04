using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.Abstraction
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ModifyAt { get; set; }
    }
}
