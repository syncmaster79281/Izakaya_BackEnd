using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IOrderDiscountRepository
    {
        void Create(OrderDiscountEntity orderDiscount);
        void Delete(int id);
        List<OrderDiscountEntity> GetAll();
        OrderDiscountEntity Get(int id);

        void Update(OrderDiscountEntity orderDiscount);
    }
}
