using System.Security.Cryptography.X509Certificates;

namespace Theme07
{
    class Program
    {
        static void Main()
        {
            Repository repository = new Repository();

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Вывести все записи");
                Console.WriteLine("2 - Вывести одну запись по ID");
                Console.WriteLine("3 - Добавить новую запись");
                Console.WriteLine("4 - Удалить запись по ID");
                Console.WriteLine("5 - Вывести записи в выбранном диапазоне дат");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DisplayAllWorkers(repository);
                        break;
                    case "2":
                        DisplayWorkerById(repository);
                        break;
                    case "3":
                        AddWorker(repository);
                        break;
                    case "4":
                        DeleteWorker(repository);
                        break;
                    case "5":
                        DisplayWorkersInDateRange(repository);
                        break;
                    default:
                        Console.WriteLine("Неверный ввод! Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void DisplayAllWorkers(Repository repository)
        {
            Worker[] workers = repository.GetAllWorkers();
            if (workers.Length > 0)
            {
                foreach (Worker worker in workers)
                {
                    DisplayWorker(worker);
                }
            }
            else
            {
                Console.WriteLine("Нет записей о сотрудниках.");
            }
        }

        static void DisplayWorkerById(Repository repository)
        {
            Console.Write("Введите ID записи: ");
            int id = int.Parse(Console.ReadLine());

            Worker worker = repository.GetWorkerById(id);
            if (worker.Id != null)
            {
                DisplayWorker(worker);
            }
            else
            {
                Console.WriteLine("Сотрудник с указанным ID не найден.");
            }
        }

        static void AddWorker(Repository repository)
        {
            Worker.indexer++;
            Worker worker = new Worker();
            worker.Id = Worker.indexer;
            worker.DateAdded = DateTime.Now;

            Console.Write("Ф. И. О.: ");
            worker.FullName = Console.ReadLine();

            Console.Write("Возраст: ");
            worker.Age = int.Parse(Console.ReadLine());

            Console.Write("Рост в сантиметрах: ");
            worker.HeightInCm = int.Parse(Console.ReadLine());

            Console.Write("Дата рождения: ");
            worker.BirthDate = DateOnly.Parse(Console.ReadLine());

            Console.Write("Место рождения: ");
            worker.BirthPlace = Console.ReadLine();

            repository.AddWorker(worker);
            Console.WriteLine("Новая запись успешно добавлена.");
            Console.WriteLine();
        }

        static void DeleteWorker(Repository repository)
        {
            Console.Write("Введите ID записи для удаления: ");
            int id = int.Parse(Console.ReadLine());

            repository.DeleteWorker(id);
            Console.WriteLine("Запись успешно удалена.");
            Console.WriteLine();
        }

        static void DisplayWorkersInDateRange(Repository repository)
        {
            Console.Write("Введите начальную дату (формат: dd.MM.yyyy): ");
            DateTime dateFrom = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

            Console.Write("Введите конечную дату (формат: dd.MM.yyyy): ");
            DateTime dateTo = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

            Worker[] workers = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
            if (workers.Length > 0)
            {
                foreach (Worker worker in workers)
                {
                    DisplayWorker(worker);
                }
            }
            else
            {
                Console.WriteLine("Нет записей о сотрудниках в указанном диапазоне дат.");
            }
        }

        static void DisplayWorker(Worker worker)
        {
            Console.WriteLine("ID: " + worker.Id);
            Console.WriteLine("Дата и время добавления: " + worker.DateAdded.ToString("dd.MM.yyyy HH:mm"));
            Console.WriteLine("Ф. И. О.: " + worker.FullName);
            Console.WriteLine("Возраст: " + worker.Age);
            Console.WriteLine("Рост: " + worker.HeightInCm);
            Console.WriteLine("Дата рождения: " + worker.BirthDate);
            Console.WriteLine("Место рождения: " + worker.BirthPlace);
            Console.WriteLine();
        }
    }
}
