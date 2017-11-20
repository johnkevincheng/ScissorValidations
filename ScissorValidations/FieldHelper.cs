using System;
using System.Linq;
using System.Reflection;

namespace ScissorValidations
{
    public class FieldHelper
    {
        /// <summary>
        /// Inserts the attribute value to the control if it supports it.
        /// </summary>
        /// <param name="webControl"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void ApplyWebControlAttribute(dynamic webControl, String attribute, String value)
        {
            if (CanAddAttributes(webControl))
                webControl.Attributes.Add(attribute, value);
        }

        /// <summary>
        /// Checks whether the object has an Attributes property that has an Add method.
        /// </summary>
        /// <param name="webControl"></param>
        /// <returns></returns>
        protected static Boolean CanAddAttributes(dynamic webControl)
        {
            if (webControl.GetType().GetProperty("Attributes") != null && webControl.Attributes.GetType().GetMethod("Add") != null)
                return true;

            return false;
        }

        /// <summary>
        /// Sets the object's property value to the specified value.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(dynamic control, String propertyName, String value)
        {
            if (HasProperty(control, propertyName))
            {
                PropertyInfo[] properties = control.GetType().GetProperties();

                if (properties.Any(p => p.Name == propertyName))
                {
                    var prop = properties.First(p => p.Name == propertyName);
                    prop.SetValue(control, Convert.ChangeType(value, prop.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// Checks whether the object contains a property of the specified name.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Boolean HasProperty(dynamic control, String propertyName)
        {
            return control.GetType().GetProperty(propertyName) != null;
        }
    }
}
