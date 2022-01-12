using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
        [Key]
        //[Required(ErrorMessage = "{0} is a mandatory field")]
        public int UniversityId { get; set; }

        //[Required(ErrorMessage = "{0} is a mandatory field")]
        //[StringLength(maximumLength: 20, MinimumLength = 1,
        //ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }
}
