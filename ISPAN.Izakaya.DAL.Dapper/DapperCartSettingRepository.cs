using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperCartSettingRepository : ICartSettingRepository
    {
        private readonly string _connStr;
        public DapperCartSettingRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(CartSettingEntity cartSetting)
        {
            string sql = "INSERT INTO CartSettings(SeatId,StartTime,EndTime)VALUES(@SeatId,@StartTime,@EndTime)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    SeatId = cartSetting.SeatId,
                    StartTime = cartSetting.StartTime,
                    EndTime = cartSetting.EndTime,
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM CartSettings WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public CartSettingEntity Get(int id)
        {
            string sql = @"SELECT cs.Id,cs.SeatId,cs.StartTime,cs.EndTime,s.Name AS SeatName,b.Name AS BranchName,b.Id AS BranchId,
				DATETIMEFROMPARTS(YEAR(b.RestDay), MONTH(b.RestDay), DAY(b.RestDay), DATEPART(HOUR, b.ClosingTime), DATEPART(MINUTE, b.ClosingTime), DATEPART(SECOND, b.ClosingTime), 0) AS ClosingTime
        FROM CartSettings cs
        JOIN Seats s ON cs.SeatId = s.Id
        JOIN Branches b ON s.BranchId = b.Id
				WHERE cs.Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                CartSettingEntity data = conn.QuerySingle<CartSettingEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<CartSettingEntity> GetAll()
        {
            string sql = @"SELECT cs.Id,cs.SeatId,cs.StartTime,cs.EndTime,s.Name AS SeatName,b.Name AS BranchName,b.Id AS BranchId,
				DATETIMEFROMPARTS(YEAR(b.RestDay), MONTH(b.RestDay), DAY(b.RestDay), DATEPART(HOUR, b.ClosingTime), DATEPART(MINUTE, b.ClosingTime), DATEPART(SECOND, b.ClosingTime), 0) AS ClosingTime
        FROM CartSettings cs
        JOIN Seats s ON cs.SeatId = s.Id
        JOIN Branches b ON s.BranchId = b.Id
        ORDER BY cs.Id";

            using (var conn = new SqlConnection(_connStr))
            {
                List<CartSettingEntity> data = conn.Query<CartSettingEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(CartSettingEntity cartSetting)
        {
            string sql = "UPDATE CartSettings Set SeatId=@SeatId,StartTime=@StartTime,EndTime=@EndTime WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, cartSetting);
            }
        }
    }
}
