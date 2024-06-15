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
    public class ProductStockDapperRepository : IProductStockRepository
    {
        public void Create(ProductStockEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "INSERT INTO ProductStocks(ProductId,BranchId,SafetyStock,Stock,MaxAlertStock) VALUES(@ProductId,@BranchId,@SafetyStock,@Stock,@MaxAlertStock);";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new
                    {
                        ProductId = entity.Id,
                        BranchId = entity.BranchId,
                        SafetyStock = entity.SafetyStock,
                        Stock = entity.Stock,
                        MaxAlertStock = entity.MaxAlertStock
                    });
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
                string sql = "DELETE FROM ProductStocks WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new { Id = id });
                }
            }
            catch (Exception)
            {
                throw new Exception("刪除失敗!");
            }
        }

        public IEnumerable<ProductStockEntity> Search()
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT PS.Id, PS.ProductId,P.Name ProductName,PS.BranchId,B.Name BranchName,PS.SafetyStock,PS.Stock,PS.MaxAlertStock,PC.Id CategoryId,PC.Name CategoryName FROM ProductStocks PS JOIN Products P ON PS.ProductId=P.Id JOIN ProductCategories PC ON P.CategoryId=PC.Id JOIN Branches B ON PS.BranchId=B.Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    var query = conn.Query(sql);
                    return query.Select(x => new ProductStockEntity
                    {
                        Id = x.Id,
                        Product = new ProductEntity
                        {
                            Id = x.ProductId,
                            Name = x.ProductName
                        },
                        BranchId = x.BranchId,
                        BranchName = x.BranchName,
                        SafetyStock = x.SafetyStock,
                        Stock = x.Stock,
                        MaxAlertStock = x.MaxAlertStock,
                        ProductCategory = new ProductCategoryEntity
                        {
                            Id = x.CategoryId,
                            Name = x.CategoryName
                        }
                    });
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }

        public void Update(ProductStockEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "UPDATE ProductStocks SET ProductId=@ProductId,BranchId=@BranchId,SafetyStock=@SafetyStock,Stock=@Stock,MaxAlertStock=@MaxAlertStock WHERE Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new
                    {
                        Id = entity.Id,
                        ProductId = entity.Id,
                        BranchId = entity.BranchId,
                        SafetyStock = entity.SafetyStock,
                        Stock = entity.Stock,
                        MaxAlertStock = entity.MaxAlertStock
                    });
                }
            }
            catch (Exception)
            {
                throw new Exception("更新失敗!");
            }
        }
    }
}
