using System;
using System.Globalization;
using System.Web.UI.WebControls;
using ScissorValidations;
using ScissorValidations.ValidationImplementors;
using ScissorValidations.Validators;

namespace ScissorsTest
{
    public class BootStrapValidationImplementor : IValidationImplementor
    {
        public void AttachValidators(StringValidatorAttribute validator, WebControl control)
        {
            control.Attributes.Add("minlength", validator.MinSize.ToString(CultureInfo.InvariantCulture));
            control.Attributes.Add("maxlength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                control.Attributes.Add("required", "required");

            control.Attributes.Add("data-error", "test message");
        }

        public void AttachValidators(DateValidatorAttribute validator, WebControl control)
        {
            control.Attributes.Add("type", "date");

            if (validator.IsRequired)
                control.Attributes.Add("required", "required");

            control.Attributes.Add("max", validator.MaximumDate.ToString(CultureInfo.InvariantCulture));
            control.Attributes.Add("min", validator.MinimumDate.ToString(CultureInfo.InvariantCulture));

            if (!validator.AllowFutureDate && validator.MaximumDate > DateTime.UtcNow)
                control.Attributes.Add("max", DateTime.Now.Date.ToString(CultureInfo.InvariantCulture));

            control.Attributes.Add("data-error", "test message");
        }

        public void AttachValidators(IntValidatorAttribute validator, WebControl control)
        {
            control.Attributes.Add("type", "number");

            control.Attributes.Add("min", validator.MinValue.ToString(CultureInfo.InvariantCulture));
            control.Attributes.Add("max", validator.MaxValue.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                control.Attributes.Add("required", "required");

            control.Attributes.Add("data-error", "test message");
        }

        public void AttachValidators(DoubleValidatorAttribute validator, WebControl control)
        {
            control.Attributes.Add("type", "number");

            control.Attributes.Add("step", Convert.ToString(1.0 / Math.Pow(10.0 , validator.Decimals)));

            control.Attributes.Add("min", validator.MinValue.ToString(CultureInfo.InvariantCulture));
            control.Attributes.Add("max", validator.MaxValue.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                control.Attributes.Add("required", "required");

            control.Attributes.Add("data-error", "test message");
        }

        public void AttachValidators(EmailValidatorAttribute validator, WebControl control)
        {
            control.Attributes.Add("type", "email");

            control.Attributes.Add("minlength", validator.MinSize.ToString(CultureInfo.InvariantCulture));
            control.Attributes.Add("maxlength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                control.Attributes.Add("required", "required");

            control.Attributes.Add("data-error", "test message");
        }
    }
}