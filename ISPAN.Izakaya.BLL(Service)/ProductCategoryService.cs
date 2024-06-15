using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class ProductCategoryService
    {
        private readonly IProductCategoryRepository _repo;
        public ProductCategoryService(IProductCategoryRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<ProductCategoryDto> Search(string name)
        {
            return _repo.Search(name).Select(x => ToDto(x));
        }
        public void Create(ProductCategoryDto dto)
        {
            var categoryInDb = _repo.Search(dto.Name);
            if (categoryInDb.Any(X => X.Name.ToLower() == dto.Name.ToLower())) throw new Exception($"{dto.Name}重複取名,已有資料在資料庫!!");
            var entity = ToEntity(dto);
            _repo.Create(entity);
        }
        public void Update(ProductCategoryDto dto)
        {
            var categoryInDb = _repo.Search(dto.Name);
            if (categoryInDb.Any(X => X.Name.ToLower() == dto.Name.ToLower() && X.Id != dto.Id)) throw new Exception($"{dto.Name}重複取名,已有資料在資料庫!!");

            var entity = ToEntity(dto);
            _repo.Update(entity);
        }
        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentNullException($"Id: {id}不可小於1");
            var categoryInDb = _repo.Get(id);
            if (categoryInDb == null) throw new ArgumentNullException("查無該分類,無法刪除");
            _repo.Delete(id);
        }
        public ProductCategoryDto Get(int id)
        {
            if (id <= 0) throw new ArgumentNullException($"Id: {id}不可小於1");
            var entity = _repo.Get(id);
            return ToDto(entity);
        }

        private ProductCategoryEntity ToEntity(ProductCategoryDto dto)
        {
            return new ProductCategoryEntity { Id = dto.Id, Name = dto.Name };
        }
        private ProductCategoryDto ToDto(ProductCategoryEntity entity)
        {
            return new ProductCategoryDto { Id = entity.Id, Name = entity.Name };
        }
    }
}