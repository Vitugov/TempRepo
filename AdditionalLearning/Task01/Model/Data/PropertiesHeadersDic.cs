using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Task01.Model.Data
{
    public static class PropertiesHeadersDic
    {
        public static Dictionary<Type, Dictionary<string, string>> Dic { get; set; }

        static PropertiesHeadersDic()
        {
            Dic = [];
            Dic.Add(typeof(Client), new Dictionary<string, string>
            {
                { "Surname", "Фамилия" },
                { "Name", "Имя"},
                { "Patronymic", "Отчество"},
                { "TelephoneNumber", "Номер телефона" },
                { "PassportSeriesNumber", "Паспорт" }
            });
        }

        public static Dictionary<string, string> GetHeaders(this Type type)
        {
            return Dic[type];
        }
    }
}
