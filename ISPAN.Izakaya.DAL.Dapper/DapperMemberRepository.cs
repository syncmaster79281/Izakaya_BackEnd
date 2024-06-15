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
    public class DapperMemberRepository : IMemberRepository
    {
        public void Create(MemberEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "INSERT INTO Members(Name,Account,Password,Phone,Email,Points,AuthenticatioCode,Birthday) VALUES(@Name,@Account,@Password,@Phone,@Email,@Points,@AuthenticatioCode,@Birthday);";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new
                    {
                        Name = entity.Name,
                        Account = entity.Account,
                        Password = entity.Password,
                        Phone = entity.Phone,
                        Email = entity.Email,
                        Points = entity.Points,
                        Birthday = entity.Birthday,
                        AuthenticatioCode = entity.AuthenticatioCode,
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
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "DELETE FROM Members WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("刪除錯誤違反SQL關聯鍵", ex);
            }
        }

        public IEnumerable<MemberEntity> Search()
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT * FROM Members;";
                using (var conn = new SqlConnection(connStr))
                {
                    return conn.Query<MemberEntity>(sql).ToList();
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("查詢失敗!");
            }
        }

        public MemberEntity Get(int id)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT * FROM Members WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    return conn.QueryFirstOrDefault<MemberEntity>(sql, new { Id = id });
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("查詢失敗!");
            }
        }
        public void Edit(MemberEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "UPDATE Members SET Name=@Name,Account=@Account,Password=@Password,Phone=@Phone,Email=@Email,Points=@Points,AuthenticatioCode=@AuthenticatioCode,Birthday=@Birthday WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Account = entity.Account,
                        Password = entity.Password,
                        Phone = entity.Phone,
                        Email = entity.Email,
                        Points = entity.Points,
                        AuthenticatioCode = entity.AuthenticatioCode,
                        Birthday = entity.Birthday
                    });
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("更新失敗!");
            }
        }
    }
}
