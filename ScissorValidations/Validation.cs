using System;

namespace ScissorValidations
{
    public class Validation
    {
        public Validation(String field, String message)
        {
            Field = field;
            Message = message;
        }

        public String Field { get; private set; }
        public String Message { get; private set; }
    }
}