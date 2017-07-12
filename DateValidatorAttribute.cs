using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScissorValidations
{
    /// <summary>
    /// Represents a validator attribute to handle DateTime-specific behaviours.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DateValidatorAttribute : Attribute, IValidatorAttribute
    {
        /// <summary>
        /// Initializes a new DateValidator attribute for the DateTime property.
        /// </summary>
        /// <param name="fieldLabel"></param>
        public DateValidatorAttribute(String fieldLabel)
            : this()
        {
            FieldLabel = fieldLabel;
        }

        /// <summary>
        /// Initializes a new DateValidator attribute for the DateTime property.
        /// </summary>
        public DateValidatorAttribute()
        {
            AllowFutureDate = false;
        }

        /// <summary>
        /// Gets whether the date field should accepts future dates.
        /// </summary>
        public Boolean AllowFutureDate { get; set; }

        /// <summary>
        /// Gets the minimum allowed date value.
        /// </summary>
        public DateTime MinimumDate { get; set; }

        /// <summary>
        /// Gets the maximum allowed date value.
        /// </summary>
        public DateTime MaximumDate { get; set; }

        /// <summary>
        /// Gets the label to use for the decorated property.
        /// </summary>
        public String FieldLabel { get; private set; }

        /// <summary>
        /// Gets whether the property field is a required field.
        /// </summary>
        public Boolean IsRequired { get; set; }

        public List<Validation> Validate<T>(T entity, PropertyInfo property, String value)
        {
            var validations = new List<Validation>();

            var attr = (DateValidatorAttribute[])property.GetCustomAttributes(typeof(DateValidatorAttribute), true);
            if (attr.Length > 0)
            {
                DateValidatorAttribute validator = attr[0];

                DateTime workingValue;

                if (!DateTime.TryParse(value, out workingValue))
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} is not a recognized date value.", validator.FieldLabel)));
                    return validations;
                }

                if (validator.IsRequired && workingValue == DateTime.MinValue)
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} is a required field.", validator.FieldLabel)));
                    return validations;
                }

                if (!validator.AllowFutureDate && workingValue > DateTime.Now)
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} cannot be a future date.", validator.FieldLabel)));
                    return validations;
                }

                if (Validator.Settings.CopyValuesOnValidate)
                    property.SetValue(entity, workingValue, null);
            }

            return validations;
        }
    }
}