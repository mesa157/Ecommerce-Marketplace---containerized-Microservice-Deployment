using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductImageRepository : IProductImageRepository
{
    private readonly ProductContext _context;

    public ProductImageRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductImage>> GetAllProductImages()
    {
        return await _context.ProductImages.ToListAsync();
    }

    public async Task<ProductImage> GetProductImageById(int id)
    {
        return await _context.ProductImages.FindAsync(id);
    }

    public async Task AddProductImage(ProductImage productImage)
    {
        await _context.ProductImages.AddAsync(productImage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductImage(ProductImage productImage)
    {
        _context.ProductImages.Update(productImage);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductImage(int id)
    {
        var productImage = await _context.ProductImages.FindAsync(id);
        if (productImage != null)
        {
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
        }
    }
}