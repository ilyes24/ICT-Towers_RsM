using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface ICommuneService
    {
        IEnumerable<Commune> GetAll();
        Commune GetById(int id);
        Commune GetByName(string codePostal);
        Commune Create(Commune commune);
        void Update(Commune commune);
        void Delete(int id);
    }
    public class CommuneService : ICommuneService
    {
        private RelShip_ManContext _context = new RelShip_ManContext();

        public IEnumerable<Commune> GetAll()
        {
            return _context.Commune;
        }

        public Commune GetById(int id)
        {
            return _context.Commune.Find(id);
        }

        public Commune GetByName(string name)
        {
            foreach (var commune in this.GetAll())
            {
                if (commune.Commune1 == name)
                    return commune;
            }
            return null;
        }

        public Commune Create(Commune commune)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(commune.Commune1))
                throw new AppException("Commune name is required");
            if (string.IsNullOrWhiteSpace(commune.CodePostal.ToString()))
                throw new AppException("Code Postal is required");

            //Add
            _context.Commune.Add(commune);
            _context.SaveChanges();

            return commune;
        }

        public void Update(Commune commune)
        {
            //Find position
            var _commune = _context.Commune.Find(commune.IdCommune);

            //if NOT FOUND
            if (_commune == null)
                throw new AppException("Commune NOT FOUNT");

            //Validation
            if (string.IsNullOrWhiteSpace(commune.Commune1))
                throw new AppException("Commune name is required");
            if (string.IsNullOrWhiteSpace(commune.CodePostal.ToString()))
                throw new AppException("Code Postal is required");

            //Update
            _commune.CodePostal = commune.CodePostal;
            _commune.Commune1 = commune.Commune1;

            //Save
            _context.Commune.Update(_commune);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var commune = _context.Commune.Find(id);
            if (commune != null)
            {
                _context.Commune.Remove(commune);
                _context.SaveChanges();
            }
        }
    }
}
