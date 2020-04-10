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
    [Table("Employees")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(64)]
        [Required]
        public string MiddleName { get; set; }

        [MaxLength(64)]
        [Required]
        public string LastName { get; set; }

        public LoginInfo LoginInfo { get; set; }
        public AtWork AtWork { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [Required]
        public int RoleId { get; set; }

        [JsonIgnore]
        public string Token { get; set; }

    }
}
