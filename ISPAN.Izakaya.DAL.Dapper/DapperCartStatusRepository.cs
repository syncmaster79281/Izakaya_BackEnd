using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperCartStatusRepository : ICartStatusRepository
    {
        private readonly string _connStr;
        public DapperCartStatusRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(CartStatusEntity cartStatus)
        {
            string sql = "INSERT INTO CartStatuses(Status)VALUES(@Status)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    Status = cartStatus.Status
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM CartStatuses WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public CartStatusEntity Get(string status)
        {
            string sql = "SELECT Id,Status FROM CartStatuses WHERE Status=@Status";

            using (var conn = new SqlConnection(_connStr))
            {

                CartStatusEntity data = conn.QueryFirst<CartStatusEntity>(sql, new { Status = status });
                return data;
            }
        }

        public List<CartStatusEntity> GetAll()
        {
            string sql = @"SELECT Id,Status
                           FROM CartStatuses
                           ORDER BY Id";

            using (var conn = new SqlConnection(_connStr))
            {
                List<CartStatusEntity> data = conn.Query<CartStatusEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(CartStatusEntity cartStatus)
        {
            string sql = "UPDATE CartStatuses Set Status=@Status WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, cartStatus);
            }
        }
    }
}
