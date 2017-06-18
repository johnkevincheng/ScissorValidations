using System.Web.UI.WebControls;

namespace ScissorValidations
{
    public interface IValidationImplementor
    {
        void AttachValidators(StringValidatorAttribute validator, WebControl control);

        void AttachValidators(DateValidatorAttribute validator, WebControl control);

        void AttachValidators(IntValidatorAttribute validator, WebControl control);

        void AttachValidators(EmailValidatorAttribute validator, WebControl control);
    }
}