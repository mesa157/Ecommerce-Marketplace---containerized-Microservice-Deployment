using System.ComponentModel.DataAnnotations;

namespace ReviewAndRatingService.Model
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ReviewerName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
