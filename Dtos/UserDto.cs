using System.Collections.Generic;

namespace RelationShipManager.Dtos
{
    public class UserDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Company { get; set; }
        public string rue { get; set; }
        public string Type { get; set; }

        public CommuneDto communeDto { get; set; }

        public ICollection<ContactDto> ContactsDtos { get; set; }
    }
}
