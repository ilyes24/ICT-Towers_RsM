using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShipManager.Entities
{
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }
        public string Product1 { get; set; }
        public string Reference { get; set; }
        public string SerialNumber { get; set; }
        public double Price { get; set; }
        public int Quantite { get; set; }
        public int IdCategory { get; set; }

        public Category IdCategoryNavigation { get; set; }
    }
}
