using System;
using System.Collections.Generic;
using System.Reflection;

namespace RockFluid.ScissorValidations.Validators
{
    /// <summary>
    /// Represents a validator attribute to handle String-specific behaviours.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StringValidatorAttribute : Attribute, IValidatorAttribute
    {
        /// <summary>
        /// Initializes a new StringValidator attribute for the String property.
        /// </summary>
        /// <param name="fieldLabel"></param>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        public StringValidatorAttribute(String fieldLabel, Int32 minSize, Int32 maxSize)
            : this()
        {
            FieldLabel = fieldLabel;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        /// <summary>
        /// Initializes a new StringValidator attribute for the String property.
        /// </summary>
        public StringValidatorAttribute()
        {
            AllowRichText = false;
        }

        /// <summary>
        /// Gets the minimum text size.
        /// </summary>
        public Int32 MinSize { get; private set; }

        /// <summary>
        /// Gets the maximum text size.
        /// </summary>
        public Int32 MaxSize { get; private set; }

        /// <summary>
        /// Gets whether to allow rich text editors for this field.
        /// </summary>
        public Boolean AllowRichText { get; set; }

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

            var attr = (StringValidatorAttribute[])property.GetCustomAttributes(typeof(StringValidatorAttribute), true);
            if (attr.Length > 0)
            {
                StringValidatorAttribute validator = attr[0];

                if (validator.IsRequired && (value == null || value.Trim().Length <= 0))
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} is a required field.", validator.FieldLabel)));
                    return validations;
                }

                if (!String.IsNullOrEmpty(value) && value.Length < validator.MinSize)
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} must be more than {1} characters.", validator.FieldLabel, validator.MinSize)));
                    return validations;
                }

                if (!String.IsNullOrEmpty(value) && value.Length > validator.MaxSize)
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} must be less than {1} character(s).", validator.FieldLabel, validator.MaxSize)));
                    return validations;
                }

                if (Validator.Settings.CopyValuesOnValidate)
                    property.SetValue(entity, value, null);
            }

            return validations;
        }
    }
}