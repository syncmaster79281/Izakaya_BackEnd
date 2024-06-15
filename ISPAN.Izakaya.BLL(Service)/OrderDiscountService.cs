using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class OrderDiscountService
    {
        private readonly IOrderDiscountRepository _repository;
        public OrderDiscountService(IOrderDiscountRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(OrderDiscountDto orderDiscount)
        {
            _repository.Create(orderDiscount.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrderDiscountDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<OrderDiscountDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(OrderDiscountDto orderDiscount)
        {
            _repository.Update(orderDiscount.ToEntity());
        }
    }
}
