using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationShipManager.Dtos
{
    public class CommuneDto
    {
        public string Commune { get; set; }
        public string code_postal { get; set; }

        public WilayaDto wilayaDto { get; set; }
    }
}
