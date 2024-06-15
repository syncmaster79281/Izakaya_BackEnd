using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class ProductStockService
    {
        private readonly IProductStockRepository _repo;
        public ProductStockService(IProductStockRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<ProductStockDto> Search()
        {
            return _repo.Search().Select(x => ToDto(x));
        }
        public void Create(ProductStockDto dto)
        {
            var entity = ToEntity(dto);
            _repo.Create(entity);
        }
        public void Update(ProductStockDto dto)
        {
            var entity = ToEntity(dto);
            _repo.Update(entity);
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        private ProductStockEntity ToEntity(ProductStockDto dto)
        {
            return new ProductStockEntity
            {
                Id = dto.Id,
                Product = new ProductEntity
                {
                    Id = dto.Product.Id,
                    Name = dto.Product.Name
                },
                BranchId = dto.BranchId,
                BranchName = dto.BranchName,
                SafetyStock = dto.SafetyStock,
                Stock = dto.Stock,
                MaxAlertStock = dto.MaxAlertStock,
                ProductCategory = new ProductCategoryEntity
                {
                    Id = dto.ProductCategory.Id,
                    Name = dto.ProductCategory.Name
                }
            };
        }
        private ProductStockDto ToDto(ProductStockEntity entity)
        {
            return new ProductStockDto
            {
                Id = entity.Id,
                Product = new ProductDto
                {
                    Id = entity.Product.Id,
                    Name = entity.Product.Name
                },
                BranchId = entity.BranchId,
                BranchName = entity.BranchName,
                SafetyStock = entity.SafetyStock,
                Stock = entity.Stock,
                MaxAlertStock = entity.MaxAlertStock,
                ProductCategory = new ProductCategoryDto
                {
                    Id = entity.ProductCategory.Id,
                    Name = entity.ProductCategory.Name
                }
            };
        }
    }
}
