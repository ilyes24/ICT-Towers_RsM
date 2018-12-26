using System.Collections.Generic;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface IMyUserService
    {
        IEnumerable<MyUser> GetAll();
        MyUser GetById(int id);
        MyUser Create(MyUser user);
        void Update(MyUser user);
        void Delete(int id);
    }

    public class MyUserService : IMyUserService
    {
        private readonly RelShip_ManContext _context = new RelShip_ManContext();

        public IEnumerable<MyUser> GetAll()
        {
            return _context.MyUser;
        }

        public MyUser GetById(int id)
        {
            return _context.MyUser.Find(id);
        }

        public MyUser Create(MyUser user)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(user.Fname))
                throw new AppException("First name is required.");
            if (string.IsNullOrWhiteSpace(user.Lname))
                throw new AppException("Last name is required.");
            if (string.IsNullOrWhiteSpace(user.Rue))
                throw new AppException("Address is required.");
            if (string.IsNullOrWhiteSpace(user.IdCommune.ToString()))
                throw new AppException("Must choose a state");
            if (string.IsNullOrWhiteSpace(user.Type))
                throw new AppException("user type is required.");

            //Add
            _context.MyUser.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(MyUser user)
        {
            var _user = _context.MyUser.Find(user.IdMyUser);

            //check if EXIST
            if (_user == null)
                throw new AppException("user NOT FOUND");

            //Validation
            if (string.IsNullOrWhiteSpace(user.Fname))
                throw new AppException("First name is required.");
            if (string.IsNullOrWhiteSpace(user.Lname))
                throw new AppException("Last name is required.");
            if (string.IsNullOrWhiteSpace(user.Rue))
                throw new AppException("Address is required.");
            if (string.IsNullOrWhiteSpace(user.IdCommune.ToString()))
                throw new AppException("Must choose a state");
            if (string.IsNullOrWhiteSpace(user.Type))
                throw new AppException("user type is required.");

            //Update
            _user.Fname = user.Fname;
            _user.Lname = user.Lname;
            _user.Rue = _user.Rue;
            _user.Contact = _user.Contact;
            _user.IdCommune = _user.IdCommune;
            _user.IdCommuneNavigation = user.IdCommuneNavigation;


            //Save
            _context.MyUser.Update(_user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.MyUser.Find(id);
            if (user != null)
            {
                _context.MyUser.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
