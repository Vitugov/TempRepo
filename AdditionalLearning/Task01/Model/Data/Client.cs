using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;

namespace Task01.Model.Data
{
    public class Client : IStoredData
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string TelephoneNumber { get; set; }
        public string PassportSeriesNumber { get; set; }

        public Client(string surname, string name, string patronymic, string telephoneNumber, string passportSeriesNumber)
            : base()
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            TelephoneNumber = telephoneNumber;
            PassportSeriesNumber = passportSeriesNumber;
        }

        public Client() : this("", "", "", "", "") {}
    }
}
