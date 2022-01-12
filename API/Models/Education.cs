using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Required]
        //[StringLength(maximumLength: 20, MinimumLength = 1,
        //ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Degree { get; set; }

        [Required]
        //[Range(0, 4, ErrorMessage = "GPA Invalid input")]
        public decimal GPA { get; set; }

        public virtual University University { get; set; }
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }
    }
}
