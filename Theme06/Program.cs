using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Вывести данные на экран");
            Console.WriteLine("2 - Добавить новую запись");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayEmployees();
                    break;
                case "2":
                    AddEmployee();
                    break;
                default:
                    Console.WriteLine("Неверный ввод! Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void DisplayEmployees()
    {
        if (File.Exists("employees.txt"))
        {
            string[] lines = File.ReadAllLines("employees.txt");
            foreach (string line in lines)
            {
                string[] data = line.Split('#');
                Console.WriteLine("ID: " + data[0]);
                Console.WriteLine("Дата и время добавления: " + data[1]);
                Console.WriteLine("Ф. И. О.: " + data[2]);
                Console.WriteLine("Возраст: " + data[3]);
                Console.WriteLine("Рост: " + data[4]);
                Console.WriteLine("Дата рождения: " + data[5]);
                Console.WriteLine("Место рождения: " + data[6]);
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Файл employees.txt не найден.");
        }
    }

    static void AddEmployee()
    {
        Console.WriteLine("Введите данные сотрудника:");

        Console.Write("ID: ");
        string id = Console.ReadLine();

        Console.Write("Ф. И. О.: ");
        string name = Console.ReadLine();

        Console.Write("Возраст: ");
        string age = Console.ReadLine();

        Console.Write("Рост: ");
        string height = Console.ReadLine();

        Console.Write("Дата рождения: ");
        string birthDate = Console.ReadLine();

        Console.Write("Место рождения: ");
        string birthPlace = Console.ReadLine();

        string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

        string employeeData = $"{id}#{dateTime}#{name}#{age}#{height}#{birthDate}#{birthPlace}";

        using (StreamWriter writer = File.AppendText("employees.txt"))
        {
            writer.WriteLine(employeeData);
        }

        Console.WriteLine("Новая запись успешно добавлена.");
        Console.WriteLine();
    }
}
