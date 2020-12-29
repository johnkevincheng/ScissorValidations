using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RockFluid.ScissorValidations.ValidationImplementors;

namespace ScissorsTestWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            var p = new Employee();

            var fieldMappings = new Dictionary<String, String>
                                {
                                    {"FirstName", firstNameInput.Text},
                                    {"MiddleName", middleNameInput.Text},
                                    {"LastName", lastNameInput.Text},
                                    {"Email", emailInput.Text},
                                    {"DateEmployed", employmentDateInput.Text},
                                    {"YearsExperience", yearsOfExperienceInput.Text},
                                    {"Rating", ratingInput.Text}
                                };

            var validation = RockFluid.ScissorValidations.Validator.Validate(p, fieldMappings);

            if (validation.Failed)
                errorList.DataSource = validation.Findings;
            else
                MessageBox.Show("Validtion Passed");
        }

        private void submitObjectButton_Click(object sender, EventArgs e)
        {
            var p = new Employee()
            {
                FirstName = firstNameInput.Text,
                MiddleName = middleNameInput.Text,
                LastName = lastNameInput.Text,
                Email = emailInput.Text,
                DateEmployed = DateTime.TryParse(employmentDateInput.Text, out var d) ? d : new DateTime(),
                YearsExperience = short.TryParse(yearsOfExperienceInput.Text, out short y) ? y : (short)0,
                Rating = float.TryParse(ratingInput.Text, out var r) ? r : 0
            };

            var validation = RockFluid.ScissorValidations.Validator.Validate(p);

            if (validation.Failed)
                errorList.DataSource = validation.Findings;
            else
                MessageBox.Show("Validtion Passed");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var fieldMappings = new Dictionary<String, Object>
                                {
                                    {"FirstName", firstNameInput},
                                    {"MiddleName", middleNameInput},
                                    {"LastName", lastNameInput},
                                    {"Email", emailInput},
                                    {"DateEmployed", employmentDateInput},
                                    {"YearsExperience", yearsOfExperienceInput},
                                    {"Rating", ratingInput}
                                };
            RockFluid.ScissorValidations.Validator.InitializeClientValidators<Employee, ExtendedWinFormsValidationImplementor>(fieldMappings);
        }
    }
}
