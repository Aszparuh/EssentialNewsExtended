using EssentialNewsMvc.Data;
using EssentialNewsMvc.Infrastructure.Mappings;
using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System.Data.Entity;
using System.Linq;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class NewsArticlesHomeQueryHandler : IRequestHandler<NewsArticlesHomeQuery, HomeViewModel>
    {
        private readonly ApplicationDbContext context;

        public NewsArticlesHomeQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        HomeViewModel IRequestHandler<NewsArticlesHomeQuery, HomeViewModel>.Handle(NewsArticlesHomeQuery message)
        {
            var allArticles = this.context.NewsArticles
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.CreatedOn)
                .Skip(4)
                .Take(10)
                .To<NewsArticleIndexViewModel>()
                .ToList();
            var carouselArticles = this.context.NewsArticles
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.CreatedOn)
                .Take(4).To<ArticleCarouselViewModel>()
                .ToList();
            HomeViewModel model = new HomeViewModel()
            {
                Articles = allArticles,
                TopNews = carouselArticles
            };

            return model;
        }
    }
}