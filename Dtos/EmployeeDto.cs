using System;

namespace RelationShipManager.Dtos
{
    public class EmployeeDto
    {
        public UserDto myUser { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salaire { get; set; }

        public PositionDto positionDto { get; set; }
    }
}
