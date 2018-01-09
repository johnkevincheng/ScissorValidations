using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RockFluid.ScissorValidations.ValidationImplementors;
using RockFluid.ScissorValidations.Validators;

namespace RockFluid.ScissorValidations
{
    /// <summary>
    /// Represents the main Scissors Validation Framework validation processor class.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validate data object's configured properties which matches the field mappings.
        /// </summary>
        /// <typeparam name="T">The type of the data class containing the validation rules.</typeparam>
        /// <param name="entity">The data class instance containing the validation rules. Also the same instance to receive validated data if Validator.Settings.CopyValuesOnValidate is set.</param>
        /// <param name="fieldMappings">The property/value mapping to identify the property to the value to validate.</param>
        /// <returns></returns>
        public static ValidationResult Validate<T>(T entity, Dictionary<String, String> fieldMappings) where T : class
        {
            var validations = new List<Validation>();

            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (fieldMappings != null && (fieldMappings.ContainsKey(property.Name)))
                {
                    String propertyValue = fieldMappings[property.Name];

                    var validatorAttributes = property.GetCustomAttributes(typeof(IValidatorAttribute), true).OfType<IValidatorAttribute>();
                    foreach (var validatorAttribute in validatorAttributes)
                        validations.AddRange(validatorAttribute.Validate(entity, property, propertyValue));
                }
            }

            return new ValidationResult(validations);
        }


        /// <summary>
        /// Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="TEntity">The type of the data class containing the validation rules.</typeparam>
        /// <param name="fieldMappings">The property/control mapping to identify the property to the control to decorate.</param>
        public static void InitializeClientValidators<TEntity>(Dictionary<String, Object> fieldMappings)
        {
            if (Settings.DefaultImplementor == null)
                throw new NullReferenceException("Default Implementor has not been set yet.");

            InitializeClientValidators<TEntity>(fieldMappings, Settings.DefaultImplementor);
        }

        /// <summary>
        /// Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="TEntity">The type of the data class containing the validation rules.</typeparam>
        /// <typeparam name="TValidationImplementor">The IValidationImplementor implementation to use to decorate the controls.</typeparam>
        /// <param name="fieldMappings">The property/control mapping to identify the property to the control to decorate.</param>
        public static void InitializeClientValidators<TEntity, TValidationImplementor>(Dictionary<String, Object> fieldMappings) where TValidationImplementor : IValidationImplementor, new()
        {
            InitializeClientValidators<TEntity>(fieldMappings, new TValidationImplementor());
        }

        /// <summary>
        /// Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldMappings"></param>
        /// <param name="validationImplementor"></param>
        private static void InitializeClientValidators<T>(Dictionary<String, Object> fieldMappings, IValidationImplementor validationImplementor)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties.Where(p => fieldMappings.ContainsKey(p.Name)))
            {
                KeyValuePair<String, Object> fieldMap = fieldMappings.First(p => p.Key == property.Name);
                var attributes = property.GetCustomAttributes(typeof(IValidatorAttribute), true).OfType<IValidatorAttribute>().ToList();

                validationImplementor.AttachValidators(attributes, fieldMap.Value);
            }
        }


        /// <summary>
        /// Sets various global Scissor Validations settings.
        /// </summary>
        public static ScissorsSettings Settings = new ScissorsSettings();
    }
}