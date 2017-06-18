using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;

namespace ScissorValidations
{
    /// <summary>
    ///     Represents the main Scissors Validation Framework validation processor class.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        ///     Validate data object's configured properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<Validation> Validate<T>(T entity)
        {
            return Validate(entity, null);
        }

        /// <summary>
        ///     Validate data object's configured properties which matches the field mappings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldMappings"></param>
        /// <returns></returns>
        public static List<Validation> Validate<T>(T entity, Dictionary<String, WebControl> fieldMappings)
        {
            var validations = new List<Validation>();

            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (fieldMappings == null || (fieldMappings.ContainsKey(property.Name)))
                {
                    String propertyValue = Convert.ToString(property.GetValue(entity, null));

                    validations.AddRange(ValidateByAttribute<StringValidatorAttribute>(property, propertyValue));
                    validations.AddRange(ValidateByAttribute<DateValidatorAttribute>(property, propertyValue));
                    validations.AddRange(ValidateByAttribute<IntValidatorAttribute>(property, propertyValue));
                    validations.AddRange(ValidateByAttribute<EmailValidatorAttribute>(property, propertyValue));
                }
            }

            return validations;
        }

        private static List<Validation> ValidateByAttribute<T>(PropertyInfo property, String value) where T : IValidatorAttribute, new()
        {
            if (HasAttribute<T>(property)) // Don't process this property if it doesn't have IValidatorAttribute.
            {
                var validator = Activator.CreateInstance<T>();
                return validator.Validate(property, value);
            }

            return new List<Validation>();
        }


        /// <summary>
        ///     Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValidationImplentor"></typeparam>
        /// <param name="fieldMappings"></param>
        public static void InitializeClientValidators<TEntity, TValidationImplentor>(Dictionary<String, WebControl> fieldMappings) where TValidationImplentor : IValidationImplementor, new()
        {
            PropertyInfo[] properties = typeof (TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (fieldMappings.ContainsKey(property.Name))
                {
                    KeyValuePair<string, WebControl> fieldMap = fieldMappings.First(p => p.Key == property.Name);

                    var validator = new TValidationImplentor();

                    InitializeClientByAttribute<StringValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<DateValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<IntValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<EmailValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                }
            }
        }

        private static void InitializeClientByAttribute<T>(PropertyInfo property, WebControl control, Action<T, WebControl> decoratorAction) where T : IValidatorAttribute
        {
            if (HasAttribute<T>(property)) // Check if the property is decorated by attribute T. We should be assured of at least one such IValidatorAttribute if true.
            {
                var attribute = (T) property.GetCustomAttributes(typeof (T), true)[0];
                decoratorAction(attribute, control);
            }
        }


        /// <summary>
        ///     Checks whether the PropertyInfo is decorated by the specified IValidatorAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        private static Boolean HasAttribute<T>(PropertyInfo property) where T : IValidatorAttribute
        {
            return Attribute.IsDefined(property, typeof (T));
        }
    }
}