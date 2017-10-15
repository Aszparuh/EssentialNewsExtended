using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System.Threading.Tasks;

namespace EssentialNewsMvc.Web.Caching
{
    public class NewsCacheService : BaseCacheService, INewsCacheService
    {
        private const string LatestNewsKey = "LatestNews";
        private readonly IMediator mediator;

        public NewsCacheService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<HomeViewModel> IndexArticles()
        {
            return await this.Get<HomeViewModel>(
                LatestNewsKey,
                async () => await this.mediator.Send(new NewsArticlesHomeQuery()), 
                this.DefaultAbsoluteExpiration);
        }
    }
}