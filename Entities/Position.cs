using System.Collections.Generic;

namespace RelationShipManager.Entities
{
    public class Position
    {
        public Position()
        {
            Employee = new HashSet<Employee>();
        }

        public int IdPosition { get; set; }
        public string Position1 { get; set; }
        public double BaseSalary { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
