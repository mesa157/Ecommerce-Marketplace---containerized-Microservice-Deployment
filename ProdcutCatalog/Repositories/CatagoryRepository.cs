using Microsoft.EntityFrameworkCore;
using ProdcutCatalog.DbContexts;
using ProdcutCatalog.Entities;

namespace ProdcutCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductCatalogDbContext _context;

        public CategoryRepository(ProductCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.Include(x => x.Products).ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}
