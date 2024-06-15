using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class CouponService
    {
        private readonly IConponRepository _repository;
        public CouponService(IConponRepository repo)
        {
            _repository = repo;
        }

        public void Create(CouponDto coupon)
        {
            _repository.Create(coupon.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CouponDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<CouponDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(CouponDto coupon)
        {
            _repository.Update(coupon.ToEntity());
        }
    }
}
