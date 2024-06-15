namespace ISPAN.Izakaya.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string Present { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsLaunched { get; set; }
        public ProductCategoryDto ProductCategory { get; set; }
    }
}
