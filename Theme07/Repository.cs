
namespace Theme07
{
    class Repository
    {
        private const string FileName = "employees.txt";

        public Worker[] GetAllWorkers()
        {
            if (File.Exists(FileName))
            {
                List<Worker> workers = new List<Worker>();
                string[] lines = File.ReadAllLines(FileName);

                foreach (string line in lines)
                {
                    Worker worker = ParseWorker(line);
                    workers.Add(worker);
                }

                return workers.ToArray();
            }

            return new Worker[0];
        }

        public Worker GetWorkerById(int id)
        {
            if (File.Exists(FileName))
            {
                string[] lines = File.ReadAllLines(FileName);

                foreach (string line in lines)
                {
                    Worker worker = ParseWorker(line);
                    if (worker.Id == id)
                    {
                        return worker;
                    }
                }
            }

            return new Worker();
        }

        public void DeleteWorker(int id)
        {
            if (File.Exists(FileName))
            {
                List<Worker> workers = new List<Worker>();
                string[] lines = File.ReadAllLines(FileName);
                bool found = false;

                foreach (string line in lines)
                {
                    Worker worker = ParseWorker(line);
                    if (worker.Id != id)
                    {
                        workers.Add(worker);
                    }
                    else
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    File.WriteAllLines(FileName, workers.Select(w => FormatWorker(w)).ToArray());
                }
            }
        }

        public void AddWorker(Worker worker)
        {
            int nextId = GetNextId();
            worker.Id = nextId;

            string workerData = FormatWorker(worker);

            using (StreamWriter writer = File.AppendText(FileName))
            {
                writer.WriteLine(workerData);
            }
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            if (File.Exists(FileName))
            {
                List<Worker> workers = new List<Worker>();
                string[] lines = File.ReadAllLines(FileName);

                foreach (string line in lines)
                {
                    Worker worker = ParseWorker(line);
                    if (worker.DateAdded >= dateFrom && worker.DateAdded <= dateTo)
                    {
                        workers.Add(worker);
                    }
                }

                return workers.ToArray();
            }

            return new Worker[0];
        }

        private int GetNextId()
        {
            int nextId = 1;

            if (File.Exists(FileName))
            {
                string[] lines = File.ReadAllLines(FileName);

                foreach (string line in lines)
                {
                    Worker worker = ParseWorker(line);
                    if (worker.Id >= nextId)
                    {
                        nextId = (int)worker.Id + 1;
                    }
                }
            }

            return nextId;
        }

        private Worker ParseWorker(string line)
        {
            string[] data = line.Split('#');

            Worker worker = new Worker();
            worker.Id = int.Parse(data[0]);
            worker.DateAdded = DateTime.ParseExact(data[1], "dd.MM.yyyy HH:mm", null);
            worker.FullName = data[2];
            worker.Age = int.Parse(data[3]);
            worker.HeightInCm = int.Parse(data[4]);
            worker.BirthDate = DateOnly.Parse(data[5]);
            worker.BirthPlace = data[6];

            return worker;
        }

        private string FormatWorker(Worker worker)
        {
            string formattedDate = worker.DateAdded.ToString("dd.MM.yyyy HH:mm");
            string workerData = $"{worker.Id}#{formattedDate}#{worker.FullName}#{worker.Age}#{worker.HeightInCm}#{worker.BirthDate}#{worker.BirthPlace}";
            return workerData;
        }
    }
}
