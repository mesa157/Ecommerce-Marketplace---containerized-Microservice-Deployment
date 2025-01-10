namespace FrontendService.Models.Product
{
    public class Tag
    {
        // Composite key
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<ProductTag> ProductTags { get; set; }
    }

}
