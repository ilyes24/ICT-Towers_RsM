using System.Collections.Generic;

namespace RelationShipManager.Entities
{
    public class Commune
    {
        public Commune()
        {
            MyUser = new HashSet<MyUser>();
        }

        public int IdCommune { get; set; }
        public int CodePostal { get; set; }
        public string Commune1 { get; set; }
        public int WilayaId { get; set; }

        public Wilaya Wilaya { get; set; }
        public ICollection<MyUser> MyUser { get; set; }
    }
}
