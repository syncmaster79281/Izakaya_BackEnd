using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface ICombinedOrderRepository
    {
        int Create(CombinedOrderEntity combinedOrder);
        void Delete(int id);
        List<CombinedOrderEntity> GetAll();
        CombinedOrderEntity Get(int id);

        void Update(CombinedOrderEntity combinedOrder);
    }
}
