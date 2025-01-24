using Microsoft.EntityFrameworkCore;
using ProdcutCatalog.DbContexts;
using ProdcutCatalog.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ProdcutCatalog.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(Guid categoryId);
        Task AddCategory(Category category);
    }
}
