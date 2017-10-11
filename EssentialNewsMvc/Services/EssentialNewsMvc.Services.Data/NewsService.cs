using EssentialNewsMvc.Data.Common;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Services.Data.Contracts;
using System.Linq;

namespace EssentialNewsMvc.Services.Data
{
    public class NewsService : INewsService
    {
        private readonly IDbRepository<NewsArticle> articles;

        public NewsService(IDbRepository<NewsArticle> articles)
        {
            this.articles = articles;
        }

        public void Add(NewsArticle article)
        {
            this.articles.Add(article);
            this.articles.Save();
        }

        public IQueryable<NewsArticle> GetAllNew()
        {
            return this.articles
                .All()
                .OrderByDescending(a => a.ModifiedOn)
                .ThenByDescending(a => a.CreatedOn);
        }

        public IQueryable<NewsArticle> GetAll()
        {
            return this.articles.All();
        }

        public NewsArticle GetById(int id)
        {
            return this.articles.GetById(id);
        }
    }
}
