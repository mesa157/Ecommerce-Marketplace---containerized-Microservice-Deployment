namespace FrontendService.Models.Product
{

    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        // Foreign key for Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigation properties
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
