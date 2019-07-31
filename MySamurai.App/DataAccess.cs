using System;
using System.Collections.Generic;
using System.Text;
using MySamurai.Domain.ClassLibrary;
using MySamurai.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MySamurai.App
{
    class DataAccess
    {
        SamuraiContext context;
        Random rnd;

        internal void Init()
        {
            context = new SamuraiContext();
            rnd = new Random();
        }

        internal void AddOneSamurai()
        {
            var samurai = new Samurai { Name = "Zelda" };

            context.Samurais.Add(samurai);
        }
        internal void SaveChanges()
        {
            context.SaveChanges();
        }
        internal void ClearChanges()
        {
            var changedEntries = context.ChangeTracker.Entries()
            .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
        internal List<Battle> GetAllBattles()
        {
            return context.Battles
                .Include(x => x.BattleLog)
                .ToList();
        }
        internal List<Quote> GettAllQuotes()
        {
            return context.Quotes
                .Include(x => x.Samurai)
                .ToList();
        }
        internal void AddSomeSamurais()
        {
            var samurais = new List<Samurai>();
            for (int i = 0; i < 5; i++)
            {
                samurais.Add(new Samurai { Name = "Samurai_" + rnd.Next() });
            }
            context.Samurais.AddRange(samurais);
            SaveChanges();
        }
        internal List<Samurai> GetAllSamurais()
        {
            return context.Samurais
                .Include(x => x.Quotes)
                .Include(x => x.SecretIdentity)
                .ToList();
        }
        internal void AddSomeBattles()
        {
            var battles = new List<Battle>();
            var battlelogs = new List<BattleLog>();
            var battleevents = new List<BattleEvent>();

            //Add 5 new battles
            for (int i = 0; i < 5; i++)
            {
                bool wasBrutal = rnd.Next(0, 2) == 0 ? false : true;

                battles.Add(new Battle
                {
                    Name = ("Battle " + rnd.Next(0, 999999)),
                    Description = ("Description " + rnd.Next(0, 999999)),
                    IsBrutal = wasBrutal,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                });
            }
            context.AddRange(battles);
            //Add 1 battlelog per battle
            for (int i = 0; i < battles.Count; i++)
            {
                battlelogs.Add(new BattleLog
                {
                    Name = ("Log " + rnd.Next(0, 999999)),
                    BattleId = battles[i].Id
                });
            }
            context.AddRange(battlelogs);
            //add 3 battleevents per battlelog
            for (int i = 0; i < battlelogs.Count; i++)
            {
                for (int a = 0; a < 3; a++)
                {
                    battleevents.Add(new BattleEvent
                    {
                        Order = (a + 1),
                        Summary = ("Summary " + rnd.Next(0, 999999)),
                        Description = ("Description " + rnd.Next(0, 999999)),
                        BattleLogId = battlelogs[i].Id
                    });
                }
            }
            context.AddRange(battleevents);
            SaveChanges();
        }
        internal void AddOneSamuraiWithRelatedData()
        {
            var sQuotes = new List<Quote>();
            var sBattles = new List<SamuraiBattle>();

            int nbQuotes = rnd.Next(1, 4);
            int nbBattles = rnd.Next(1, 4); ;

            var samurai = new Samurai
            {
                Name = ("Samurai_" + rnd.Next(100000, 1000000)),
                HairStyle = GetRandomHairStyle()
            };
            var secretidentity = new SecretIdentity
            {
                RealName = ("Realname_" + rnd.Next(100000, 1000000)),
                Samurai = samurai
            };

            for (int i = 1; i < (nbQuotes + 1); i++)
            {
                sQuotes.Add(new Quote
                {
                    Text = ("Text " + rnd.Next(100000, 1000000)),
                    Samurai = samurai,
                    Style = GetRandomQuoteStyle()
                });
            }
            for (int i = 1; i < (nbBattles + 1); i++)
            {
                sBattles.Add(new SamuraiBattle
                {
                    Battle = GetRandomBattle(),
                    Samurai = samurai
                });
            }



            context.Add(samurai);
            context.Add(secretidentity);
            context.AddRange(sQuotes);
            context.AddRange(sBattles);
            //context.Add(sb);

            //context.AddRange(samuraiBattles);
            SaveChanges();

            //AddSamuraiBattles(samurai);
        }
        internal void AddSamuraiBattles(Samurai samurai)
        {
            int nbBattles = rnd.Next(1, 4);

            for (int i = 0; i < nbBattles; i++)
            {
                var sb = new SamuraiBattle
                {
                    Battle = GetRandomBattle(),
                    Samurai = samurai
                };

                samurai.SamuraiBattles.Add(sb);

                context.Add(sb);

                SaveChanges();
            }
        }
        internal void ClearDatabase()
        {
            // För att ta bort ett objekt måste du först göra en query
            context.RemoveRange(context.Samurais);
            context.RemoveRange(context.Battles);
            context.RemoveRange(context.Quotes);

            ReseedAllTables();
            SaveChanges();
        }
        internal void ReseedAllTables()
        {
            // Nollställer alla nycklar
            // Kopplingstabellen SamuraiBattles behöver inte nollställas
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Samurais', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('SecretIdentity', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Quotes', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Battles', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('BattleLog', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('BattleEvent', RESEED, 0)");
        }
        internal HairStyle GetRandomHairStyle()
        {
            var values = Enum.GetValues(typeof(HairStyle));
            return (HairStyle)values.GetValue(rnd.Next(values.Length));
        }
        internal QuoteStyle GetRandomQuoteStyle()
        {
            var values = Enum.GetValues(typeof(QuoteStyle));
            return (QuoteStyle)values.GetValue(rnd.Next(values.Length));
        }
        internal Battle GetRandomBattle()
        {
            var battles = context.Battles.ToList();
            return battles[rnd.Next(battles.Count)];
        }
    }
}
