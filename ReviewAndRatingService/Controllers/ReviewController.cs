using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReviewAndRatingService.Model;
using ReviewAndRatingService.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ReviewAndRatingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IReviewRepository reviewRepository, ILogger<ReviewController> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting reviews.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return NotFound();
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the review.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Review>> PostReview([FromBody] Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reviewRepository.AddReviewAsync(review);
                return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding the review.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest("Review ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reviewRepository.UpdateReviewAsync(review);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the review.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _reviewRepository.DeleteReviewAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the review.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
