using System;
using ScissorValidations.Validators;

namespace ScissorValidations.ValidationImplementors
{
    public interface IValidationImplementor
    {
        void AttachValidators(StringValidatorAttribute validator, Object control);

        void AttachValidators(DateValidatorAttribute validator, Object control);

        void AttachValidators(IntValidatorAttribute validator, Object control);

        void AttachValidators(DoubleValidatorAttribute validator, Object control);

        void AttachValidators(EmailValidatorAttribute validator, Object control);
    }
}