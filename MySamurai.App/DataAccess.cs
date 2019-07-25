using System;
using System.Collections.Generic;
using System.Text;
using MySamurai.Domain.ClassLibrary;
using MySamurai.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            for (int i = 0; i < battles.Count; i++)
            {
                battlelogs.Add(new BattleLog
                {
                    Name = ("Log " + rnd.Next(0, 999999)),
                    BattleId = battles[i].Id
                });
            }
            context.AddRange(battlelogs);
            for (int i = 0; i < battlelogs.Count; i++)
            {
                battleevents.Add(new BattleEvent
                {
                    Order = i,
                    Summary = ("Summary " + rnd.Next(0, 999999)),
                    Description = ("Description " + rnd.Next(0, 999999)),
                    BattleLogId = battlelogs[i].Id
                });
            }
            context.AddRange(battleevents);
            SaveChanges();
        }
    }
}
