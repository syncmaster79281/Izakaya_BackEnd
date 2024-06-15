using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface ICartSettingRepository
    {
        void Create(CartSettingEntity cartSetting);
        void Delete(int id);
        List<CartSettingEntity> GetAll();
        CartSettingEntity Get(int id);

        void Update(CartSettingEntity cartSetting);
    }
}
