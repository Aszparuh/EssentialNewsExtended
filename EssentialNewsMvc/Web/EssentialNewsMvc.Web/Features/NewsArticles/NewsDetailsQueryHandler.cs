using EssentialNewsMvc.Data;
using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;
using EssentialNewsMvc.Infrastructure.Mappings;
using System.Linq;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class NewsDetailsQueryHandler : IRequestHandler<NewsDetailsQuery, DetailsViewModel>
    {
        private readonly ApplicationDbContext context;

        public NewsDetailsQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }
        public DetailsViewModel Handle(NewsDetailsQuery message)
        {
            var model = this.context.NewsArticles
                .Where(x => x.Id == message.Id)
                .To<DetailsViewModel>()
                .Single();
            return model;
        }
    }
}