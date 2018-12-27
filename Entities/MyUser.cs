using System.Collections.Generic;

namespace RelationShipManager.Entities
{
    public class MyUser
    {
        public MyUser()
        {
            Contact = new HashSet<Contact>();
            Operation = new HashSet<Operation>();
        }

        public int IdMyUser { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Company { get; set; }
        public string Rue { get; set; }
        public int? IdCommune { get; set; }
        public string Type { get; set; }

        public Commune IdCommuneNavigation { get; set; }
        public Employee Employee { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Operation> Operation { get; set; }
    }
}
