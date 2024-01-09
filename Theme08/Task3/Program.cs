namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> set = new HashSet<string>();
            
            while (true)
            {
                var input = InputString("Input number: ");
                if (input == "")
                    break;
                if (set.Contains(input))
                    Console.WriteLine("Number was inputed earlier.");
                else
                {
                    set.Add(input);
                    Console.WriteLine("Number was added to set.");
                }
            }
            Console.WriteLine("The program is completed. To continue, press any key.");
            Console.ReadLine();
        }

        private static string InputString(string text)
        {
            Console.WriteLine(text);
            var value = Console.ReadLine();
            return value != null ? value : "";
        }
    }
}
