using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class SkillCategory : BaseEntity
    {
        private SkillCategory()
        {
            
        }

        public SkillCategory(string name, string description)
        {
            Name = name;
            Description = description;
            Validation();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidNameException("SkillName is required !!");

            if (string.IsNullOrWhiteSpace(Description))
                throw new InvalidNameException("SkillDescription is required !!");
        }
    }
}
