using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShipManager.Entities
{
    public partial class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContact { get; set; }
        public string ContactType { get; set; }
        public string ContactInfo { get; set; }
        public string IsPrimary { get; set; }
        public int IdMyUser { get; set; }

        public MyUser IdMyUserNavigation { get; set; }
    }
}
