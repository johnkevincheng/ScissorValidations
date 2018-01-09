using System;
using System.Collections.Generic;
using RockFluid.ScissorValidations.Validators;

namespace RockFluid.ScissorValidations.ValidationImplementors
{
    public interface IValidationImplementor
    {
        void AttachValidators(List<IValidatorAttribute> validators, Object control);
    }
}