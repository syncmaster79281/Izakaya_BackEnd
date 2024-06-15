using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperActivityRepository : IActivityRepository
    {
        private readonly string _connStr;
        public DapperActivityRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(ActivityEntity activity)
        {
            string sql = "INSERT INTO Activities( BranchId, Name, Type, Discount, StartTime, EndTime, IsUsed, Levels, Description)VALUES( @BranchId, @Name, @Type, @Discount, @StartTime, @EndTime, @IsUsed, @Levels, @Description)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    BranchId = activity.BranchId,
                    Name = activity.Name,
                    Type = activity.Type,
                    Discount = activity.Discount,
                    StartTime = activity.StartTime,
                    EndTime = activity.EndTime,
                    IsUsed = activity.IsUsed,
                    Levels = activity.Levels,
                    Description = activity.Description
                });

            }


        }
        public void Delete(int id)
        {
            string sql = "DELETE FROM Activities WHERE Id=@Id";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public ActivityEntity Get(int id)
        {
            string sql = "SELECT Id, BranchId, Name, Type, Discount, StartTime, EndTime, IsUsed, Levels, Description FROM Activities WHERE Id=@Id";
            using (var conn = new SqlConnection(_connStr))
            {
                ActivityEntity data = conn.QuerySingle<ActivityEntity>(sql, new { Id = id });
                return data;

            }
        }

        public List<ActivityEntity> GetAll()
        {
            string sql = "SELECT Id, BranchId, Name, Type, Discount, StartTime, EndTime, IsUsed, Levels, Description FROM Activities ORDER BY StartTime";
            using (var conn = new SqlConnection(_connStr))
            {
                List<ActivityEntity> data = conn.Query<ActivityEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(ActivityEntity activity)
        {
            string sql = "UPDATE Activities SET Name=@Name, Type=@Type, Discount=@Discount, StartTime=@StartTime, EndTime=@EndTime, IsUsed=@IsUsed, Levels=@Levels, Description=@Description WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, activity);
            }

        }
    }
}
