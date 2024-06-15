using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperCombinedOrderRepository : ICombinedOrderRepository
    {
        private readonly string _connStr;
        public DapperCombinedOrderRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public int Create(CombinedOrderEntity combinedOrder)
        {
            string sql = "INSERT INTO CombinedOrders(CreateTime) VALUES(@CreateTime); SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                int insertedId = conn.QuerySingle<int>(sql, new
                {
                    CreateTime = combinedOrder.CreateTime,
                });
                return insertedId;
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM CombinedOrders WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public CombinedOrderEntity Get(int id)
        {
            string sql = "SELECT Id,CreateTime FROM CombinedOrders WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                CombinedOrderEntity data = conn.QuerySingle<CombinedOrderEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<CombinedOrderEntity> GetAll()
        {
            string sql = @"SELECT Id,CreateTime
                           FROM CombinedOrders
                           ORDER BY Id";

            using (var conn = new SqlConnection(_connStr))
            {
                List<CombinedOrderEntity> data = conn.Query<CombinedOrderEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(CombinedOrderEntity combinedOrder)
        {
            string sql = "UPDATE CombinedOrders Set CreateTime=@CreateTime WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, combinedOrder);
            }
        }
    }
}
