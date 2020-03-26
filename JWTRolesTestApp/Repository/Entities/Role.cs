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
    [Table("Roles")]
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(64)]
        [Required]
        public string RoleDescription { get; set; }

        [JsonIgnore]
        public IEnumerable<Employee> Employees { get; set; }
    }
}
