using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Accsess
{
    public static class UserSets
    {
        public static List<User> Users { get; }
        static UserSets()
        {
            Users = [];
            Users.Add(new User("Балаганов Олег Васильевич", RoleSets.Manager));
            Users.Add(new User("Антропов Александр Евгеньевич", RoleSets.Consultant));
        }
        public static Dictionary<string, User> GetUserList()
        {
            var userDic = new Dictionary<string, User>();
            foreach (var user in Users)
            {
                userDic[user.ToString()] = user;
            }
            return userDic;
        }
    }
}
