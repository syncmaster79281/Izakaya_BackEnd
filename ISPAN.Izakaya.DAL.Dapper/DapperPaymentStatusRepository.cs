using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperPaymentStatusRepository : IPaymentStatusRepository
    {
        private readonly string _connStr;
        public DapperPaymentStatusRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(PaymentStatusEntity paymentStatus)
        {
            string sql = "INSERT INTO PaymentStatuses(Status) VALUES(@Status);";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    Status = paymentStatus.Status
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM PaymentStatuses WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public PaymentStatusEntity Get(int id)
        {
            string sql = "SELECT Id,Status FROM PaymentStatuses WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                PaymentStatusEntity data = conn.QuerySingle<PaymentStatusEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<PaymentStatusEntity> GetAll()
        {
            string sql = @"SELECT Id,Status 
                           FROM PaymentStatuses 
                           ORDER BY Id";

            using (var conn = new SqlConnection(_connStr))
            {
                List<PaymentStatusEntity> data = conn.Query<PaymentStatusEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(PaymentStatusEntity paymentStatus)
        {
            string sql = "UPDATE PaymentStatuses Set Status=@Status WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, paymentStatus);
            }
        }
    }
}
