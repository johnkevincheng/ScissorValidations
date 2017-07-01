﻿using System;
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
        /// <summary>
        ///     Initializes a new IntValidator attribute for the Int property.
        /// </summary>
        /// <param name="fieldLabel"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public IntValidatorAttribute(String fieldLabel, Int64 minValue, Int64 maxValue)
            : this()
        {
            FieldLabel = fieldLabel;

            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        ///     Initializes a new IntValidator attribute for the Int property.
        /// </summary>
        public IntValidatorAttribute()
        {
        }

        /// <summary>
        ///     Gets or sets the minimum integer size.
        /// </summary>
        public Int64 MinValue { get; set; }

        /// <summary>
        ///     Gets or sets the maximum integer size.
        /// </summary>
        public Int64 MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the label to use for the decorated property.
        /// </summary>
        public string FieldLabel { get; set; }

        /// <summary>
        ///     Gets or sets whether the property field is a required field.
        /// </summary>
        public Boolean IsRequired { get; set; }

        public enum IntegerScope
        {
            Int16,
            Int32,
            Int64
        }

        public List<Validation> Validate<T>(T entity, PropertyInfo property, String value)
        {
            var validations = new List<Validation>();

            var attr = (IntValidatorAttribute[])property.GetCustomAttributes(typeof(IntValidatorAttribute), true);
            if (attr.Length > 0)
            {
                IntValidatorAttribute validator = attr[0];

                if (property.PropertyType == typeof(Int16))
                {
                    if (MinValue < Int16.MinValue)
                        MinValue = Int16.MinValue;
                    if (MaxValue > Int16.MaxValue)
                        MaxValue = Int16.MaxValue;
                }
                else if (property.PropertyType == typeof(Int32))
                {
                    if (MinValue < Int32.MinValue)
                        MinValue = Int32.MinValue;
                    if (MaxValue > Int32.MaxValue)
                        MaxValue = Int32.MaxValue;
                }

                Int64 workingValue = 0;

                if (!Int64.TryParse(value, out workingValue))
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} is not recognized as a valid whole number.", validator.FieldLabel)));
                    return validations;
                }

                if (validator.IsRequired && (workingValue < validator.MinValue || workingValue > validator.MaxValue))
                {
                    validations.Add(new Validation(property.Name, String.Format("{0} must be between {1} and {2}.", validator.FieldLabel, validator.MinValue, validator.MaxValue)));
                    return validations;
                }

                if (Validator.Settings.CopyValuesOnValidate)
                    property.SetValue(entity, workingValue, null);
            }

            return validations;
        }
    }
}