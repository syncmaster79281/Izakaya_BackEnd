namespace ISPAN.Izakaya.DAL.Dapper.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UnitPrice { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string Present { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsLaunched { get; set; }
    }
}
