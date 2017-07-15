using System;

namespace ScissorValidations
{
    public class WebHelper
    {
        /// <summary>
        /// Inserts the attribute value to the control if it supports it.
        /// </summary>
        /// <param name="webControl"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void ApplyWebControlAttribute(dynamic webControl, String attribute, String value)
        {
            Type controlType = webControl.GetType();
            if (controlType.GetProperty("Attributes") != null)
            {
                var attr = webControl.Attributes;

                Type attrType = attr.GetType();
                if (attrType.GetMethod("Add") != null)
                {
                    webControl.Attributes.Add(attribute, value);
                }
            }
        }
    }
}
