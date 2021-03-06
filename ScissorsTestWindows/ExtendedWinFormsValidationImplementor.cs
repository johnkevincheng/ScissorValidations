﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using RockFluid.ScissorValidations;
using RockFluid.ScissorValidations.ValidationImplementors;
using RockFluid.ScissorValidations.Validators;

namespace ScissorsTestWindows
{
    public class ExtendedWinFormsValidationImplementor : WinFormsValidationImplmentor, IValidationImplementor
    {
        public new void AttachValidators(List<IValidatorAttribute> validators, object control)
        {
            base.AttachValidators(validators, control);

            foreach (var validator in validators)
            {
                if (validator is TestValidatorAttribute)
                {
                    FieldHelper.SetPropertyValue(control, "Text", "TEST");
                }
            }
        }
    }
}
