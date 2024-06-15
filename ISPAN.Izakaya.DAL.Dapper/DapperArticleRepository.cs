using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperArticleRepository : IArticleRepository
    {
        private readonly string _connStr;
        public DapperArticleRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(ArticleEntity article)
        {
            string sql = "INSERT INTO Articles(EmployeeId,CategoryId,Title,Contents,PublishDate,HideTime,Status,ImageURL)VALUES(@EmployeeId,@CategoryId,@Title,@Contents,@PublishDate,@HideTime,@Status,@ImageURL)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    EmployeeId = article.EmployeeId,
                    CategoryId = article.CategoryId,
                    Title = article.Title,
                    Contents = article.Contents,
                    PublishDate = article.PublishDate,
                    HideTime = article.HideTime,
                    Status = article.Status,
                    ImageURL = article.ImageURL
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Articles WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public ArticleEntity Get(int id)
        {
            string sql = "SELECT Id,EmployeeId,CategoryId,Title,Contents,PublishDate,HideTime,Status,ImageURL FROM Articles WHERE Id=@Id";
            using (var conn = new SqlConnection(_connStr))
            {
                ArticleEntity data = conn.QuerySingle<ArticleEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<ArticleEntity> GetAll()
        {
            string sql = @"SELECT Id,EmployeeId,CategoryId,Title,Contents,PublishDate,HideTime,Status,ImageURL FROM Articles ORDER BY PublishDate";

            using (var conn = new SqlConnection(_connStr))
            {
                List<ArticleEntity> data = conn.Query<ArticleEntity>(sql).ToList();
                return data;
            }
        }

        public List<ArticleCategoryList> GetCategoriers()
        {
            string sql = @"SELECT Id,Category FROM ArticleCategories";

            using (var conn = new SqlConnection(_connStr))
            {
                List<ArticleCategoryList> data = conn.Query<ArticleCategoryList>(sql).ToList();
                return data;
            }
        }

        public void Update(ArticleEntity article)
        {
            string sql = "UPDATE Articles SET EmployeeId=@EmployeeId,CategoryId=@CategoryId,Title=@Title,Contents=@Contents,PublishDate=@PublishDate,HideTime=@HideTime,Status=@Status,ImageURL=@ImageURL WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, article);
            }
        }


    }
}
