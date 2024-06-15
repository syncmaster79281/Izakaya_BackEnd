using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class ProductCategoryDapperRepository : IProductCategoryRepository
    {
        public void Create(ProductCategoryEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "INSERT INTO ProductCategories(Name) VALUES(@Name);";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new { Name = entity.Name });
                }
            }
            catch (Exception)
            {
                throw new Exception("新增失敗!");
            }
        }

        public void Delete(int id)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "DELETE FROM ProductCategories WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(@"刪除失敗!
請確認該產品分類下沒有商品"
+ ex.Message);
            }
        }

        public ProductCategoryEntity Get(int id)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT Id,Name FROM ProductCategories WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    var query = conn.QueryFirst<ProductCategoryEntity>(sql, new { Id = id });
                    return query;
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }

        public IEnumerable<ProductCategoryEntity> Search(string name)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT Id,Name FROM ProductCategories WHERE Name LIKE '%'+@Name+'%';";
                using (var conn = new SqlConnection(connStr))
                {
                    var query = conn.Query<ProductCategoryEntity>(sql, new { Name = name });
                    return query;
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }

        public void Update(ProductCategoryEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "UPDATE ProductCategories SET Name = @Name WHERE Id = @Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new { Id = entity.Id, Name = entity.Name });
                }
            }
            catch (Exception)
            {
                throw new Exception("更新失敗!");
            }
        }
    }
}
