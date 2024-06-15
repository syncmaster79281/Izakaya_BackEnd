using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IProductStockRepository
    {
        void Create(ProductStockEntity entity);
        void Update(ProductStockEntity entity);
        void Delete(int Id);
        IEnumerable<ProductStockEntity> Search();
    }
}
