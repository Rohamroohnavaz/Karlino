using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Category : BaseEntity
    {
        private Category()
        {
            
        }

        public Category(string categoryName ,string description)
        {
            CategoryName = categoryName;
            Description = description;
            Validation();
        }

        public string CategoryName { get; private set; }

        public string Description { get; private set; }

        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
                throw new InvalidNameException("CategoryName is required !");

            if (string.IsNullOrWhiteSpace(Description))
                throw new InvalidTextException("Description is required !");
        }
    }
}
