using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperBranchRepository : IBranchRepository
    {
        private readonly string _connStr;
        public DapperBranchRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(BranchEntity branch)
        {
            string sql = "INSERT INTO Branches(Name,Address,Tel,SeatingCapacity,OpeningTime,ClosingTime,RestDay)VALUES(@Name,@Address,@Tel,@SeatingCapacity,@OpeningTime,@ClosingTime,@RestDay)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    Name = branch.Name,
                    Address = branch.Address,
                    Tel = branch.Tel,
                    SeatingCapacity = branch.SeatingCapacity,
                    OpeningTime = branch.OpeningTime,
                    ClosingTime = branch.ClosingTime,
                    RestDay = branch.RestDay,
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Branches WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public BranchEntity Get(int id)
        {
            string sql = "SELECT Id,Name,Address,Tel,SeatingCapacity,OpeningTime,ClosingTime,RestDay FROM Branches WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {

                BranchEntity data = conn.QuerySingle<BranchEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<BranchEntity> GetAll()
        {
            string sql = @"SELECT Id,Name,Address,Tel,SeatingCapacity,OpeningTime,ClosingTime,RestDay
                           FROM Branches
                           ORDER BY Id";

            using (var conn = new SqlConnection(_connStr))
            {
                List<BranchEntity> data = conn.Query<BranchEntity>(sql).ToList();
                return data;
            }
        }

        public void Update(BranchEntity branch)
        {
            string sql = "UPDATE Branches Set Name=@Name,Address=@Address,Tel=@Tel,SeatingCapacity=@SeatingCapacity,OpeningTime=@OpeningTime,ClosingTime=@ClosingTime,RestDay=@RestDay WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, branch);
            }
        }

        public void UpdateCloseTime(DateTime closeTime, int branchId)
        {
            string sql = "UPDATE Branches Set RestDay=@RestDay WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { RestDay = closeTime, Id = branchId });
            }
        }
    }
}

