using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }
        public int RoleId { get; set; }
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual  Role Role { get; set; }
        [JsonIgnore]
        public virtual  Account Account { get; set; }
    }
}
