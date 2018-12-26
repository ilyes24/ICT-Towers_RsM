using System.Collections.Generic;
using System.Linq;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category Create(Category user);
        void Update(Category user);
        void Delete(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly RelShip_ManContext _context = new RelShip_ManContext();


        public IEnumerable<Category> GetAll()
        {
            return _context.Category;
        }

        public Category GetById(int id)
        {
            return _context.Category.Find(id);
        }

        public Category Create(Category category)
        {
            if (_context.Category.Any(x => x.Category1 == category.Category1))
                throw new AppException("Category with name " + category.Category1 + " Already Exists");

            _context.Category.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void Update(Category category)
        {
            var c = GetById(category.IdCategory);

            if (c == null)
                throw new AppException("Category Not Found");

            c.Category1 = category.Category1;
            _context.Category.Update(c);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = GetById(id);
            if (c != null)
            {
                _context.Category.Remove(c);
                _context.SaveChanges();
            }
            else
            {
                throw new AppException("Category Not Found:  " + id);
            }
        }

        public Category GetByName(string name)
        {
            return _context.Category.Find(name);
        }
    }
}
