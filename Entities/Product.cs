namespace RelationShipManager.Entities
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string Product1 { get; set; }
        public string Reference { get; set; }
        public string SerialNumber { get; set; }
        public double Price { get; set; }
        public int Quantite { get; set; }
        public int IdCategory { get; set; }
        public int Deleted { get; set; }

        public Category IdCategoryNavigation { get; set; }
    }
}
