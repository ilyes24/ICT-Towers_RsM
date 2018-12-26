using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShipManager.Entities
{
    public class Position
    {
        public Position()
        {
            Employee = new HashSet<Employee>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPosition { get; set; }

        public string Position1 { get; set; }
        public double BaseSalary { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
