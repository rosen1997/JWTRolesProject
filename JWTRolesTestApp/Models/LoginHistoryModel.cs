using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Models
{
    [Serializable]
    public class LoginHistoryModel
    {
        public int Id { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime LogoutTime { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
