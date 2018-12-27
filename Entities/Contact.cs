namespace RelationShipManager.Entities
{
    public class Contact
    {
        public int IdContact { get; set; }
        public string ContactType { get; set; }
        public string ContactInfo { get; set; }
        public string IsPrimary { get; set; }
        public int IdMyUser { get; set; }

        public MyUser IdMyUserNavigation { get; set; }
    }
}
