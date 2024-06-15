using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class ArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repo)
        {
            _repository = repo;
        }

        public void Create(ArticleDto article)
        {
            _repository.Create(article.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(ArticleDto article)
        {
            _repository.Update(article.ToEntity());
        }

        public List<ArticleDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public ArticleDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }
        public List<ArticleCategoryList> GetCategoriers()
        {
            return _repository.GetCategoriers();
        }
    }
}
