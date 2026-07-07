using MyFinalProject.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Attach : BaseEntity
    {
        public Attach(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                throw new Exception("FilePath is null !!");
        }
    }
}
