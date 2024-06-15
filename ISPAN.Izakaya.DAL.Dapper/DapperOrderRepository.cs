using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperOrderRepository : IOrderRepository
    {
        private readonly string _connStr;
        public DapperOrderRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public int Create(OrderEntity order)
        {
            string sql = "INSERT INTO Orders(SeatId,CombinedOrderId,CreateTime,Subtotal)VALUES(@SeatId,@CombinedOrderId,@CreateTime,@Subtotal); SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                int insertedId = conn.QuerySingle<int>(sql, new
                {
                    SeatId = order.SeatId,
                    CombinedOrderId = order.CombinedOrderId,
                    CreateTime = order.CreateTime,
                    Subtotal = order.Subtotal
                });
                return insertedId;
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Orders WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public OrderEntity Get(int id)
        {
            string sql = "SELECT Id,SeatId,CombinedOrderId,CreateTime,Subtotal FROM Orders WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                OrderEntity data = conn.QuerySingle<OrderEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<OrderEntity> GetAll()
        {
            string sql = @"SELECT Id,SeatId,CombinedOrderId,CreateTime,Subtotal 
                           FROM Orders 
                           ORDER BY CreateTime";

            using (var conn = new SqlConnection(_connStr))
            {
                List<OrderEntity> data = conn.Query<OrderEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(OrderEntity order)
        {
            string sql = "UPDATE Orders Set SeatId=@SeatId,CombinedOrderId=@CombinedOrderId,CreateTime=@CreateTime,Subtotal=@Subtotal WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, order);
            }
        }
    }
}
