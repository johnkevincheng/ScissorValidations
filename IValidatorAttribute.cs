using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScissorValidations
{
    public interface IValidatorAttribute
    {
        String FieldLabel { get; }
        Boolean IsRequired { get; set; }

        List<Validation> Validate<T>(T entity, PropertyInfo property, String value);
    }
}