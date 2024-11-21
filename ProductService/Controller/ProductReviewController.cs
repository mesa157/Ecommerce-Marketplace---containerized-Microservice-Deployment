using Microsoft.AspNetCore.Mvc;
using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductReviewController : ControllerBase
{
    private readonly IProductReviewRepository _productReviewRepository;

    public ProductReviewController(IProductReviewRepository productReviewRepository)
    {
        _productReviewRepository = productReviewRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReview>>> GetProductReviews()
    {
        var productReviews = await _productReviewRepository.GetAllProductReviews();
        return Ok(productReviews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReview>> GetProductReview(int id)
    {
        var productReview = await _productReviewRepository.GetProductReviewById(id);
        if (productReview == null)
        {
            return NotFound();
        }
        return Ok(productReview);
    }

    [HttpPost]
    public async Task<ActionResult<ProductReview>> AddProductReview(ProductReview productReview)
    {
        await _productReviewRepository.AddProductReview(productReview);
        return CreatedAtAction(nameof(GetProductReview), new { id = productReview.Id }, productReview);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductReview(int id, ProductReview productReview)
    {
        if (id != productReview.Id)
        {
            return BadRequest();
        }
        await _productReviewRepository.UpdateProductReview(productReview);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductReview(int id)
    {
        await _productReviewRepository.DeleteProductReview(id);
        return NoContent();
    }
}
