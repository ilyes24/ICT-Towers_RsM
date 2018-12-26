using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShipManager.Entities
{
    public class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategory { get; set; }

        public string Category1 { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
