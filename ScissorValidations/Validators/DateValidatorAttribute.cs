using System;
using System.Collections.Generic;
using System.Reflection;

namespace RockFluid.ScissorValidations.Validators
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
        /// <param name="fieldLabel">The friendly field name to show in findings.</param>
        public DateValidatorAttribute(String fieldLabel)
            : this()
        {
            FieldLabel = fieldLabel;
        }

        /// <summary>
        /// Initializes a new DateValidator attribute for the DateTime property.
        /// </summary>
        /// <param name="fieldLabel">The friendly field name to show in findings.</param>
        /// <param name="minDate">The minimum date in the format yyyy-mm-dd.</param>
        public DateValidatorAttribute(String fieldLabel, String minDate)
            : this()
        {
            FieldLabel = fieldLabel;

            DateTime workingDate;
            if (DateTime.TryParse(minDate, out workingDate)) MinimumDate = workingDate;
        }

        /// <summary>
        /// Initializes a new DateValidator attribute for the DateTime property.
        /// </summary>
        /// <param name="fieldLabel">The friendly field name to show in findings.</param>
        /// <param name="minDate">The minimum date in the format yyyy-mm-dd.</param>
        /// <param name="maxDate">The maximum date in the format yyyy-mm-dd.</param>
        public DateValidatorAttribute(String fieldLabel, String minDate, String maxDate)
            : this()
        {
            FieldLabel = fieldLabel;

            DateTime workingDate;
            if (DateTime.TryParse(minDate, out workingDate)) MinimumDate = workingDate;
            if (DateTime.TryParse(maxDate, out workingDate)) MaximumDate = workingDate;
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
        public DateTime MinimumDate { get; private set; }

        /// <summary>
        /// Gets the maximum allowed date value.
        /// </summary>
        public DateTime MaximumDate { get; private set; }

        /// <summary>
        /// Gets the label to use for the decorated property.
        /// </summary>
        public String FieldLabel { get; private set; }

        /// <summary>
        /// Gets whether the property field is a required field.
        /// </summary>
        public Boolean IsRequired { get; set; }

        /// <summary>
        /// Validate the Date field using date-specific validation strategies.
        /// </summary>
        /// <typeparam name="T">The type of the data object whose fields are being validated.</typeparam>
        /// <param name="entity">The data object whose fields are being validated.</param>
        /// <param name="property">The reflected PropertyInfo which contains the validation attributes.</param>
        /// <param name="value">The field value to validate.</param>
        /// <returns></returns>
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