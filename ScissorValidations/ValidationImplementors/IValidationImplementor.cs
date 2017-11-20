using System;
using System.Collections.Generic;
using ScissorValidations.Validators;

namespace ScissorValidations.ValidationImplementors
{
    public interface IValidationImplementor
    {
        void AttachValidators(List<IValidatorAttribute> validators, Object control);
    }
}