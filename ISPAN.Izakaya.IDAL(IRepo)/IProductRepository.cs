using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IProductRepository
    {
        void Create(ProductEntity entity);
        void Update(ProductEntity entity);
        void Delete(int id);
        IEnumerable<ProductEntity> Search(string name);
        IEnumerable<ProductEntity> Search(SearchDataEntity entity);
        ProductEntity Get(int id);
        int GetCount(SearchDataEntity entity);
        IEnumerable<ProductEntity> GetProductsInSameCategory(int id);
        List<ProductDropList> GetProducts();
    }
}
