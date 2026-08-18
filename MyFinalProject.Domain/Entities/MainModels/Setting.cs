using MyFinalProject.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Setting : BaseEntity
    {
        protected Setting() { }

        public Setting(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;

        public void UpdateValue(string newValue)
        {
            Value = newValue;
        }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Key))
                throw new Exception("Setting Key Is Invalid !!");

            if (string.IsNullOrWhiteSpace(Value))
                throw new Exception("Setting Value Is Invalid !!");
        }
    }
}
