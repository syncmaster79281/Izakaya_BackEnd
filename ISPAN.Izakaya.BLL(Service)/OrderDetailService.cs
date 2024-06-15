using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _repository;
        public OrderDetailService(IOrderDetailRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(OrderDetailDto orderDetail)
        {
            _repository.Create(orderDetail.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrderDetailDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }
        public List<OrderDetailDto> GetDetails(int combinedOrderId)
        {
            return _repository.GetDetails(combinedOrderId).Select(d => d.ToDto()).ToList();
        }
        public List<OrderDetailDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(OrderDetailDto orderDetail)
        {
            _repository.Update(orderDetail.ToEntity());
        }
    }
}
