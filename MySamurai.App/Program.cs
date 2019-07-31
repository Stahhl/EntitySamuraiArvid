using System;
using System.Linq;

namespace MySamurai.App
{
    class Program
    {
        static DataAccess dataAccess;
        static void Main(string[] args)
        {
            dataAccess = new DataAccess();
            dataAccess.Init();

            dataAccess.ClearDatabase();

            //dataAccess.AddOneSamurai();
            //dataAccess.AddSomeSamurais();
            dataAccess.AddSomeBattles();
            for (int i = 0; i < 5; i++)
            {
                dataAccess.AddOneSamuraiWithRelatedData();
            }

            Run();
        }
        static void Run()
        {
            //ListAllSamuraiNames();
            //FindSamuraiWithRealName();
            //ListAllQuotes();
            ListAllBattles();

            Console.WriteLine("DONE");
        }
        static void ListAllBattles()
        {
            var battles = dataAccess.GetAllBattles();

            foreach (var b in battles)
            {
                string startDate = b.StartDate.Year.ToString();
                string endDate = b.EndDate.Year.ToString();

                if(b.IsBrutal == true)
                {
                    Console.WriteLine($"{b.Name} is a brutal battle in the period {startDate} to {endDate}");
                }
                if (b.IsBrutal == false)
                {
                    Console.WriteLine($"{b.Name} is a non brutal battle in the period {startDate} to {endDate}");
                }
            }
        }
        static void ListAllQuotes()
        {
            var quotes = dataAccess.GettAllQuotes();

            foreach (var q in quotes)
            {
                Console.WriteLine($"{q.Text} is a {q.Style} quote by {q.Samurai.Name}");
            }
        }
        static void FindSamuraiWithRealName()
        {
            var samurais = dataAccess.GetAllSamurais();

            Console.WriteLine("\nEnter secret identity: ");
            string input = Console.ReadLine().ToUpper();

            var hit = samurais.FirstOrDefault(x => x.SecretIdentity.RealName.ToUpper().Contains(input));

            if(hit != null)
            {
                Console.WriteLine("Hit!");
            }
            else
            {
                Console.WriteLine("Miss!");
            }
        }
        static void ListAllSamuraiNames()
        {
            var samurais = dataAccess.GetAllSamurais().OrderByDescending(x => x.Name);

            foreach (var s in samurais)
            {
                Write($"{s.Id} Name: {s.Name} Quotes: {s.Quotes.Count} Battles: {s.SamuraiBattles.Count} SI: {s.SecretIdentity.RealName}");
                Console.WriteLine();
            }
        }
        static void Write(object obj)
        {
            Console.Write(obj);
        }
    }
}
