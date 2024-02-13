using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Accsess
{
    public static class PermissionSets
    {
        public static Permission NoAccess { get; set; }
        public static Permission ReadOnly { get; }
        public static Permission ReadAndWrite { get; }
        public static Permission FullRights { get; }

        static PermissionSets()
        {
            NoAccess = new Permission();
            ReadOnly = new Permission(true);
            ReadAndWrite = new Permission(true, true);
            FullRights = new Permission(true, true, true);
        }
    }
}
