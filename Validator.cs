using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScissorValidations.ValidationImplementors;
using ScissorValidations.Validators;

namespace ScissorValidations
{
    /// <summary>
    /// Represents the main Scissors Validation Framework validation processor class.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validate data object's configured properties which matches the field mappings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldMappings"></param>
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

                    validations.AddRange(ValidateByAttribute<StringValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<DateValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<IntValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<DoubleValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<EmailValidatorAttribute, T>(entity, property, propertyValue));
                }
            }

            return new ValidationResult(validations);
        }

        private static List<Validation> ValidateByAttribute<TValidatorAttribute, T>(T entity, PropertyInfo property, String value)
            where TValidatorAttribute : IValidatorAttribute, new()
            where T : class
        {
            if (property.HasAttribute<TValidatorAttribute>()) // Don't process this property if it doesn't have IValidatorAttribute.
            {
                var validator = Activator.CreateInstance<TValidatorAttribute>();
                return validator.Validate(entity, property, value);
            }

            return new List<Validation>();
        }


        /// <summary>
        /// Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldMappings"></param>
        public static void InitializeClientValidators<TEntity>(Dictionary<String, Object> fieldMappings)
        {
            if (Settings.DefaultImplementor == null)
                throw new NullReferenceException("Default Implementor has not been set yet.");

            InitializeClientValidators<TEntity>(fieldMappings, Settings.DefaultImplementor);
        }

        /// <summary>
        /// Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValidationImplentor"></typeparam>
        /// <param name="fieldMappings"></param>
        public static void InitializeClientValidators<TEntity, TValidationImplentor>(Dictionary<String, Object> fieldMappings) where TValidationImplentor : IValidationImplementor, new()
        {
            InitializeClientValidators<TEntity>(fieldMappings, new TValidationImplentor());
        }

        /// <summary>
        /// Decorate user interface controls with client-side validation configurations.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldMappings"></param>
        /// <param name="validator"></param>
        private static void InitializeClientValidators<TEntity>(Dictionary<String, Object> fieldMappings, IValidationImplementor validator)
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (fieldMappings.ContainsKey(property.Name))
                {
                    KeyValuePair<String, Object> fieldMap = fieldMappings.First(p => p.Key == property.Name);

                    InitializeClientByAttribute<StringValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<DateValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<IntValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<DoubleValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<EmailValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                }
            }
        }

        private static void InitializeClientByAttribute<T>(PropertyInfo property, Object control, Action<T, Object> decoratorAction) where T : IValidatorAttribute
        {
            if (property.HasAttribute<T>()) // Check if the property is decorated by attribute T. We should be assured of at least one such IValidatorAttribute if true.
            {
                var attribute = (T)property.GetCustomAttributes(typeof(T), true)[0];
                decoratorAction(attribute, control);
            }
        }


        /// <summary>
        /// Sets various global Scissor Validations settings.
        /// </summary>
        public static ScissorsSettings Settings = new ScissorsSettings();
    }
}