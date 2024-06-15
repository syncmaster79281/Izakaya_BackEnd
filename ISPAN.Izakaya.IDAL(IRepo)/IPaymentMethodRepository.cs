using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IPaymentMethodRepository
    {
        void Create(PaymentMethodEntity paymentMethod);
        void Delete(int id);
        List<PaymentMethodEntity> GetAll();
        PaymentMethodEntity Get(int id);

        void Update(PaymentMethodEntity paymentMethod);
    }
}
