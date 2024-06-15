using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperSeatCartRepository : ISeatCartRepository
    {
        private readonly string _connStr;
        public DapperSeatCartRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(SeatCartEntity seatCart)
        {
            string sql = "INSERT INTO SeatCarts(SeatId,ProductId,CartStatusId,UnitPrice,Qty,Notes,OrderTime)VALUES(@SeatId,@ProductId,@CartStatusId,@UnitPrice,@Qty,@Notes,@OrderTime)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    SeatId = seatCart.SeatId,
                    ProductId = seatCart.ProductId,
                    CartStatusId = seatCart.CartStatusId,
                    UnitPrice = seatCart.UnitPrice,
                    Qty = seatCart.Qty,
                    Notes = seatCart.Notes,
                    OrderTime = seatCart.OrderTime
                });
            }
        }

        public void Delete(int seatId)
        {
            string sql = "DELETE FROM SeatCarts WHERE SeatId=@SeatId";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { SeatId = seatId });
            }
        }

        public SeatCartEntity Get(int id)
        {
            var sql = @"SELECT 
										sc.Id,
										sc.SeatId,
										sc.ProductId,
										sc.CartStatusId,
										sc.UnitPrice, 
										sc.Qty,
										sc.Notes,
										sc.OrderTime,
										s.Name AS SeatName,
										p.Name AS ProductName,
										cs.Status AS CartStatus,
										b.Id AS BranchId 
					        FROM SeatCarts sc
						        JOIN Seats s ON sc.SeatId = s.Id
						        JOIN Products p ON sc.ProductId = p.Id
						        JOIN CartStatuses cs ON sc.CartStatusId = cs.Id
						        JOIN Branches b ON s.BranchId = b.Id
									WHERE sc.Id = @Id";

            using (var conn = new SqlConnection(_connStr))
            {

                SeatCartEntity data = conn.QuerySingle<SeatCartEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<CouponListEntity> GetCoupons()
        {
            var sql = @"SELECT Id,Condition FROM Coupons";

            using (var conn = new SqlConnection(_connStr))
            {
                var data = conn.Query<CouponListEntity>(sql).ToList();
                return data;
            }
        }

        public List<MemberListEntity> GetMembers()
        {
            var sql = @"SELECT Id,Name FROM Members";

            using (var conn = new SqlConnection(_connStr))
            {
                var data = conn.Query<MemberListEntity>(sql).ToList();
                return data;
            }
        }

        public int GetProductId(string productName)
        {
            var sql = @"SELECT Id FROM Products WHERE Name = @ProductName";

            using (var conn = new SqlConnection(_connStr))
            {
                return conn.QuerySingle<int>(sql, new { ProductName = productName });
            }
        }

        public string GetSeatName(int seatId)
        {
            var sql = @"SELECT Name FROM Seats WHERE Id=@SeatId";

            using (var conn = new SqlConnection(_connStr))
            {
                return conn.QuerySingle<string>(sql, new { SeatId = seatId });
            }
        }

        public List<SeatCartEntity> Search(params int[] ids)
        {
            var sql = @"SELECT 
										sc.Id,
										sc.SeatId,
										sc.ProductId,
										sc.CartStatusId,
										sc.UnitPrice,
										sc.Qty,
										sc.Notes,
										sc.OrderTime,
										s.Name AS SeatName,
										p.Name AS ProductName,
										cs.Status AS CartStatus,
										b.Id AS BranchId
					        FROM SeatCarts sc
						        JOIN Seats s ON sc.SeatId = s.Id
						        JOIN Products p ON sc.ProductId = p.Id
						        JOIN CartStatuses cs ON sc.CartStatusId = cs.Id
						        JOIN Branches b ON s.BranchId = b.Id";

            // 若 ids 有值，則添加 WHERE 條件
            if (ids != null && ids.Length > 0)
            {
                sql += " WHERE sc.CartStatusId IN @Ids";
            }

            sql += " ORDER BY sc.OrderTime";

            using (var conn = new SqlConnection(_connStr))
            {
                var data = conn.Query<SeatCartEntity>(sql, new { Ids = ids }).ToList();
                return data;
            }
        }

        public List<MealDetailEntity> SearchMealList(int seatId, int statusId)
        {
            var sql = @"SELECT 
										 SC.Id,
										 SC.SeatId,
										 SC.CartStatusId,
							       S.Name AS SeatName, 
							       P.Name AS ProductName, 
							       CS.Status, 
							       SC.UnitPrice, 
							       SC.Qty, 
							       SC.OrderTime
									FROM SeatCarts SC
									JOIN Products P ON SC.ProductId = P.Id
									JOIN CartStatuses CS ON SC.CartStatusId = CS.Id
									JOIN Seats S ON SC.SeatId = S.Id
									WHERE SC.SeatId = @SeatId
									AND SC.CartStatusId = @CartStatusId";

            using (var conn = new SqlConnection(_connStr))
            {
                var data = conn.Query<MealDetailEntity>(sql, new { SeatId = seatId, CartStatusId = statusId }).ToList();
                return data;
            }
        }

        public void Update(SeatCartEntity seatCart)
        {
            string sql = "UPDATE SeatCarts Set SeatId=@SeatId,ProductId=@ProductId,CartStatusId=@CartStatusId,UnitPrice=@UnitPrice,Qty=@Qty,Notes=@Notes,OrderTime=@OrderTime WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, seatCart);
            }
        }


        public void UpdateAll(int oldStatusId, int newStatusId)
        {
            string sql = "UPDATE SeatCarts Set CartStatusId=@NewStatusId WHERE CartStatusId=@OldStatusId";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { OldStatusId = oldStatusId, NewStatusId = newStatusId });
            }
        }


        public void Update(int id, int cartStatusId)
        {
            string sql = "UPDATE SeatCarts Set CartStatusId=@CartStatusId WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id, CartStatusId = cartStatusId });
            }
        }

        public void UpdateAll(int seatId, int statusId, int completeId)
        {
            string sql = "UPDATE SeatCarts Set CartStatusId=@NewCartStatusId WHERE SeatId=@SeatId AND CartStatusId=@CartStatusId";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { NewCartStatusId = completeId, CartStatusId = statusId, SeatId = seatId });
            }
        }
    }
}
