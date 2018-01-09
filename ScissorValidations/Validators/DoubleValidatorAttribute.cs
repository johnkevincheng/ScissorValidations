using System;
using System.Collections.Generic;
using System.Reflection;

namespace RockFluid.ScissorValidations.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DoubleValidatorAttribute : Attribute, IValidatorAttribute
    {
        /// <summary>
        /// Initializes a new DecimalValidator attribute for the floating-point number property.
        /// </summary>
        /// <param name="fieldLabel"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public DoubleValidatorAttribute(String fieldLabel, Double minValue, Double maxValue)
            : this()
        {
            FieldLabel = fieldLabel;

            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new DecimalValidator attribute for the floating-point number property.
        /// </summary>
        public DoubleValidatorAttribute()
        {
            Decimals = 2;
        }

        /// <summary>
        /// Gets the number of decimal places.
        /// </summary>
        public Int16 Decimals { get; set; }

        /// <summary>
        /// Gets the minimum floating-point number size.
        /// </summary>
        public Double MinValue { get; private set; }

        /// <summary>
        /// Gets the maximum floating-point number size.
        /// </summary>
        public Double MaxValue { get; private set; }

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

            var attr = (DoubleValidatorAttribute[])property.GetCustomAttributes(typeof(DoubleValidatorAttribute), true);
            if (attr.Length > 0)
            {
                DoubleValidatorAttribute validator = attr[0];

                Double workingValue = 0;

                if (!Double.TryParse(value, out workingValue))
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} is not recognized as a valid number.", validator.FieldLabel)));
                    return validations;
                }

                if (validator.IsRequired && (workingValue < validator.MinValue || workingValue > validator.MaxValue))
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} must be between {1} and {2}.", validator.FieldLabel, validator.MinValue, validator.MaxValue)));
                    return validations;
                }

                //-- Couldn't setValue the Double value directly, so use workaround found here: https://stackoverflow.com/a/13270302
                Boolean isNullable = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
                var targetType = isNullable ? Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;

                if (Validator.Settings.CopyValuesOnValidate)
                    property.SetValue(entity, Convert.ChangeType(workingValue, targetType), null);
            }

            return validations;
        }
    }
}
