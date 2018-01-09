using System;
using RockFluid.ScissorValidations.Validators;

namespace ScissorsTest.Entities
{
    public class Employee
    {
        [StringValidator("First Name", 5, 50, IsRequired = true)]
        public String FirstName { get; set; }

        [StringValidator("Middle Name", 5, 20)]
        public String MiddleName { get; set; }

        [StringValidator("Last Name", 5, 20, IsRequired = true)]
        public String LastName { get; set; }

        [EmailValidator("Email", 5, 20, IsRequired = true)]
        public String Email { get; set; }

        [IntValidator("Years of Experience", 0, 30)]
        public Int16 YearsExperience { get; set; }

        [DoubleValidator("Rating", 0.0, 5.0, IsRequired = true)]
        public Single Rating { get; set; }

        [DateValidator("Date Employed")]
        public DateTime DateEmployed { get; set; }
    }
}