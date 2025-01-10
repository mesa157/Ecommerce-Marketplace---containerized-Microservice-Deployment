using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProdcutCatalog.Entities;
using ProdcutCatalog.Models;
using ProdcutCatalog.Repositories;

namespace ProductCatalog.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var result = await _categoryRepository.GetAllCategories();
            return Ok(_mapper.Map<List<CategoryDto>>(result));
        }

        // Get category by ID
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid categoryId)
        {
            var result = await _categoryRepository.GetCategoryById(categoryId.ToString());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(result));
        }

        // Create a new category
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddCategory(category);
            return CreatedAtAction(nameof(GetById), new { categoryId = category.CategoryId }, categoryDto);
        }
    }
}
