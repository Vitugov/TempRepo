using System.IO;
using System.Xml.Linq;

namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xmlDoc = CreateXMLDocument();
            xmlDoc.Save("contact.xml");
        }

        static private XDocument CreateXMLDocument()
        {
            XElement contact = new XElement("Person",
                new XAttribute("name", GetStringFromUser("Введите ФИО: ")),
                new XElement("Address",
                    new XElement("Street", GetStringFromUser("Введите название улицы: ")),
                    new XElement("HouseNumber", GetStringFromUser("Введите номер дома: ")),
                    new XElement("FlatNumber", GetStringFromUser("Введите номер квартиры: "))),
                new XElement("Phones",
                    new XElement("MobilePhone", GetStringFromUser("Введите мобильный телефон: ")),
                    new XElement("FlatPhone", GetStringFromUser("Введите домашний телефон: "))));

            return new XDocument(contact);
        }

        static private string GetStringFromUser(string prompt)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            return value != null ? value : "";
        }
    }
}
