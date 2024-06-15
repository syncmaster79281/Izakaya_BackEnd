using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class CombinedOrderService
    {
        private readonly ICombinedOrderRepository _repository;
        public CombinedOrderService(ICombinedOrderRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public int Create(CombinedOrderDto combinedOrder)
        {
            return _repository.Create(combinedOrder.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CombinedOrderDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<CombinedOrderDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(CombinedOrderDto combinedOrder)
        {
            _repository.Update(combinedOrder.ToEntity());
        }
    }
}
