using EssentialNewsMvc.Data.Models;
using System.Linq;

namespace EssentialNewsMvc.Services.Data.Contracts
{
    public interface INewsService
    {
        IQueryable<NewsArticle> GetAllNew();

        IQueryable<NewsArticle> GetAll();

        NewsArticle GetById(int id);

        void Add(NewsArticle article);
    }
}
