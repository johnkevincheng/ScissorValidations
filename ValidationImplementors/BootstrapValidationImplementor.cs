using System;
using System.Globalization;
using ScissorValidations.Validators;

namespace ScissorValidations.ValidationImplementors
{
    public class BootstrapValidationImplementor : IValidationImplementor
    {
        public void AttachValidators(StringValidatorAttribute validator, Object control)
        {
            FieldHelper.ApplyWebControlAttribute(control, "minlength", validator.MinSize.ToString(CultureInfo.InvariantCulture));
            FieldHelper.ApplyWebControlAttribute(control, "maxlength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                FieldHelper.ApplyWebControlAttribute(control, "required", "required");

            FieldHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(DateValidatorAttribute validator, Object control)
        {
            FieldHelper.ApplyWebControlAttribute(control, "type", "date");

            if (validator.IsRequired)
                FieldHelper.ApplyWebControlAttribute(control, "required", "required");

            FieldHelper.ApplyWebControlAttribute(control, "max", validator.MaximumDate.ToString(CultureInfo.InvariantCulture));
            FieldHelper.ApplyWebControlAttribute(control, "min", validator.MinimumDate.ToString(CultureInfo.InvariantCulture));

            if (!validator.AllowFutureDate && validator.MaximumDate > DateTime.UtcNow)
                FieldHelper.ApplyWebControlAttribute(control, "max", DateTime.Now.Date.ToString(CultureInfo.InvariantCulture));

            FieldHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(IntValidatorAttribute validator, Object control)
        {
            FieldHelper.ApplyWebControlAttribute(control, "type", "number");

            FieldHelper.ApplyWebControlAttribute(control, "min", validator.MinValue.ToString(CultureInfo.InvariantCulture));
            FieldHelper.ApplyWebControlAttribute(control, "max", validator.MaxValue.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                FieldHelper.ApplyWebControlAttribute(control, "required", "required");

            FieldHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(DoubleValidatorAttribute validator, Object control)
        {
            FieldHelper.ApplyWebControlAttribute(control, "type", "number");

            FieldHelper.ApplyWebControlAttribute(control, "step", Convert.ToString(1.0 / Math.Pow(10.0, validator.Decimals)));

            FieldHelper.ApplyWebControlAttribute(control, "min", validator.MinValue.ToString(CultureInfo.InvariantCulture));
            FieldHelper.ApplyWebControlAttribute(control, "max", validator.MaxValue.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                FieldHelper.ApplyWebControlAttribute(control, "required", "required");

            FieldHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }

        public void AttachValidators(EmailValidatorAttribute validator, Object control)
        {
            FieldHelper.ApplyWebControlAttribute(control, "type", "email");

            FieldHelper.ApplyWebControlAttribute(control, "minlength", validator.MinSize.ToString(CultureInfo.InvariantCulture));
            FieldHelper.ApplyWebControlAttribute(control, "maxlength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));

            if (validator.IsRequired)
                FieldHelper.ApplyWebControlAttribute(control, "required", "required");

            FieldHelper.ApplyWebControlAttribute(control, "data-error", "test message");
        }
    }
}
