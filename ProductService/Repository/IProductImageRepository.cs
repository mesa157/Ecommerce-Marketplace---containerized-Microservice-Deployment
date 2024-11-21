using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductImageRepository
{
    Task<IEnumerable<ProductImage>> GetAllProductImages();
    Task<ProductImage> GetProductImageById(int id);
    Task AddProductImage(ProductImage productImage);
    Task UpdateProductImage(ProductImage productImage);
    Task DeleteProductImage(int id);
}