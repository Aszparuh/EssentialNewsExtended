using EssentialNewsMvc.Data;
using EssentialNewsMvc.Infrastructure.Mappings;
using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class NewsArticlesHomeQueryHandler : IRequestHandler<NewsArticlesHomeQuery, List<NewsArticleIndexViewModel>>
    {
        private readonly ApplicationDbContext context;

        public NewsArticlesHomeQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<NewsArticleIndexViewModel> Handle(NewsArticlesHomeQuery message)
        {
            return this.context.NewsArticles
                .Where(x => !x.IsDeleted)
                .Take(10)
                .To<NewsArticleIndexViewModel>()
                .ToList();
        }
    }
}