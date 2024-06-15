using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class CartSettingService
    {
        private readonly ICartSettingRepository _repository;
        public CartSettingService(ICartSettingRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(CartSettingDto cartSetting)
        {
            _repository.Create(cartSetting.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CartSettingDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<CartSettingDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(CartSettingDto cartSetting)
        {
            _repository.Update(cartSetting.ToEntity());
        }
    }
}
