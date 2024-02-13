using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class Validation
    {
        public static Dictionary<Type, Validation> GeneralDic { get; set; }
        public Type Type { get; set; }
        public Dictionary<string, List<(Func<string, bool>, string)>> TypeRulesDic {get; set;}

        static Validation()
        {
            GeneralDic = [];
            var clientValidation = new Validation(typeof(Client));
            GeneralDic[typeof(Client)] = clientValidation;
            clientValidation.TypeRulesDic["Surname"] = new List<(Func<string, bool>, string)>
                { { ((item) => item.Length > 0, "Фамилия не может быть пустой.") } };
            clientValidation.TypeRulesDic["Name"] = new List<(Func<string, bool>, string)>
                { { ((item) => item.Length > 0, "Имя не может быть пустым.") } };
            clientValidation.TypeRulesDic["Patronymic"] = new List<(Func<string, bool>, string)>
                { { ((item) => item.Length > 0, "Отчество не может быть пустым.") } };
            clientValidation.TypeRulesDic["TelephoneNumber"] = new List<(Func<string, bool>, string)>
                { { ((item) => item.Length >= 12, "Номер телефона не может содержать менее 12 знаков.") },
                  { ((item) => item.Contains('+'), "Номер телефона должен содержать '+'.") }};
            clientValidation.TypeRulesDic["PassportSeriesNumber"] = new List<(Func<string, bool>, string)>
                { { ((item) => item.Length == 11, "Номер паспорта должен содержать 10 цифр и пробел между серией и номером паспорта.") },
                  { ((item) => item.Contains(' '), "Номер паспорта должен содержать пробел между серией и номером паспорта.") }};
        }

        public Validation(Type type)
        {
            Type = type;
            TypeRulesDic = [];
        }

        public static bool Validate(ExpandoObject obj, Type type, Role role, out List<string> validationErrors)
        {
            var validate = true;
            validationErrors = [];

            var dic = GeneralDic[type].TypeRulesDic;
            foreach (var property in dic.Keys)
            {
                if (!Validate(obj, type, property, role, out var validationPropErrors))
                {
                    validationErrors.AddRange(validationPropErrors);
                    validate = false;
                }
            }
            return validate;
        }

        public static bool Validate(ExpandoObject obj, Type type, string property, Role role, out List<string> validationErrors)
        {
            var validate = true;
            validationErrors = [];
            
            var dic = GeneralDic[type].TypeRulesDic;
            if (!dic.ContainsKey(property) || !role.IsChangeable(type, property))
                return true;

            var objAsDic = obj as IDictionary<string, object>;
            var value = objAsDic[property] as string;

            foreach (var rule in dic[property])
            { 
                if (!rule.Item1(value))
                {
                    validationErrors.Add(rule.Item2);
                    validate = false;
                }
            }
            return validate;
        }
    }
}
