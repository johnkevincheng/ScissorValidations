using System;
using System.Globalization;
using ScissorValidations.Validators;

namespace ScissorValidations.ValidationImplementors
{
    public class WinFormsValidationImplmentor : IValidationImplementor
    {
        public void AttachValidators(StringValidatorAttribute validator, object control)
        {
            FieldHelper.SetPropertyValue(control, "MaxLength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));
        }

        public void AttachValidators(DateValidatorAttribute validator, object control)
        {
            if (validator.MinimumDate > DateTime.MinValue)
                FieldHelper.SetPropertyValue(control, "MinDate", validator.MinimumDate.ToString(CultureInfo.InvariantCulture));
        }

        public void AttachValidators(IntValidatorAttribute validator, object control)
        {

        }

        public void AttachValidators(DoubleValidatorAttribute validator, object control)
        {

        }

        public void AttachValidators(EmailValidatorAttribute validator, object control)
        {
            FieldHelper.SetPropertyValue(control, "MaxLength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));
        }
    }
}
