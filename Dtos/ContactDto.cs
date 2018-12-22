using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationShipManager.Dtos
{
    public class ContactDto
    {
        public string ContactType { get; set; }
        public string ContactInfo { get; set; }
        public char IsPrimary { get; set; }
    }
}
