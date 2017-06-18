﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ScissorValidations
{
    /// <summary>
    ///     Represents a validator attribute to handle email-specific behaviours.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EmailValidatorAttribute : Attribute, IValidatorAttribute
    {
        /// <summary>
        ///     Initializes a new EmailValidator attribute for the String property.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        public EmailValidatorAttribute(String fieldName, Int32 minSize, Int32 maxSize)
            : this()
        {
            FieldName = fieldName;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        /// <summary>
        ///     Initializes a new EmailValidator attribute for the String property.
        /// </summary>
        public EmailValidatorAttribute()
        {
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
        ///     The custom regular expression to use in place of the DefaultRegEx.
        /// </summary>
        public String CustomRegEx { get; set; }

        /// <summary>
        ///     The default regular expression used to validate an email.
        /// </summary>
        public String DefaultRegEx
        {
            get { return @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"; }
        }

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

            var attr = (EmailValidatorAttribute[]) property.GetCustomAttributes(typeof (EmailValidatorAttribute), true);
            if (attr.Length > 0)
            {
                EmailValidatorAttribute validator = attr[0];

                if (validator.IsRequired && value != null && value.Trim().Length > 0)
                    validations.Add(new Validation(property.Name, String.Format("{0} is a required field.", validator.FieldName)));

                String regEx = validator.DefaultRegEx;
                if (!String.IsNullOrEmpty(validator.CustomRegEx))
                    regEx = validator.CustomRegEx;

                if (!String.IsNullOrEmpty(value) && Regex.IsMatch(value, regEx, RegexOptions.IgnoreCase))
                    validations.Add(new Validation(property.Name, String.Format("{0} is not a valid email.", validator.FieldName)));
            }

            return validations;
        }
    }
}