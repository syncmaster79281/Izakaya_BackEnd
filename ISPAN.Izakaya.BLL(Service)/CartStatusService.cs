using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class CartStatusService
    {
        private readonly ICartStatusRepository _repository;
        public CartStatusService(ICartStatusRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(CartStatusDto cartStatus)
        {
            _repository.Create(cartStatus.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CartStatusDto Get(string status)
        {
            return _repository.Get(status).ToDto();
        }

        public List<CartStatusDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(CartStatusDto cartStatus)
        {
            _repository.Update(cartStatus.ToEntity());
        }
    }
}
