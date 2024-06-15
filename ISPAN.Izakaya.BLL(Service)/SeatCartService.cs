using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class SeatCartService
    {
        private readonly ISeatCartRepository _repository;
        public SeatCartService(ISeatCartRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(SeatCartDto seatCart)
        {
            _repository.Create(seatCart.ToEntity());
        }

        public void Delete(int seatId)
        {
            if (seatId <= 0) throw new ArgumentException("seatId 不可小於0");
            _repository.Delete(seatId);
        }

        public SeatCartDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<SeatCartDto> Search(params int[] ids)
        {
            return _repository.Search(ids).Select(x => x.ToDto()).ToList();
        }

        public void UpdateAll(int oldStatusId, int newStatusId)
        {
            _repository.UpdateAll(oldStatusId, newStatusId);
        }

        public void Update(SeatCartDto seatCart)
        {
            _repository.Update(seatCart.ToEntity());
        }
        public void Update(int id, int cartStatusId)
        {
            if (id <= 0) throw new ArgumentException("Id 不可小於0");
            if (cartStatusId <= 0) throw new ArgumentException("CartStatusId 不可小於0");

            _repository.Update(id, cartStatusId);
        }

        public List<MealDetailDto> SearchMealList(int seatId, int statusId)
        {
            if (seatId <= 0) throw new ArgumentException("SeatId 不可小於0");
            if (statusId <= 0) throw new ArgumentException("CartStatusId 不可小於0");


            var entity = _repository.SearchMealList(seatId, statusId);
            return entity.Select(x => x.ToDto()).ToList();
        }

        public int GetProductId(string productName)
        {
            if (string.IsNullOrEmpty(productName)) throw new ArgumentException("ProductName 不可為空");

            return _repository.GetProductId(productName);
        }
        public List<MemberListDto> GetMembers()
        {
            return _repository.GetMembers().Select(x => x.ToDto()).ToList();
        }
        public List<CouponListDto> GetCoupons()
        {
            return _repository.GetCoupons().Select(x => x.ToDto()).ToList();
        }
        public string GetSeatName(int seatId)
        {
            if (seatId <= 0) throw new ArgumentException("SeatId 不可小於0");

            return _repository.GetSeatName(seatId);
        }

        public void UpdateAll(int seatId, int statusId, int completeId)
        {
            if (seatId <= 0) throw new ArgumentException("SeatId 不可小於0");
            if (statusId <= 0) throw new ArgumentException("statusId 不可小於0");
            if (completeId <= 0) throw new ArgumentException("completeId 不可小於0");

            _repository.UpdateAll(seatId, statusId, completeId);
        }
    }
}
