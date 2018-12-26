using System;

namespace RelationShipManager.Entities
{
    public class Employee
    {
        public int IdMyUser { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public decimal? Salaire { get; set; }
        public int IdPosition { get; set; }
        public string UserName { get; set; }

        public MyUser IdMyUserNavigation { get; set; }
        public Position IdPositionNavigation { get; set; }
    }
}
