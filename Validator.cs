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
        ///     Validate data object's configured properties which matches the field mappings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldMappings"></param>
        /// <returns></returns>
        public static List<Validation> Validate<T>(T entity, Dictionary<String, FieldMap> fieldMappings) where T : class
        {
            var validations = new List<Validation>();

            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (fieldMappings != null && (fieldMappings.ContainsKey(property.Name)))
                {
                    String propertyValue = fieldMappings[property.Name].FieldValue;

                    validations.AddRange(ValidateByAttribute<StringValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<DateValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<IntValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<DoubleValidatorAttribute, T>(entity, property, propertyValue));
                    validations.AddRange(ValidateByAttribute<EmailValidatorAttribute, T>(entity, property, propertyValue));
                }
            }

            return validations;
        }

        public class FieldMap
        {
            public WebControl WebControl { get; set; }
            public String FieldValue { get; set; }

            public FieldMap(WebControl webControl, String fieldValue)
            {
                this.WebControl = webControl;
                this.FieldValue = fieldValue;
            }
        }

        private static List<Validation> ValidateByAttribute<TValidatorAttribute, T>(T entity, PropertyInfo property, String value)
            where TValidatorAttribute : IValidatorAttribute, new()
            where T : class
        {
            if (HasAttribute<TValidatorAttribute>(property)) // Don't process this property if it doesn't have IValidatorAttribute.
            {
                var validator = Activator.CreateInstance<TValidatorAttribute>();
                return validator.Validate(entity, property, value);
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
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (fieldMappings.ContainsKey(property.Name))
                {
                    KeyValuePair<string, WebControl> fieldMap = fieldMappings.First(p => p.Key == property.Name);

                    var validator = new TValidationImplentor();

                    InitializeClientByAttribute<StringValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<DateValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<IntValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<DoubleValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                    InitializeClientByAttribute<EmailValidatorAttribute>(property, fieldMap.Value, validator.AttachValidators);
                }
            }
        }

        private static void InitializeClientByAttribute<T>(PropertyInfo property, WebControl control, Action<T, WebControl> decoratorAction) where T : IValidatorAttribute
        {
            if (HasAttribute<T>(property)) // Check if the property is decorated by attribute T. We should be assured of at least one such IValidatorAttribute if true.
            {
                var attribute = (T)property.GetCustomAttributes(typeof(T), true)[0];
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
            return Attribute.IsDefined(property, typeof(T));
        }


        public static ScissorsSettings Settings = new ScissorsSettings();

        public class ScissorsSettings
        {
            internal ScissorsSettings()
            {

            }

            public Boolean CopyValuesOnValidate { get; set; }
        }
    }
}