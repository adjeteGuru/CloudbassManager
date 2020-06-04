using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts.Inputs.User
{
    public class UserModel
    {
        public UserModel(int id, int employeeId, string email, string name, string password, string salt, bool active, int tokenVersion)
        {
            Id = id;
            EmployeeId = employeeId;
            Email = email;
            Name = name;
            Password = password;
            Salt = salt;
            Active = active;
            TokenVersion = tokenVersion;
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; }
        public int TokenVersion { get; set; }
    }
}
