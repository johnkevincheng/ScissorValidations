using System;
using System.Collections.Generic;

namespace RockFluid.ScissorValidations
{
    public class ValidationResult
    {
        /// <summary>
        /// Gets the list of validation errors found during the validation process.
        /// </summary>
        public List<Validation> Findings { get; private set; }

        /// <summary>
        /// Gets whether the validation process find any validation errors.
        /// </summary>
        public Boolean Failed
        {
            get
            {
                return Findings != null && Findings.Count > 0;
            }
        }

        public ValidationResult(List<Validation> findings)
        {
            Findings = findings;
        }
    }
}
