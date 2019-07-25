using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MySamurai.Domain.ClassLibrary
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public HairStyle? HairStyle { get; set; }

        public List<Quote> Quotes { get; set; }
        //public SecretIdentity SecretIdentity { get; set; }

        //public List<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
