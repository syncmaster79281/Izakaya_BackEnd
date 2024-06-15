using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class OrderPaymentService
    {
        private readonly IOrderPaymentRepository _repository;
        public OrderPaymentService(IOrderPaymentRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(OrderPaymentDto orderPayment)
        {
            _repository.Create(orderPayment.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrderPaymentDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<OrderPaymentDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public Dtos.PagedList<OrderPaymentDto> Search(SortPaymentDto dto, int pageNumber, int pageSize, int branchId)
        {
            var entity = _repository.Search(dto.ToEntity(), pageNumber, pageSize, branchId);
            return new Dtos.PagedList<OrderPaymentDto>(entity.Data.Select(d => d.ToDto()), entity.Pagination.PageNumber, entity.Pagination.PageSize, entity.Pagination.TotalCount);
        }

        public void Update(OrderPaymentDto orderPayment)
        {
            _repository.Update(orderPayment.ToEntity());
        }
    }
}
