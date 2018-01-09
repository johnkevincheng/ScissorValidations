using System;
using System.Collections.Generic;
using ScissorsTest.Entities;
using RockFluid.ScissorValidations.ValidationImplementors;

namespace ScissorsTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var fieldMappings = new Dictionary<String, Object>
                                {
                                    {"FirstName", firstNameInput},
                                    {"MiddleName", middleNameInput},
                                    {"LastName", lastNameInput},
                                    {"Email", emailInput},
                                    {"DateEmployed", employmentDateInput},
                                    {"YearsExperience", yearsExperienceInput},
                                    {"Rating", ratingInput}
                                };
            RockFluid.ScissorValidations.Validator.InitializeClientValidators<Employee, BootstrapValidationImplementor>(fieldMappings);
            //ScissorValidations.Validator.InitializeClientValidators<Employee>(fieldMappings);
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            errorsList.DataSource = null;
            errorsList.DataBind();

            var p = new Employee();
            
            var fieldMappings = new Dictionary<String, String>
                                {
                                    {"FirstName", firstNameInput.Text},
                                    {"MiddleName", middleNameInput.Text},
                                    {"LastName", lastNameInput.Text},
                                    {"Email", emailInput.Text},
                                    {"DateEmployed", employmentDateInput.Text},
                                    {"YearsExperience", yearsExperienceInput.Text},
                                    {"Rating", ratingInput.Text}
                                };

            var validation = RockFluid.ScissorValidations.Validator.Validate(p, fieldMappings);

            if (validation.Failed)
            {
                errorsList.DataSource = validation.Findings;
                errorsList.DataBind();
            }
        }


    }
}
