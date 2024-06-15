using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<ProductDto> Search(string name)
        {
            return _repo.Search(name).Select(x => ToDto(x));
        }
        public List<ProductDropList> GetProducts()
        {
            return _repo.GetProducts();
        }
        public IEnumerable<ProductDto> Search(SearchDataDto dto)
        {
            var entity = new SearchDataEntity
            {
                CategoryId = dto.CategoryId,
                Keyword = dto.Keyword,
                Page = dto.Page,
                PageSize = dto.PageSize,
                SortBy = dto.SortBy,
                SortType = dto.SortType,
                BranchId = dto.BranchId
            };
            var query = _repo.Search(entity);
            return query.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                DisplayOrder = x.DisplayOrder,
                Image = x.Image,
                ImageUrl = x.ImageUrl,
                IsLaunched = x.IsLaunched,
                Present = x.Present,
                UnitPrice = x.UnitPrice,
                ProductCategory = new ProductCategoryDto
                {
                    Id = x.ProductCategory.Id,
                    Name = x.ProductCategory.Name
                }
            });
        }
        public void Create(ProductDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));
            if (dto.ProductCategory.Id <= 0) throw new ArgumentOutOfRangeException(nameof(dto.ProductCategory.Id));
            if (dto.UnitPrice <= 0) throw new ArgumentOutOfRangeException($"商品價格 $ {dto.UnitPrice} 不能小於0");
            if (string.IsNullOrEmpty(dto.Image))
            {
                dto.Image = "5aPfiji";
                dto.ImageUrl = "https://imgur.com/5aPfiji.jpeg";
            }
            if (dto.DisplayOrder <= 0) throw new ArgumentOutOfRangeException($"排序 {dto.DisplayOrder}不可小於0");
            var productsInDb = _repo.Search(dto.Name);
            if (productsInDb.Any(X => X.Name.ToLower() == dto.Name.ToLower())) throw new Exception($"{dto.Name}重複取名,已有資料在資料庫!!");
            if (dto.ProductCategory.Id <= 0) throw new ArgumentOutOfRangeException($"此Id: {dto.ProductCategory.Id}在資料庫找不到,對應的分類名稱為:{dto.ProductCategory.Name}");
            var entity = ToEntity(dto);
            _repo.Create(entity);
        }
        public void Update(ProductDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentNullException($"此Id: {dto.Id} 沒有相對應的商品!!");
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));
            if (dto.Name.Length > 20) throw new ArgumentOutOfRangeException($"商品名稱的長度: {dto.Name.Length} 不可超過20");
            if (dto.ProductCategory.Id <= 0) throw new ArgumentOutOfRangeException(nameof(dto.ProductCategory.Id));
            if (dto.UnitPrice <= 0) throw new ArgumentOutOfRangeException($"商品價格 $ {dto.UnitPrice} 不能小於0");
            if (dto.DisplayOrder <= 0) throw new ArgumentOutOfRangeException($"排序 {dto.DisplayOrder}不可小於0");
            if (dto.Present != null)
            {
                if (dto.Present.Length > 200) throw new ArgumentOutOfRangeException($"商品介紹的長度: {dto.Present.Length} 不可超過200");
            }
            if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
            {
                var productInDb = _repo.Get(dto.Id);
                dto.ImageUrl = productInDb.ImageUrl;
            }
            if (dto.Image == null || dto.Image.Length == 0)
            {
                var productInDb = _repo.Get(dto.Id);
                dto.Image = productInDb.Image;
            }
            var productsInDb = _repo.Search(dto.Name);
            if (productsInDb.Any(X => X.Name.ToLower() == dto.Name.ToLower() && X.Id != dto.Id)) throw new Exception($"{dto.Name}重複取名,已有資料在資料庫!!");
            var entity = ToEntity(dto);
            _repo.Update(entity);
        }
        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentNullException($"Id: {id}不可小於1");
            var productInDb = _repo.Get(id);
            if (productInDb == null) throw new ArgumentNullException("查無該分類,無法刪除");
            _repo.Delete(id);
        }
        public int GetCount(SearchDataDto dto)
        {
            var entity = new SearchDataEntity
            {
                CategoryId = dto.CategoryId,
                Keyword = dto.Keyword,
                Page = dto.Page,
                PageSize = dto.PageSize,
                SortBy = dto.SortBy,
                SortType = dto.SortType
            };
            return _repo.GetCount(entity);
        }
        public ProductDto Get(int id)
        {
            if (id <= 0) throw new ArgumentNullException($"Id: {id}不可小於1");
            var query = _repo.Get(id);
            if (query == null) throw new ArgumentNullException(nameof(query));
            return ToDto(query);
        }
        public IEnumerable<ProductDto> GetProductsInSameCategory(int id)
        {
            var query = _repo.GetProductsInSameCategory(id).Select(x => new ProductDto { Id = x.Id, Name = x.Name, UnitPrice = x.UnitPrice });
            return query;
        }

        private ProductEntity ToEntity(ProductDto dto)
        {
            return new ProductEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                UnitPrice = dto.UnitPrice,
                Image = dto.Image,
                ImageUrl = dto.ImageUrl,
                Present = dto.Present,
                DisplayOrder = dto.DisplayOrder,
                IsLaunched = dto.IsLaunched,
                ProductCategory = new ProductCategoryEntity
                {
                    Id = dto.ProductCategory.Id,
                    Name = dto.ProductCategory.Name
                }
            };
        }
        private ProductDto ToDto(ProductEntity entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                UnitPrice = entity.UnitPrice,
                Image = entity.Image,
                ImageUrl = entity.ImageUrl,
                Present = entity.Present,
                DisplayOrder = entity.DisplayOrder,
                IsLaunched = entity.IsLaunched,
                ProductCategory = new ProductCategoryDto
                {
                    Id = entity.ProductCategory.Id,
                    Name = entity.ProductCategory.Name
                }
            };
        }

    }
}
