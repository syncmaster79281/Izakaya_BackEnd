using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface ISeatRepository
    {
        void Create(SeatEntity seat);
        void Delete(int id);
        List<SeatEntity> GetAll();
        SeatEntity Get(int id);

        void Update(SeatEntity seat);
    }
}
