using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class ProductDapperRepository : IProductRepository
    {
        public void Create(ProductEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = " INSERT INTO Products(Name,CategoryId,UnitPrice,Image,ImageUrl,Present,DisplayOrder,IsLaunched)Values(@Name,@CategoryId,@UnitPrice,@Image,@ImageUrl,@Present,@DisplayOrder,@IsLaunched);";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, GetDbString(entity));
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
                string sql = "DELETE FROM Products WHERE Id=@Id;";
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

        public ProductEntity Get(int id)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT P.Id,P.Name,P.CategoryId,PC.Name CategoryName, P.UnitPrice,P.Image,P.ImageUrl,P.Present,P.DisplayOrder,P.IsLaunched FROM ProductCategories PC JOIN Products P ON PC.Id=P.CategoryId WHERE P.Id=@Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    var query = conn.QueryFirstOrDefault<Product>(sql, new { Id = id });

                    return new ProductEntity
                    {
                        Id = query.Id,
                        Name = query.Name,
                        UnitPrice = query.UnitPrice,
                        Image = query.Image,
                        ImageUrl = query.ImageUrl,
                        Present = query.Present,
                        DisplayOrder = query.DisplayOrder,
                        IsLaunched = query.IsLaunched,
                        ProductCategory = new ProductCategoryEntity
                        {
                            Id = query.CategoryId,
                            Name = query.CategoryName
                        }
                    };
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }
        public List<ProductDropList> GetProducts()
        {
            string connStr = SqlDb.GetConnectionString("Izakaya");
            string sql = "SELECT Id,Name From Products";
            using (var conn = new SqlConnection(connStr))
            {
                List<ProductDropList> data = conn.Query<ProductDropList>(sql).ToList();
                return data;
            }
        }
        public int GetCount(SearchDataEntity entity)
        {
            string connStr = SqlDb.GetConnectionString("Izakaya");
            string sql = "SELECT COUNT(*) From Products P Join ProductCategories PC ON P.CategoryId=PC.Id ";
            int count = 0;
            if (entity.CategoryId > 0)
            {
                sql += "WHERE P.CategoryId=@CategoryId ";
                count++;
            }
            if (!string.IsNullOrEmpty(entity.Keyword))
            {
                if (count == 0)
                {
                    sql += "WHERE (P.Name LIKE '%'+@Keyword+'%' OR PC.Name LIKE '%'+@Keyword+'%' )";
                    count++;
                }
                else
                {
                    sql += "AND (P.Name LIKE '%'+@Keyword+'%' OR PC.Name LIKE '%'+@Keyword+'%' )";
                }
            }

            using (var conn = new SqlConnection(connStr))
            {
                var query = conn.QueryFirst<int>(sql, new
                {
                    CategoryId = entity.CategoryId,
                    Keyword = entity.Keyword,
                });

                return query;
            }
        }

        public IEnumerable<ProductEntity> GetProductsInSameCategory(int id)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT Id,Name,UnitPrice,ImageUrl,Present,DisplayOrder,IsLaunched From Products WHERE CategoryId=@CategoryId;";
                using (var conn = new SqlConnection(connStr))
                {
                    var query = conn.Query(sql, new { CategoryId = id });
                    return query.Select(x => new ProductEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UnitPrice = x.UnitPrice,
                        DisplayOrder = x.DisplayOrder,
                        Image = x.ImageUrl,
                        IsLaunched = x.IsLaunched,
                        Present = x.Present,
                    });
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }

        public IEnumerable<ProductEntity> Search(string name)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "SELECT P.Id,P.Name,P.CategoryId,P.UnitPrice,P.Image,P.ImageUrl,P.Present,P.DisplayOrder,P.IsLaunched,PC.Id CategoryId,PC.Name CategoryName From Products P Join ProductCategories PC ON P.CategoryId=PC.Id WHERE P.Name LIKE '%'+@Name+'%';";
                using (var conn = new SqlConnection(connStr))
                {
                    var query = conn.Query(sql, new { Name = name });
                    var result = query.Select(x => new ProductEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UnitPrice = x.UnitPrice,
                        DisplayOrder = x.DisplayOrder,
                        Image = x.Image,
                        ImageUrl = x.ImageUrl,
                        IsLaunched = x.IsLaunched,
                        Present = x.Present,
                        ProductCategory = new ProductCategoryEntity
                        {
                            Id = x.CategoryId,
                            Name = x.CategoryName,
                        }
                    }).ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }

        public IEnumerable<ProductEntity> Search(SearchDataEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql;
                if (entity.BranchId == 0)
                {
                    sql = "SELECT P.Id Id,P.Name Name,P.CategoryId CategoryId,P.UnitPrice UnitPrice,P.Image,P.ImageUrl,P.Present,P.DisplayOrder DisplayOrder,P.IsLaunched IsLaunched,PC.Name CategoryName From Products P Join ProductCategories PC ON P.CategoryId=PC.Id ";
                }
                else
                {
                    sql = "SELECT P.Id Id,P.Name Name,P.CategoryId CategoryId,P.UnitPrice UnitPrice,P.ImageUrl,P.Present,P.DisplayOrder DisplayOrder,P.IsLaunched IsLaunched,PC.Name CategoryName, PS.BranchId BranchId From Products P Join ProductCategories PC ON P.CategoryId=PC.Id Join ProductStocks PS ON P.Id=PS.ProductId ";
                }
                int count = 0;
                if (entity.CategoryId > 0)
                {
                    sql += "WHERE P.CategoryId=@CategoryId ";
                    count++;
                }
                if (!string.IsNullOrEmpty(entity.Keyword))
                {
                    if (count == 0)
                    {
                        sql += "WHERE (P.Name LIKE '%'+@Keyword+'%' OR PC.Name LIKE '%'+@Keyword+'%' ) ";
                        count++;
                    }
                    else
                    {
                        sql += "AND (P.Name LIKE '%'+@Keyword+'%' OR PC.Name LIKE '%'+@Keyword+'%' ) ";
                    }
                }
                if (entity.BranchId != 0)
                {
                    if (count == 0)
                    {
                        sql += "WHERE PS.BranchId =@BranchId ";
                    }
                    else
                    {
                        sql += "AND PS.BranchId =@BranchId ";
                    }
                }

                int skipRow = entity.Page == 1 ? 0 : (entity.Page - 1) * entity.PageSize;

                sql += $" ORDER BY {entity.SortType} {entity.SortBy} OFFSET {skipRow} ROWS FETCH FIRST {entity.PageSize} ROWS ONLY; ";

                using (var conn = new SqlConnection(connStr))
                {
                    object searchDbString;
                    if (entity.CategoryId > 0 && entity.BranchId != 0)
                    {
                        searchDbString = new
                        {
                            CategoryId = entity.CategoryId,
                            Keyword = new DbString
                            {
                                Value = entity.Keyword,
                                IsAnsi = false,
                                Length = 20
                            },
                            BranchId = entity.BranchId
                        };
                    }
                    else if (entity.CategoryId > 0 && entity.BranchId == 0)
                    {
                        searchDbString = new
                        {
                            CategoryId = entity.CategoryId,
                            Keyword = new DbString
                            {
                                Value = entity.Keyword,
                                IsAnsi = false,
                                Length = 20
                            }
                        };
                    }
                    else if (entity.CategoryId == 0 && entity.BranchId != 0)
                    {
                        searchDbString = new
                        {
                            Keyword = new DbString
                            {
                                Value = entity.Keyword,
                                IsAnsi = false,
                                Length = 20
                            },
                            BranchId = entity.BranchId
                        };
                    }
                    else
                    {
                        searchDbString = new
                        {
                            Keyword = entity.Keyword,
                        };

                    }
                    var query = conn.Query(sql, searchDbString);

                    var datas = query.Select(x => new ProductEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UnitPrice = x.UnitPrice,
                        DisplayOrder = x.DisplayOrder,
                        ImageUrl = x.ImageUrl,
                        Image = x.Image,
                        IsLaunched = x.IsLaunched ?? true,
                        Present = x.Present,
                        ProductCategory = new ProductCategoryEntity
                        {
                            Id = x.CategoryId,
                            Name = x.CategoryName,
                        }
                    });
                    return datas;
                }
            }
            catch (Exception)
            {
                throw new Exception("查無資料!");
            }
        }

        public void Update(ProductEntity entity)
        {
            try
            {
                string connStr = SqlDb.GetConnectionString("Izakaya");
                string sql = "UPDATE Products SET Name = @Name,CategoryId=@CategoryId ,Unitprice=@UnitPrice,Image=@Image, ImageUrl=@ImageUrl , Present=@Present, DisplayOrder=@DisplayOrder,IsLaunched=@IsLaunched WHERE Id = @Id;";
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sql, new
                    {
                        Id = entity.Id,
                        Name = new DbString { Value = entity.Name, IsAnsi = false, Length = 20 },
                        CategoryId = entity.ProductCategory.Id,
                        UnitPrice = entity.UnitPrice,
                        Image = new DbString { Value = entity.Image, IsAnsi = false, Length = 200 },
                        ImageUrl = new DbString { Value = entity.ImageUrl, IsAnsi = false, Length = 200 },
                        Present = new DbString { Value = entity.Present, IsAnsi = false, Length = 200 },
                        DisplayOrder = entity.DisplayOrder,
                        IsLaunched = entity.IsLaunched
                    });
                }
            }
            catch (Exception)
            {
                throw new Exception("更新失敗!");
            }
        }

        private object GetDbString(ProductEntity entity)
        {
            return new
            {
                Name = new DbString { Value = entity.Name, IsAnsi = false, Length = 20 },
                CategoryId = entity.ProductCategory.Id,
                UnitPrice = entity.UnitPrice,
                Image = new DbString { Value = entity.Image, IsAnsi = false, Length = 200 },
                ImageUrl = new DbString { Value = entity.ImageUrl, IsAnsi = false, Length = 200 },
                Present = new DbString { Value = entity.Present, IsAnsi = false, Length = 200 },
                DisplayOrder = entity.DisplayOrder,
                IsLaunched = entity.IsLaunched
            };
        }
    }
}
