using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IOrderPaymentRepository
    {
        void Create(OrderPaymentEntity orderPayment);
        void Delete(int id);
        List<OrderPaymentEntity> GetAll();
        OrderPaymentEntity Get(int id);

        PagedList<OrderPaymentEntity> Search(SortPaymentEntity entity, int pageNumber, int pageSize, int branchId);

        void Update(OrderPaymentEntity orderPayment);
    }
}
