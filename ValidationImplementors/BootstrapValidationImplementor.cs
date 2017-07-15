using System;
using System.Globalization;
using ScissorValidations.Validators;

namespace ScissorValidations.ValidationImplementors
{
    public class BootstrapValidationImplementor : IValidationImplementor
    {
        public void AttachValidators(StringValidatorAttribute validator, Object control)
        {
            WebHelper.ApplyWebControlAttribute(control, "minlength", validator.MinSize.ToString(CultureInfo.InvariantCulture));
            WebHelper.ApplyWebControlAttribute(control, "maxlength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                WebHelper.ApplyWebControlAttribute(control, "required", "required");

            WebHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(DateValidatorAttribute validator, Object control)
        {
            WebHelper.ApplyWebControlAttribute(control, "type", "date");

            if (validator.IsRequired)
                WebHelper.ApplyWebControlAttribute(control, "required", "required");

            WebHelper.ApplyWebControlAttribute(control, "max", validator.MaximumDate.ToString(CultureInfo.InvariantCulture));
            WebHelper.ApplyWebControlAttribute(control, "min", validator.MinimumDate.ToString(CultureInfo.InvariantCulture));

            if (!validator.AllowFutureDate && validator.MaximumDate > DateTime.UtcNow)
                WebHelper.ApplyWebControlAttribute(control, "max", DateTime.Now.Date.ToString(CultureInfo.InvariantCulture));

            WebHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(IntValidatorAttribute validator, Object control)
        {
            WebHelper.ApplyWebControlAttribute(control, "type", "number");

            WebHelper.ApplyWebControlAttribute(control, "min", validator.MinValue.ToString(CultureInfo.InvariantCulture));
            WebHelper.ApplyWebControlAttribute(control, "max", validator.MaxValue.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                WebHelper.ApplyWebControlAttribute(control, "required", "required");

            WebHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(DoubleValidatorAttribute validator, Object control)
        {
            WebHelper.ApplyWebControlAttribute(control, "type", "number");

            WebHelper.ApplyWebControlAttribute(control, "step", Convert.ToString(1.0 / Math.Pow(10.0, validator.Decimals)));

            WebHelper.ApplyWebControlAttribute(control, "min", validator.MinValue.ToString(CultureInfo.InvariantCulture));
            WebHelper.ApplyWebControlAttribute(control, "max", validator.MaxValue.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                WebHelper.ApplyWebControlAttribute(control, "required", "required");

            WebHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(EmailValidatorAttribute validator, Object control)
        {
            WebHelper.ApplyWebControlAttribute(control, "type", "email");

            WebHelper.ApplyWebControlAttribute(control, "minlength", validator.MinSize.ToString(CultureInfo.InvariantCulture));
            WebHelper.ApplyWebControlAttribute(control, "maxlength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                WebHelper.ApplyWebControlAttribute(control, "required", "required");

            WebHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }
    }
}
