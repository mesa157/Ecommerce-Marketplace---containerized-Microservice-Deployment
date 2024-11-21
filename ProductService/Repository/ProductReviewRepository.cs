using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductReviewRepository : IProductReviewRepository
{
    private readonly ProductContext _context;

    public ProductReviewRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductReview>> GetAllProductReviews()
    {
        return await _context.ProductReviews.ToListAsync();
    }

    public async Task<ProductReview> GetProductReviewById(int id)
    {
        return await _context.ProductReviews.FindAsync(id);
    }

    public async Task AddProductReview(ProductReview productReview)
    {
        await _context.ProductReviews.AddAsync(productReview);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductReview(ProductReview productReview)
    {
        _context.ProductReviews.Update(productReview);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductReview(int id)
    {
        var productReview = await _context.ProductReviews.FindAsync(id);
        if (productReview != null)
        {
            _context.ProductReviews.Remove(productReview);
            await _context.SaveChangesAsync();
        }
    }
}