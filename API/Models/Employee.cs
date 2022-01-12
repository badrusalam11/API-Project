using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        [Key]
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
       
        public virtual Account Account { get; set; }

    }
    public enum Gender
    {
        Male,
        Female
    }

}
