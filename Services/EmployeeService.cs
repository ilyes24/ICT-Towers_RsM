using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;


namespace RelationShipManager.Services
{
    public interface IEmployeeService
    {
        Employee Authenticate(string username, string password);
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        Employee Create(Employee user, string password);
        void Update(Employee user, string password = null);
        void Delete(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private RelShip_ManContext _context = new RelShip_ManContext();
        
        public Employee Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Employee.SingleOrDefault(x => x.UserName == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employee;
        }

        public Employee GetById(int id)
        {
            return _context.Employee.Find(id);
        }

        public Employee Create(Employee employee, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Employee.Any(x => x.UserName == employee.UserName))
                throw new AppException("Username \"" + employee.UserName + "\" is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;
            
            _context.Employee.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public void Update(Employee userParam, string password = null)
        {
            var user = _context.Employee.Find(userParam.IdMyUser);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.UserName != user.UserName)
            {
                // username has changed so check if the new username is already taken
                if (_context.Employee.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("Username " + userParam.UserName + " is already taken");
            }

            // update user properties
            user.IdMyUserNavigation.Fname = userParam.IdMyUserNavigation.Fname;
            user.IdMyUserNavigation.Lname = userParam.IdMyUserNavigation.Lname;
            user.UserName = userParam.UserName;
            user.BirthDate = userParam.BirthDate;
            user.Salaire = userParam.Salaire;
            user.IdPosition = userParam.IdPosition;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Employee.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Employee.Find(id);
            if (user != null)
            {
                _context.Employee.Remove(user);
                _context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password is empty");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
