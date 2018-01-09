using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using RockFluid.ScissorValidations;
using RockFluid.ScissorValidations.Validators;

namespace ScissorsTestWindows
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TestValidatorAttribute : Attribute, IValidatorAttribute
    {
        public string FieldLabel { get; private set; }
        public bool IsRequired { get; set; }

        public List<Validation> Validate<T>(T entity, PropertyInfo property, string value)
        {
            return new List<Validation>();
        }
    }
}
