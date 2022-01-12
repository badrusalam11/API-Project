using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class RegisterVM
    {
        public string NIK { get; set; }

        //[Required(ErrorMessage = "{0} is a mandatory field")]
        //[StringLength(maximumLength: 50, MinimumLength = 1,
        //ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[Required(ErrorMessage = "{0} is a mandatory field")]
        //[Phone]
        public string Phone { get; set; }

    //    [Required]
    //    [Range(typeof(DateTime), "01/01/1900", "01/01/2014",
    //ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]
        public DateTime BirthDate { get; set; }

        //[Required]
        //[Range(0, 9999999, ErrorMessage = "Salary out of range")]
        public int Salary { get; set; }

        //[Required]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required]
        //[Range(0, 1, ErrorMessage = "Wrong input")]
        public Gender Gender { get; set; }

        //[Required]
        //[StringLength(maximumLength: 100, MinimumLength = 8,
        //ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Password { get; set; }

        //[Required]
        //[StringLength(maximumLength: 20, MinimumLength = 1,
        //ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Degree { get; set; }

        //[Required]
        //[Range(0, 4, ErrorMessage = "GPA Invalid input")]
        public decimal GPA { get; set; }

        //[Required(ErrorMessage = "{0} is a mandatory field")]
        public int UniversityId { get; set; }

        //[Required(ErrorMessage = "{0} is a mandatory field")]
        public int EducationId { get; set; }

    

    }


}
