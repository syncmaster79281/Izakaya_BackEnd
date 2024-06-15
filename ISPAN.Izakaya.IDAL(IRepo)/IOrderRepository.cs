using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IOrderRepository
    {
        int Create(OrderEntity order);
        void Delete(int id);
        List<OrderEntity> GetAll();
        OrderEntity Get(int id);

        void Update(OrderEntity order);
    }
}
