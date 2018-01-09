using System;
using RockFluid.ScissorValidations.ValidationImplementors;

namespace RockFluid.ScissorValidations
{
    public class ScissorsSettings
    {
        internal ScissorsSettings()
        {

        }

        /// <summary>
        /// Gets or sets whether validating fields copies validated data to that field.
        /// </summary>
        public Boolean CopyValuesOnValidate { get; set; }

        /// <summary>
        /// Gets or sets the default client-validation implementor to use.
        /// </summary>
        public IValidationImplementor DefaultImplementor { get; set; }
    }
}