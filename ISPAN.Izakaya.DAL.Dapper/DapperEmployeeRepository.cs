using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperEmployeeRepository : IEmployeeRepository
    {
        public void Create(EmployeeEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "INSERT INTO Employees(BranchId,Name,Department,HireDate,Salary,EmployeePassword,Gender) VALUES(@BranchId,@Name,@Department,@HireDate,@Salary,@EmployeePassword,@Gender);";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new
                    {
                        BranchId = entity.BranchId,
                        Name = entity.Name,
                        Department = entity.Department,
                        HireDate = entity.HireDate,
                        Salary = entity.Salary,
                        Gender = entity.Gender,
                        EmployeePassword = entity.EmployeePassword

                    });
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("新增失敗!");
            }
        }
        public void Delete(int id)
        {
            try
            {
                string connsStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "DELETE FROM Employees WHERE Id=@Id;";
                using (var conn = new SqlConnection(connsStr))
                {
                    conn.Execute(sql, new { Id = id });

                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("刪除失敗!");
            }
        }
        public void Edit(EmployeeEntity entity)
        {
            try
            {
                string connsStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "UPDATE Employees SET BranchId=@BranchId,Name=@Name,Department=@Department,HireDate=@HireDate,Salary=@Salary,EmployeePassword=@EmployeePassword ,Gender=@Gender WHERE Id=@Id;";
                using (var conn = new SqlConnection(connsStr))
                {
                    conn.Execute(sql, new
                    {
                        Id = entity.Id,
                        BranchId = entity.BranchId,
                        Name = entity.Name,
                        Department = entity.Department,
                        HireDate = entity.HireDate,
                        Salary = entity.Salary,
                        Gender = entity.Gender,
                        EmployeePassword = entity.EmployeePassword
                    });
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("更新失敗!");
            }
        }
        public EmployeeEntity Get(int id)
        {
            try
            {
                string connsStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT * FROM Employees WHERE Id=@Id;";
                using (var conn = new SqlConnection(connsStr))
                {
                    return conn.QueryFirstOrDefault<EmployeeEntity>(sql, new { Id = id });
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("查詢失敗!");
            }
        }
        public IEnumerable<EmployeeEntity> Search()
        {
            try
            {
                string connsStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT * FROM Employees;";
                using (var conn = new SqlConnection(connsStr))
                {
                    return conn.Query<EmployeeEntity>(sql).ToList();
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("查詢失敗!");
            }
        }
        public EmployeeEntity GetEmployeeByName(string name)
        {
            string connsStr = SqlDb.GetConnectionString("Izakaya");
            string sql = "SELECT Id,BranchId,Name,Department,Salary,HireDate,Gender,EmployeePassword FROM Employees WHERE Name=@Name";

            using (var conn = new SqlConnection(connsStr))
            {
                EmployeeEntity data = conn.QuerySingle<EmployeeEntity>(sql, new { Name = name });
                return data;
            }
        }

        public UserInfoEntity GetEmployeeByAccount(string account)
        {
            string connsStr = SqlDb.GetConnectionString("Izakaya");
            string sql = @"SELECT Id,BranchId,Name,Department as Role 
											FROM Employees
											WHERE Name=@Name";

            using (var conn = new SqlConnection(connsStr))
            {
                UserInfoEntity data = conn.QuerySingle<UserInfoEntity>(sql, new { Name = account });
                return data;
            }
        }

        public List<EmployeeList> GetList()
        {
            string connsStr = SqlDb.GetConnectionString("Izakaya");
            string sql = @"SELECT Id,Name FROM Employees";

            using (var conn = new SqlConnection(connsStr))
            {
                List<EmployeeList> data = conn.Query<EmployeeList>(sql).ToList();
                return data;
            }
        }
    }
}
