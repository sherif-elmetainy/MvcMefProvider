using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CodeArt.MvcMefProvider.Sample.Models
{
    public interface IProductsRepository
    {
        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);

        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task SaveAsync();
    }
}