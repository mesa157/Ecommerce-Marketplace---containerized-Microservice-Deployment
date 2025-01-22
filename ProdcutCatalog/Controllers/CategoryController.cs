using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProdcutCatalog.Entities;
using ProdcutCatalog.Models;
using ProdcutCatalog.Repositories;

namespace ProdcutCatalog.Controllers;

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

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var result = await _categoryRepository.GetAllCategories();
        return Ok(_mapper.Map<List<CategoryDto>>(result));
    }
}
