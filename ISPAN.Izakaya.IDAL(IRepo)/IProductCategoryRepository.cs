using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategoryEntity entity);
        void Update(ProductCategoryEntity entity);
        void Delete(int id);
        IEnumerable<ProductCategoryEntity> Search(string name);
        ProductCategoryEntity Get(int id);

    }
}
