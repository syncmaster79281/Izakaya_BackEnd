using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class ActivityService
    {
        private readonly IActivityRepository _repository;

        public ActivityService(IActivityRepository repo)
        {
            _repository = repo;
        }
        public void Create(ActivityDto activity)
        {
            _repository.Create(activity.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public ActivityDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<ActivityDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }
        public void Update(ActivityDto activity)
        {
            _repository.Update(activity.ToEntity());
        }
    }
}
