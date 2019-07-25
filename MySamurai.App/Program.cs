using System;

namespace MySamurai.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataAccess = new DataAccess();
            dataAccess.Init();
        }
    }
}
