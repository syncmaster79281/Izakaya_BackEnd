using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class PaymentStatusService
    {
        private readonly IPaymentStatusRepository _repository;
        public PaymentStatusService(IPaymentStatusRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(PaymentStatusDto paymentStatus)
        {
            _repository.Create(paymentStatus.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public PaymentStatusDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<PaymentStatusDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(PaymentStatusDto paymentStatus)
        {
            _repository.Update(paymentStatus.ToEntity());
        }
    }
}
