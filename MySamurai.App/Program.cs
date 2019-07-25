using System;

namespace MySamurai.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataAccess = new DataAccess();
            dataAccess.Init();

            //dataAccess.AddOneSamurai();
            //dataAccess.AddSomeSamurais();
            //dataAccess.AddSomeBattles();
            //ListAllSamurais(dataAccess);

            Console.WriteLine("DONE");
        }

        static void ListAllSamurais(DataAccess data)
        {
            var samurais = data.GetAllSamurais();

            foreach (var s in samurais)
            {
                Write(s.Name);
                Console.WriteLine();
            }
        }
        static void Write(object obj)
        {
            Console.Write(obj);
        }
    }
}
