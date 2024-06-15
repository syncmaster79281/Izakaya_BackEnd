using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public int Create(OrderDto order)
        {
            return _repository.Create(order.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrderDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<OrderDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(OrderDto order)
        {
            _repository.Update(order.ToEntity());
        }
    }
}
