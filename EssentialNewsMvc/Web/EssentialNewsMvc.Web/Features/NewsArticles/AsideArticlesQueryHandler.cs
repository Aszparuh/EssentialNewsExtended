using EssentialNewsMvc.Data;
using EssentialNewsMvc.Infrastructure.Mappings;
using EssentialNewsMvc.Web.ViewModels.Partials;
using MediatR;
using System.Linq;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class AsideArticlesQueryHandler : IRequestHandler<AsideArticlesQuery, AsideViewModel>
    {
        private readonly ApplicationDbContext context;

        public AsideArticlesQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public AsideViewModel Handle(AsideArticlesQuery message)
        {
            var mostViewedArticles = this.context.NewsArticles.Where(x => !x.IsDeleted).To<NewsArticleAsideViewModel>().ToList();
            var model = new AsideViewModel()
            {
                MostVisitedArticles = mostViewedArticles,
                RecentArticles = mostViewedArticles
            };

            return model;
        }
    }
}