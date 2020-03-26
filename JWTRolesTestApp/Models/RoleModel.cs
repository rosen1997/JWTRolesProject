using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Models
{
    [Serializable]
    public class RoleModel
    {
        public int Id { get; set; }
        public string RoleDescription { get; set; }
    }
}
