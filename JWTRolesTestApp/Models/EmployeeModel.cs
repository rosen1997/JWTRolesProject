using JWTRolesTestApp.Repository.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Models
{
    [Serializable]
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public RoleModel Role { get; set; }

        [JsonIgnore]
        public string Token { get; set; }
    }
}
