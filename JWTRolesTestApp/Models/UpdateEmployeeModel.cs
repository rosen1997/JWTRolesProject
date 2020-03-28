using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Models
{
    public class UpdateEmployeeModel
    {
        [Required]
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string RoleDescription { get; set; }

    }
}
