using System;
using System.Collections.Generic;
using System.Composition;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CodeArt.MvcMefProvider.Sample.Models
{
    public class DbProductRepository : IProductsRepository
    {
        private readonly ILogger _logger;
        private readonly ProductDbContext _context;

        public DbProductRepository(ILogger logger, ProductDbContext context)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (context == null)
                throw new ArgumentNullException("context");
            _logger = logger;
            _context = context;
        }


        public void AddProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Added;
            _logger.WriteEntry(string.Format("Product Added. SKU: '{0}', Name: '{1}', Price: '{2}', Stock: '{3}'", product.Sku, product.Name, product.Price, product.Stock));
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _logger.WriteEntry(string.Format("Product Updated. SKU: '{0}', Name: '{1}', Price: '{2}', Stock: '{3}'", product.Sku, product.Name, product.Price, product.Stock));
        }

        public void DeleteProduct(int productId)
        {
            var product = new Product() { Id = productId };
            _context.Entry(product).State = EntityState.Deleted;
            _logger.WriteEntry(string.Format("Product Deleted. Id: '{0}'", product.Id));

        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            return _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}