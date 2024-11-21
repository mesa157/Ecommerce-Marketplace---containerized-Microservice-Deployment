namespace ProductService.Model
{
    public class ProductReview
    {
        public int Id { get; set; }

        // Foreign key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
