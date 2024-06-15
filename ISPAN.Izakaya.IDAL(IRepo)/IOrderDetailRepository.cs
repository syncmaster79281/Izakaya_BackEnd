using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IOrderDetailRepository
    {
        void Create(OrderDetailEntity orderDetail);
        void Delete(int id);
        List<OrderDetailEntity> GetAll();
        OrderDetailEntity Get(int id);
        List<OrderDetailEntity> GetDetails(int combinedOrderId);

        void Update(OrderDetailEntity orderDetail);
    }
}
