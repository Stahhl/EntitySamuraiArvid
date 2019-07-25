using System;
using System.Collections.Generic;
using System.Text;
using MySamurai.Domain.ClassLibrary;
using MySamurai.Data.Models;

namespace MySamurai.App
{
    class DataAccess
    {
        SamuraiContext context;

        internal void Init()
        {
            context = new SamuraiContext();
        }

        internal void AddOneSamurai()
        {
            var samurai = new Samurai { Name = "Zelda" };

            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
        internal void AddSomeSamurais()
        {

        }
    }
}
