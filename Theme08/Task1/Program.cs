
namespace Task1
{
    public class Program
    {
        private static int listLength { get; } = 100;
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            AddRandomToList(list, listLength, 0, 100);
            PrintList(list);
            FilterList(list, 25, 50);
            PrintList(list);
        }

        /// <summary>
        /// Adds to the list the number of values ​​equal to length which are random int values from minRnd to maxRnd
        /// </summary>
        /// <param name="list">The list which will be fill</param>
        /// <param name="length">Number of elements that will be add</param>
        /// <param name="minRnd">Minimum random number</param>
        /// <param name="maxRnd">Maximum random number (except that number)</param>
        private static void AddRandomToList(List<int> list, int length, int minRnd, int maxRnd)
        {
            var rnd = new Random();
            for (int i = 0; i < length; i++)
                list.Add(rnd.Next(minRnd, maxRnd));
            Console.WriteLine("Were generated list with {0} random elements.", list.Count);
        }

        /// <summary>
        /// Print the elements of the list on the screen
        /// </summary>
        /// <param name="list">List that will be printed</param>
        private static void PrintList(List<int> list)
        {
            Console.WriteLine("List elements:");
            foreach (int item in list)
                Console.Write(item + "\t");
            Console.WriteLine();
        }

        /// <summary>
        /// Filter the list deleting all elements that larger minValueToFilter and less than maxValueToFilter
        /// </summary>
        /// <param name="list">List to filter</param>
        /// <param name="minValueToFilter">Lower bound of range</param>
        /// <param name="maxValueToFilter">Upper bounf of range</param>
        private static void FilterList(List<int> list, int minValueToFilter, int maxValueToFilter)
        {
            var length = list.Count;
            for (int i = list.Count - 1; i >= 0 ; i--)
                if (list[i] > minValueToFilter && list[i] < maxValueToFilter)
                    list.RemoveAt(i);
            Console.WriteLine("Were deleted {0} elements.", length - list.Count);
        }
    }
}
