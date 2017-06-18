using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScissorValidations
{
    /// <summary>
    ///     Represents a validator attribute to handle String-specific behaviours.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StringValidatorAttribute : Attribute, IValidatorAttribute
    {
        /// <summary>
        ///     Initializes a new StringValidator attribute for the String property.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        public StringValidatorAttribute(String fieldName, Int32 minSize, Int32 maxSize)
            : this()
        {
            FieldName = fieldName;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        /// <summary>
        ///     Initializes a new StringValidator attribute for the String property.
        /// </summary>
        public StringValidatorAttribute()
        {
            AllowRichText = false;
        }

        /// <summary>
        ///     Gets or sets the minimum text size.
        /// </summary>
        public Int32 MinSize { get; set; }

        /// <summary>
        ///     Gets or sets the maximum text size.
        /// </summary>
        public Int32 MaxSize { get; set; }

        /// <summary>
        ///     Gets or sets whether to allow rich text editors for this field.
        /// </summary>
        public Boolean AllowRichText { get; set; }

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

            var attr = (StringValidatorAttribute[]) property.GetCustomAttributes(typeof (StringValidatorAttribute), true);
            if (attr.Length > 0)
            {
                StringValidatorAttribute validator = attr[0];

                if (validator.IsRequired && (value == null || value.Trim().Length <= 0))
                    validations.Add(new Validation(property.Name, String.Format("{0} is a required field.", validator.FieldName)));

                if (value.Length > 0 && value.Length < validator.MinSize)
                    validations.Add(new Validation(property.Name, String.Format("{0} must be more than {1} characters.", validator.FieldName, validator.MinSize)));

                if (value.Length > 0 && value.Length > validator.MaxSize)
                    validations.Add(new Validation(property.Name, String.Format("{0} must be less than {1} character(s).", validator.FieldName, validator.MaxSize)));
            }

            return validations;
        }
    }
}