using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperOrderDiscountRepository : IOrderDiscountRepository
    {
        private readonly string _connStr;
        public DapperOrderDiscountRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(OrderDiscountEntity orderDiscount)
        {
            string sql = "INSERT INTO OrderDiscounts(OrderPaymentId,CouponId,AppliedValue)VALUES(@OrderPaymentId,@CouponId,@AppliedValue)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    OrderPaymentId = orderDiscount.OrderPaymentId,
                    CouponId = orderDiscount.CouponId,
                    AppliedValue = orderDiscount.AppliedValue,
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM OrderDiscounts WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public OrderDiscountEntity Get(int id)
        {
            string sql = "SELECT Id,OrderPaymentId,CouponId,AppliedValue FROM OrderDiscounts WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                OrderDiscountEntity data = conn.QuerySingle<OrderDiscountEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<OrderDiscountEntity> GetAll()
        {
            string sql = @"SELECT Id,OrderPaymentId,CouponId,AppliedValue
                           FROM OrderDiscounts
                           ORDER BY OrderPaymentId";

            using (var conn = new SqlConnection(_connStr))
            {
                List<OrderDiscountEntity> data = conn.Query<OrderDiscountEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(OrderDiscountEntity orderDiscount)
        {
            string sql = "UPDATE OrderDiscounts Set OrderPaymentId=@OrderPaymentId,CouponId=@CouponId,AppliedValue=@AppliedValue WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, orderDiscount);
            }
        }
    }
}
