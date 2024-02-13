using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Accsess
{
    public class User
    {
        public string UserName { get; set; }
        public Role Role { get; set; }

        public User(string userName, Role role)
        {
            UserName = userName;
            Role = role;
        }
        public override string ToString()
        {
            return String.Format($"{UserName} ({Role.Name})");
        }
    }
}
