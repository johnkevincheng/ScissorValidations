using System;
using System.Collections.Generic;
using System.Globalization;
using ScissorValidations.Validators;

namespace ScissorValidations.ValidationImplementors
{
    public class WinFormsValidationImplmentor : IValidationImplementor
    {
        public void AttachValidators(List<IValidatorAttribute> validators, object control)
        {
            foreach (var validator in validators)
            {
                if (validator is StringValidatorAttribute)
                    AttachValidators(validator as StringValidatorAttribute, control);
                else if (validator is DateValidatorAttribute)
                    AttachValidators(validator as DateValidatorAttribute, control);
                else if (validator is IntValidatorAttribute)
                    AttachValidators(validator as IntValidatorAttribute, control);
                else if (validator is DoubleValidatorAttribute)
                    AttachValidators(validator as DoubleValidatorAttribute, control);
                else if (validator is EmailValidatorAttribute)
                    AttachValidators(validator as EmailValidatorAttribute, control);
            }
        }

        private void AttachValidators(StringValidatorAttribute validator, object control)
        {
            FieldHelper.SetPropertyValue(control, "MaxLength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));
        }

        private void AttachValidators(DateValidatorAttribute validator, object control)
        {
            if (validator.MinimumDate > DateTime.MinValue)
                FieldHelper.SetPropertyValue(control, "MinDate", validator.MinimumDate.ToString(CultureInfo.InvariantCulture));
        }

        private void AttachValidators(IntValidatorAttribute validator, object control)
        {

        }

        private void AttachValidators(DoubleValidatorAttribute validator, object control)
        {

        }

        private void AttachValidators(EmailValidatorAttribute validator, object control)
        {
            FieldHelper.SetPropertyValue(control, "MaxLength", validator.MaxSize.ToString(CultureInfo.InvariantCulture));
        }
    }
}
