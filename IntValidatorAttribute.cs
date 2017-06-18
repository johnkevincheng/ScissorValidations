using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScissorValidations
{
    /// <summary>
    ///     Represents a validator attribute to handle Integer-specific behaviours.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IntValidatorAttribute : Attribute, IValidatorAttribute
    {
        public enum IntegerScope
        {
            Int16,
            Int32,
            Int64
        }

        /// <summary>
        ///     Initializes a new IntValidator attribute for the Int property.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="scope"></param>
        public IntValidatorAttribute(string fieldName, Int64 minValue, Int64 maxValue, IntegerScope scope)
            : this()
        {
            FieldName = fieldName;
            Scope = scope;

            MinValue = minValue;
            MaxValue = maxValue;

            if (scope == IntegerScope.Int16)
            {
                if (MinValue < Int16.MinValue)
                    MinValue = Int16.MinValue;
                if (maxValue > Int16.MaxValue)
                    MaxValue = Int16.MaxValue;
            }
            else if (scope == IntegerScope.Int32)
            {
                if (MinValue < Int32.MinValue)
                    MinValue = Int32.MinValue;
                if (maxValue > Int32.MaxValue)
                    MaxValue = Int32.MaxValue;
            }
            else if (scope == IntegerScope.Int64)
            {
                if (MinValue < Int64.MinValue)
                    MinValue = Int64.MinValue;
                if (maxValue > Int64.MaxValue)
                    MaxValue = Int64.MaxValue;
            }
        }

        /// <summary>
        ///     Initializes a new IntValidator attribute for the Int property.
        /// </summary>
        public IntValidatorAttribute()
        {
        }

        /// <summary>
        ///     Gets or sets the Integer size.
        /// </summary>
        public IntegerScope Scope { get; set; }

        /// <summary>
        ///     Gets or sets the minimum integer size.
        /// </summary>
        public Int64 MinValue { get; set; }

        /// <summary>
        ///     Gets or sets the maximum integer size.
        /// </summary>
        public Int64 MaxValue { get; set; }

        /// <summary>
        ///     Gets or sets the friendly field name for the decorated property.
        /// </summary>
        public String FieldName { get; set; }

        /// <summary>
        ///     Gets or sets whether the property field is a required field.
        /// </summary>
        public Boolean IsRequired { get; set; }

        public List<Validation> Validate(PropertyInfo property, String value)
        {
            var validations = new List<Validation>();

            var attr = (IntValidatorAttribute[]) property.GetCustomAttributes(typeof (IntValidatorAttribute), true);
            if (attr.Length > 0)
            {
                IntValidatorAttribute validator = attr[0];

                Int64 intValue = 0;

                if (!Int64.TryParse(value, out intValue))
                    validations.Add(new Validation(property.Name, String.Format("{0} is not recognized as a valid number.", validator.FieldName)));

                if (validator.IsRequired && intValue < validator.MinValue)
                    validations.Add(new Validation(property.Name, String.Format("{0} is a required field.", validator.FieldName)));
            }

            return validations;
        }
    }
}