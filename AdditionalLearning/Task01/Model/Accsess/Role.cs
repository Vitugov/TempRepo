using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class Role
    {
        public string Name { get; set; }
        public Dictionary<Type, Dictionary<string, Permission>> AccessRules { get; set; }

        public Role(string name)
        {
            Name = name;
            AccessRules = new Dictionary<Type, Dictionary<string, Permission>>();
        }
        public Dictionary<string, Permission> this[Type type]
        {
            get
            {
                if (!AccessRules.ContainsKey(type))
                {
                    throw new ArgumentException("There is no access rules for this type");
                }
                return AccessRules[type];
            }
        }
        public List<string> GetChangeableProperties(Type type)
        {
            var result = AccessRules[type]
                .Where((dic)=>dic.Value.Write)
                .Select((dic)=>dic.Key).ToList();
            return result;
        }

        public bool IsChangeable(Type type, string property)
        {
            var result = AccessRules[type][property].Write;
            return result;
        }

        public bool CanAddNew(Type type)
        {
            var result = AccessRules[type].First().Value.Create;
            return result;
        }
    }
}
