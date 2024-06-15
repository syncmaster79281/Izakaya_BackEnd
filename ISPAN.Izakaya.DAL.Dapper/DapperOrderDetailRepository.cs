using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperOrderDetailRepository : IOrderDetailRepository
    {
        private readonly string _connStr;
        public DapperOrderDetailRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(OrderDetailEntity orderDetail)
        {
            string sql = "INSERT INTO OrderDetails(OrderId,ProductId,UnitPrice,Qty)VALUES(@OrderId,@ProductId,@UnitPrice,@Qty)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    OrderId = orderDetail.OrderId,
                    ProductId = orderDetail.ProductId,
                    UnitPrice = orderDetail.UnitPrice,
                    Qty = orderDetail.Qty
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM OrderDetails WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public OrderDetailEntity Get(int id)
        {
            string sql = "SELECT Id,OrderId,ProductId,UnitPrice,Qty FROM OrderDetails WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                OrderDetailEntity data = conn.QuerySingle<OrderDetailEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<OrderDetailEntity> GetAll()
        {
            string sql = @"SELECT Id,OrderId,ProductId,UnitPrice,Qty 
                           FROM OrderDetails
                           ORDER BY OrderId";

            using (var conn = new SqlConnection(_connStr))
            {
                List<OrderDetailEntity> data = conn.Query<OrderDetailEntity>(sql).ToList();
                return data;
            }
        }
        public List<OrderDetailEntity> GetDetails(int combinedOrderId)
        {
            string sql = @"SELECT 
											od.Id,
											od.OrderId,
											od.ProductId,
											od.UnitPrice,
											od.Qty,
											p.Name AS ProductName
										FROM OrderDetails od
										INNER JOIN Orders o ON od.OrderId = o.Id
										INNER JOIN Products p ON od.ProductId = p.Id
										WHERE o.CombinedOrderId = @CombinedOrderId
										ORDER BY od.OrderId";

            using (var conn = new SqlConnection(_connStr))
            {
                List<OrderDetailEntity> data = conn.Query<OrderDetailEntity>(sql, new { CombinedOrderId = combinedOrderId }).ToList();
                return data;
            }
        }
        public void Update(OrderDetailEntity orderDetail)
        {
            string sql = "UPDATE OrderDetails Set OrderId=@OrderId,ProductId=@ProductId,UnitPrice=@UnitPrice,Qty=@Qty WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, orderDetail);
            }
        }
    }
}
