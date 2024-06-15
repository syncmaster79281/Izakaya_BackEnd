using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class PaymentMethodService
    {
        private readonly IPaymentMethodRepository _repository;
        public PaymentMethodService(IPaymentMethodRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(PaymentMethodDto paymentMethod)
        {
            _repository.Create(paymentMethod.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public PaymentMethodDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<PaymentMethodDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(PaymentMethodDto paymentMethod)
        {
            _repository.Update(paymentMethod.ToEntity());
        }
    }
}
