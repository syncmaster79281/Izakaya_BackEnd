using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IConponRepository
    {
        void Create(CouponEntity coupon);
        void Delete(int id);
        List<CouponEntity> GetAll();
        CouponEntity Get(int id);
        void Update(CouponEntity coupon);

    }
}
