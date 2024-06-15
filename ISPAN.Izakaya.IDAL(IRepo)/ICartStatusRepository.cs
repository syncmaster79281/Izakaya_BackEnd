using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface ICartStatusRepository
    {
        void Create(CartStatusEntity cartStatus);
        void Delete(int id);
        List<CartStatusEntity> GetAll();
        CartStatusEntity Get(string status);

        void Update(CartStatusEntity cartStatus);
    }
}
