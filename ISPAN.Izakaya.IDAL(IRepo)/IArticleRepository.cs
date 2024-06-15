using ISPAN.Izakaya.Entities;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IArticleRepository
    {
        void Create(ArticleEntity article);
        void Delete(int id);
        List<ArticleEntity> GetAll();
        ArticleEntity Get(int id);

        void Update(ArticleEntity article);
        List<ArticleCategoryList> GetCategoriers();
    }
}
