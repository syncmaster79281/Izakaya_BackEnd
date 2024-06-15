namespace ISPAN.Izakaya.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string Present { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsLaunched { get; set; }
        public ProductCategoryEntity ProductCategory { get; set; }
    }
}
