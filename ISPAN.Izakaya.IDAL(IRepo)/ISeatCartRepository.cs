using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface ISeatCartRepository
    {
        void Create(SeatCartEntity seatCart);
        void Delete(int seatId);
        List<SeatCartEntity> Search(params int[] ids);
        SeatCartEntity Get(int id);
        List<MealDetailEntity> SearchMealList(int seatId, int statusId);
        void Update(SeatCartEntity seatCart);
        void Update(int id, int cartStatusId);
        int GetProductId(string productName);
        List<MemberListEntity> GetMembers();
        string GetSeatName(int seatId);
        void UpdateAll(int seatId, int statusId, int completeId);
        List<CouponListEntity> GetCoupons();
        void UpdateAll(int oldStatusId, int newStatusId);
    }
}
