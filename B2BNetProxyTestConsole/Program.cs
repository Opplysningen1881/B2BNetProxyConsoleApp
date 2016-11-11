using System;

namespace B2BNetProxyTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //string query = String.Join(" ", args);

            Console.WriteLine("What are you searching for? ");

            string query = Console.ReadLine();

            search s = new search();

            Console.WriteLine(string.Format("Query: {0}", s.doSearch(query)));

            Console.Read();
        }
    }
}
