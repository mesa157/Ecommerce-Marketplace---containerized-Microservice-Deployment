namespace ProdcutCatalog.Models
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public IList<ProductDto> Products { get; set; }
    }
}
