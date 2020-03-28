using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Models
{
    [Serializable]
    public class LoginModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [JsonIgnore]
        public EmployeeModel Employee { get; set; }
    }
}
