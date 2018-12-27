using System.Collections.Generic;

namespace RelationShipManager.Entities
{
    public class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int IdCategory { get; set; }
        public string Category1 { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
