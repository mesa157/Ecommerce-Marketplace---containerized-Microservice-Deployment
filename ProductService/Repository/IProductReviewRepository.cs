using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductReviewRepository
{
    Task<IEnumerable<ProductReview>> GetAllProductReviews();
    Task<ProductReview> GetProductReviewById(int id);
    Task AddProductReview(ProductReview productReview);
    Task UpdateProductReview(ProductReview productReview);
    Task DeleteProductReview(int id);
}