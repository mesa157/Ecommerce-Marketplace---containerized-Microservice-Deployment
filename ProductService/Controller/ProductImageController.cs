using Microsoft.AspNetCore.Mvc;
using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductImageController : ControllerBase
{
    private readonly IProductImageRepository _productImageRepository;

    public ProductImageController(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
    {
        var productImages = await _productImageRepository.GetAllProductImages();
        return Ok(productImages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductImage>> GetProductImage(int id)
    {
        var productImage = await _productImageRepository.GetProductImageById(id);
        if (productImage == null)
        {
            return NotFound();
        }
        return Ok(productImage);
    }

    [HttpPost]
    public async Task<ActionResult<ProductImage>> AddProductImage(ProductImage productImage)
    {
        await _productImageRepository.AddProductImage(productImage);
        return CreatedAtAction(nameof(GetProductImage), new { id = productImage.Id }, productImage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductImage(int id, ProductImage productImage)
    {
        if (id != productImage.Id)
        {
            return BadRequest();
        }
        await _productImageRepository.UpdateProductImage(productImage);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductImage(int id)
    {
        await _productImageRepository.DeleteProductImage(id);
        return NoContent();
    }
}