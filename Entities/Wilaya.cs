using System;
using System.Collections.Generic;

namespace RelationShipManager.Entities
{
    public partial class Wilaya
    {
        public Wilaya()
        {
            Commune = new HashSet<Commune>();
        }

        public int IdWilaya { get; set; }
        public string Wilaya1 { get; set; }

        public ICollection<Commune> Commune { get; set; }
    }
}
