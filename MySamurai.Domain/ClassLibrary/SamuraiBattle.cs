using System;
using System.Collections.Generic;
using System.Text;

namespace MySamurai.Domain.ClassLibrary
{
    public class SamuraiBattle
    {
        public int Id { get; set; }

        public Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }

        public Battle Battle { get; set; }
        public int BattleId { get; set; }
    }
}
