using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Skill : BaseEntity
    {
        private Skill()
        {
            
        }

        public Skill(string name ,string description ,int experienceYear)
        {
            Name = name;
            Description = description;
            ExperienceYear = experienceYear;
            Validation();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public SkillLevel SkillLevel { get; private set; }
        public int ExperienceYear { get; private set; }
        public Guid? SkillCategoryId { get; private set; }
        public SkillCategory SkillCategory { get; private set; }
        public User? User { get; private set; }
        public Guid? UserId { get; private set; }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidNameException("Skill's name is required !!");
        }
    }
}
