using ISPAN.Izakaya.Dtos.YUMING;
using ISPAN.Izakaya.Entities.YUMING;
using ISPAN.Izakaya.IDAL_IRepo_.YUMING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISPAN.Izakaya.BLL_Service_.CategoryService
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
            return _repo.Search(name).Select(x=>ToDto(x));
        }
        public void Create(ProductCategoryDto dto)
        {
            string categoryname=dto.Name.ToLower();
            var categoryInDb = _repo.Search(categoryname);
            if (categoryInDb.Any(x => x.Name.ToLower() == categoryname))
            {
                throw new ArgumentException($"資料庫已有{categoryname}的分類名稱,請更換為其他名稱");
            }
            var entity=ToEntity(dto);
            _repo.Create(entity);
        }
        public void Update(ProductCategoryDto dto)
        {
            string categoryname=dto.Name.ToLower();
            var categoryInDb=_repo.Search(categoryname);
            if(categoryInDb.Any(x=>x.Name.ToLower() == categoryname && x.Id!=dto.Id))
            {
                throw new ArgumentException($"資料庫已有{categoryname}的分類名稱,請更換為其他名稱");
            }
            var entity = ToEntity(dto);
            _repo.Update(entity);
        }
        public void Delete(int Id)
        {
            var categoryInDb=_repo.Get(Id);
            if(categoryInDb == null)
            {
                throw new ArgumentNullException($"無此筆資料,無法刪除");
            }
            _repo.Delete(Id);
        }
        public ProductCategoryDto Get(int Id)
        {
            var entity=_repo.Get(Id);
            return ToDto(entity);
        }
        private ProductCategoryEntity ToEntity(ProductCategoryDto dto)
        {
            return new ProductCategoryEntity { Id = dto.Id, Name = dto.Name };
        }
        private ProductCategoryDto ToDto(ProductCategoryEntity entity)
        {
            return new ProductCategoryDto { Id = entity.Id, Name = entity.Name};
        }
    }
}
