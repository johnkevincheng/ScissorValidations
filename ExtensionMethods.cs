using System;
using System.Reflection;
using ScissorValidations.Validators;

namespace ScissorValidations
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Checks whether the PropertyInfo is decorated by the specified IValidatorAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Boolean HasAttribute<T>(this PropertyInfo property) where T : IValidatorAttribute
        {
            return Attribute.IsDefined(property, typeof(T));
        }
    }
}