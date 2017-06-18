using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScissorValidations
{
    public interface IValidatorAttribute
    {
        String FieldName { get; set; }
        Boolean IsRequired { get; set; }

        List<Validation> Validate(PropertyInfo property, String value);
    }
}