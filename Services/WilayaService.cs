using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface IWilayaService
    {
        IEnumerable<Wilaya> GetAll();
        Wilaya GetById(int id);
        Wilaya GetByName(string Name);
        Wilaya Create(Wilaya wilaya);
        void Update(Wilaya wilaya);
        void Delete(int id);
    }
    public class WilayaService : IWilayaService
    {
        private RelShip_ManContext _context = new RelShip_ManContext();

        public IEnumerable<Wilaya> GetAll()
        {
            return _context.Wilaya;
        }

        public Wilaya GetById(int id)
        {
            return _context.Wilaya.Find(id);
        }

        public Wilaya GetByName(string Name)
        {
            foreach (var wilaya in this.GetAll())
            {
                if (wilaya.Wilaya1 == Name)
                    return wilaya;

            }
            return null;
        }

        public Wilaya Create(Wilaya wilaya)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(wilaya.Wilaya1))
                throw new AppException("Wilaya name is required");

            //Add
            _context.Wilaya.Add(wilaya);
            _context.SaveChanges();

            return wilaya;
        }

        public void Update(Wilaya wilaya)
        {
            //Find position
            var _wilaya = _context.Wilaya.Find(wilaya.IdWilaya);

            //if NOT FOUND
            if (_wilaya == null)
                throw new AppException("Wilaya NOT FOUNT");

            //Validation
            if (string.IsNullOrWhiteSpace(wilaya.Wilaya1))
                throw new AppException("Wilaya name is required");

            //Update
            _wilaya.Wilaya1 = wilaya.Wilaya1;

            //Save
            _context.Wilaya.Update(_wilaya);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var wilaya = _context.Wilaya.Find(id);
            if (wilaya != null)
            {
                _context.Wilaya.Remove(wilaya);
                _context.SaveChanges();
            }
        }
    }
}
