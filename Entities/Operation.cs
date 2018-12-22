using System;
using System.Collections.Generic;

namespace RelationShipManager.Entities
{
    public partial class Operation
    {
        public int IdMyUser { get; set; }
        public DateTime Date { get; set; }
        public int IdProduct { get; set; }
        public int Qantite { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }

        public MyUser IdMyUserNavigation { get; set; }
    }
}
