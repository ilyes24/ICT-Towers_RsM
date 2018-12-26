using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(Category c);
        Product GetById(int id);
        Product Create(Product user);
        void Update(Product user);
        void Delete(int id);
    }

    public class ProductService : IProductService
    {
        private readonly RelShip_ManContext _context = new RelShip_ManContext();


        public IEnumerable<Product> GetAll(Category c)
        {
            var products = new ObservableCollection<Product>();
            foreach (var product in _context.Product)
                if (product.IdCategory == c.IdCategory)
                    products.Add(product);

            return products;
        }

        public Product GetById(int id)
        {
            return _context.Product.Find(id);
        }

        public Product Create(Product product)
        {
            if (_context.Product.Any(x => x.Product1 == product.Product1))
                throw new AppException("Product with name " + product.Product1 + " Already Exists");

            _context.Product.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Update(Product product)
        {
            var c = GetById(product.IdProduct);

            if (c == null)
                throw new AppException("Product Not Found");

            c.Product1 = product.Product1;
            _context.Product.Update(c);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = GetById(id);
            if (c != null)
            {
                _context.Product.Remove(c);
                _context.SaveChanges();
            }
        }

        public Product GetByName(string name)
        {
            return _context.Product.Find(name);
        }
    }
}
