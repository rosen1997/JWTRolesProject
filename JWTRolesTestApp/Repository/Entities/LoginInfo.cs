using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Entities
{
    [Serializable]
    [Table("LoginInfos")]
    public class LoginInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(64)]
        [Required]
        public string Username { get; set; }

        [MaxLength(64)]
        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }
    }
}
