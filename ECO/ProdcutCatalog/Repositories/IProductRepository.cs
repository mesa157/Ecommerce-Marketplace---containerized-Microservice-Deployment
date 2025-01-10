using Microsoft.EntityFrameworkCore;
using ProdcutCatalog.DbContexts;
using ProdcutCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdcutCatalog.Entities;

namespace ProdcutCatalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(Guid categoryId, string name, decimal? minPrice, decimal? maxPrice);
        Task<Product> GetProductById(Guid productId);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Guid productId);
    }
}
