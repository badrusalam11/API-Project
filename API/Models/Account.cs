﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }

        //[Required]
        [JsonIgnore]
        public string Password { get; set; }

        public int? OTP { get; set; }
        public DateTime? ExpiredToken { get; set; }
        public bool? IsUsed { get; set; }


        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public virtual Profiling Profiling { get; set; }

        //[JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles{ get; set; }

    }
}
