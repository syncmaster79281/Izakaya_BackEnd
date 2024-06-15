using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperSeatRepository : ISeatRepository
    {
        private readonly string _connStr;
        public DapperSeatRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(SeatEntity seat)
        {
            string sql = "INSERT INTO Seats(BranchId,Name,QRCodeLink,Capacity,Status)VALUES(@BranchId,@Name,@QRCodeLink,@Capacity,@Status)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    BranchId = seat.BranchId,
                    Name = seat.Name,
                    QRCodeLink = seat.QRCodeLink,
                    Capacity = seat.Capacity,
                    Status = seat.Status
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Seats WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public SeatEntity Get(int id)
        {
            string sql = "SELECT Id,BranchId,Name,QRCodeLink,Capacity,Status FROM Seats WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                SeatEntity data = conn.QuerySingle<SeatEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<SeatEntity> GetAll()
        {
            string sql = @"SELECT Id,BranchId,Name,QRCodeLink,Capacity,Status
                           FROM Seats 
                           ORDER BY Name";

            using (var conn = new SqlConnection(_connStr))
            {
                List<SeatEntity> data = conn.Query<SeatEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(SeatEntity seat)
        {
            string sql = "UPDATE Seats Set BranchId=@BranchId,Name=@Name,QRCodeLink=@QRCodeLink,Capacity=@Capacity,Status=@Status WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, seat);
            }
        }
    }

}
