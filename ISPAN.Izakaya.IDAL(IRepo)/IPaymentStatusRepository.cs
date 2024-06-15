using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IPaymentStatusRepository
    {
        void Create(PaymentStatusEntity paymentStatus);
        void Delete(int id);
        List<PaymentStatusEntity> GetAll();
        PaymentStatusEntity Get(int id);

        void Update(PaymentStatusEntity paymentStatus);
    }
}
