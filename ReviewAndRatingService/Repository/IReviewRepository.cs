using Microsoft.EntityFrameworkCore;
using ReviewAndRatingService.Data;
using ReviewAndRatingService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewAndRatingService.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}
