using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperCouponRepository : IConponRepository
    {
        private readonly string _connStr;

        public DapperCouponRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(CouponEntity coupon)
        {
            string sql = "INSERT INTO Coupons(BranchId,Name,ProductId,TypeId,Condition,DiscountMethod,StartTime,EndTime,IsUsed,Description) VALUES(@BranchId,@Name,@ProductId,@TypeId,@Condition,@DiscountMethod,@StartTime,@EndTime,@IsUsed,@Description)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {

                    BranchId = coupon.BranchId,
                    Name = coupon.Name,
                    ProductId = coupon.ProductId,
                    TypeId = coupon.TypeId,
                    Condition = coupon.Condition,
                    DiscountMethod = coupon.DiscountMethod,
                    StartTime = coupon.StartTime,
                    EndTime = coupon.EndTime,
                    IsUsed = coupon.IsUsed,
                    Description = coupon.Description

                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Coupons WHERE Id=@Id";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public CouponEntity Get(int id)
        {
            string sql = "SELECT Id,BranchId,Name,ProductId,TypeId,Condition,DiscountMethod,StartTime,EndTime,IsUsed,Description FROM Coupons WHERE Id=@Id";
            using (var conn = new SqlConnection(_connStr))
            {
                CouponEntity data = conn.QuerySingle<CouponEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<CouponEntity> GetAll()
        {
            string sql = "SELECT Id,BranchId,Name,ProductId,TypeId,Condition,DiscountMethod,StartTime,EndTime,IsUsed,Description FROM Coupons ORDER BY StartTime";
            using (var conn = new SqlConnection(_connStr))
            {
                List<CouponEntity> data = conn.Query<CouponEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(CouponEntity coupon)
        {
            string sql = "UPDATE Coupons SET BranchId=@BranchId, Name=@Name, ProductId =@ProductId, TypeId = @TypeId, Condition=@Condition,DiscountMethod=@DiscountMethod ,StartTime=@StartTime ,EndTime=@EndTime ,IsUsed=@IsUsed, Description=@Description WHERE Id = @Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, coupon);
            }
        }
    }
}
