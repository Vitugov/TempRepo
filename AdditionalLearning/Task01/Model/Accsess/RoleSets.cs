using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public static class RoleSets
    {
        public static Role Consultant {  get; set; }
        public static Role Manager { get; set; }

        static RoleSets()
        {
            Consultant = new Role("Consultant");
            
            var consultantClientPermissions = new Dictionary<string, Permission>
            {
                { "Surname", PermissionSets.ReadOnly },
                { "Name", PermissionSets.ReadOnly },
                { "Patronymic", PermissionSets.ReadOnly },
                { "TelephoneNumber", PermissionSets.ReadAndWrite },
                { "PassportSeriesNumber", PermissionSets.NoAccess }
            };
            Consultant.AccessRules.Add(typeof(Client), consultantClientPermissions);

            var consultantEditPermissions = new Dictionary<string, Permission>
            {
                { "DateTime", PermissionSets.ReadOnly },
                { "ChangedData", PermissionSets.ReadOnly },
                { "TypeOfChanges", PermissionSets.ReadOnly },
                { "Author", PermissionSets.ReadOnly },
            };
            Consultant.AccessRules.Add(typeof(Edit), consultantEditPermissions);

            
            Manager = new Role("Manager");
            var ManagerClientPermissions = new Dictionary<string, Permission>
            {
                { "Surname", PermissionSets.FullRights },
                { "Name", PermissionSets.FullRights },
                { "Patronymic", PermissionSets.FullRights },
                { "TelephoneNumber", PermissionSets.FullRights },
                { "PassportSeriesNumber", PermissionSets.FullRights }
            };
            Manager.AccessRules.Add(typeof(Client), ManagerClientPermissions);

            var managerEditPermissions = consultantEditPermissions;
            Manager.AccessRules.Add(typeof(Edit), managerEditPermissions);
        }
    }
}
