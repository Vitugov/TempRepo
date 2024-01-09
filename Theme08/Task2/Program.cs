namespace Task2
{
    public class Program
    {
        public static Dictionary<string, string> TelephoneBook { get; } = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            InputContacts();
            SearchContatct();
            Console.WriteLine("The program is completed. To continue, press any key.");
            Console.ReadKey();
        }

        public static void SearchContatct()
        {
            string telephoneNumber;

            while (true)
            {
                telephoneNumber = InputString("Input telephone number to search: ");
                if (telephoneNumber == "")
                    break;
                if (TelephoneBook.ContainsKey(telephoneNumber))
                    Console.WriteLine("This telephone number belongs to {0}.", TelephoneBook[telephoneNumber]);
                else
                    Console.WriteLine("Contact wasn't finded");
            }
        }

        public static void InputContacts()
        {
            var pair = new KeyValuePair<string, string>();
            
            while (true)
            {
                pair = InputContact();
                if (pair.Key == "")
                    break;
                TelephoneBook[pair.Key] = pair.Value;
            }
        }
        public static KeyValuePair<string, string> InputContact()
        {
            var key = InputString("Input telephone number:");
            if (key == "")
                return new KeyValuePair<string, string>("", "");
            var value = InputString("Input full name:");
            return new KeyValuePair<string, string>(key, value);
        }

        private static string InputString(string text)
        {
            Console.WriteLine(text);
            var value = Console.ReadLine();
            return value != null ? value : "";
        }
    }
}
