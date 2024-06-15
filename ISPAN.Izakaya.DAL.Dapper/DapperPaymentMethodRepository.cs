using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperPaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly string _connStr;
        public DapperPaymentMethodRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(PaymentMethodEntity paymentMethod)
        {
            string sql = "INSERT INTO PaymentMethods(Method) VALUES(@Method);";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    Method = paymentMethod.Method
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM PaymentMethods WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public PaymentMethodEntity Get(int id)
        {
            string sql = "SELECT Id,Method FROM PaymentMethods WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                PaymentMethodEntity data = conn.QuerySingle<PaymentMethodEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<PaymentMethodEntity> GetAll()
        {
            string sql = @"SELECT Id,Method 
                           FROM PaymentMethods 
                           ORDER BY Id";

            using (var conn = new SqlConnection(_connStr))
            {
                List<PaymentMethodEntity> data = conn.Query<PaymentMethodEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(PaymentMethodEntity paymentMethod)
        {
            string sql = "UPDATE PaymentMethods Set Method=@Method WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, paymentMethod);
            }
        }
    }
}
