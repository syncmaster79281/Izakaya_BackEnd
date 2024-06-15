using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IActivityRepository
    {
        void Create(ActivityEntity activity);
        void Delete(int id);
        List<ActivityEntity> GetAll();
        ActivityEntity Get(int id);
        void Update(ActivityEntity activity);
    }
}
