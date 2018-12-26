using System.Collections.Generic;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface IPositionService
    {
        IEnumerable<Position> GetAll();
        Position GetById(int id);
        Position GetByName(string Name);
        Position Create(Position position);
        void Update(Position position);
        void Delete(int id);
    }

    public class PositionService : IPositionService
    {
        private readonly RelShip_ManContext _context = new RelShip_ManContext();

        public IEnumerable<Position> GetAll()
        {
            return _context.Position;
        }

        public Position GetById(int id)
        {
            return _context.Position.Find(id);
        }

        public Position GetByName(string Name)
        {
            foreach (var position in GetAll())
                if (position.Position1 == Name)
                    return position;
            return null;
        }

        public Position Create(Position position)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(position.Position1))
                throw new AppException("Position name is required");
            if (string.IsNullOrWhiteSpace(position.BaseSalary.ToString()))
                throw new AppException("Base salary is required");

            //Add
            _context.Position.Add(position);
            _context.SaveChanges();

            return position;
        }

        public void Update(Position position)
        {
            //Find position
            var _position = _context.Position.Find(position.IdPosition);

            //if NOT FOUND
            if (_position == null)
                throw new AppException("Position NOT FOUNT");

            //Validation
            if (string.IsNullOrWhiteSpace(position.Position1))
                throw new AppException("Position name is required");
            if (string.IsNullOrWhiteSpace(position.BaseSalary.ToString()))
                throw new AppException("Base salary is required");

            //Update
            _position.BaseSalary = position.BaseSalary;
            _position.Position1 = position.Position1;

            //Save
            _context.Position.Update(_position);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var position = _context.Position.Find(id);
            if (position != null)
            {
                _context.Position.Remove(position);
                _context.SaveChanges();
            }
        }
    }
}
