using System.Collections.Generic;
using System.Linq;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;

namespace ISPAN.Izakaya.BLL_Service_
{
	public class SeatService
	{
		private readonly ISeatRepository _repository;
		public SeatService(ISeatRepository repo)
		{
			//決定用 EF 或 Dapper
			_repository = repo;
		}

		public void Create(SeatDto seat)
		{
			_repository.Create(seat.ToEntity());
		}

		public void Delete(int id)
		{
			_repository.Delete(id);
		}

		public SeatDto Get(int id)
		{
			return _repository.Get(id).ToDto();
		}

		public List<SeatDto> GetAll()
		{
			return _repository.GetAll().Select(x => x.ToDto()).ToList();
		}

		public void Update(SeatDto seat)
		{
			_repository.Update(seat.ToEntity());
		}
	}
}
