using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class NewsDetailsQuery : IRequest<DetailsViewModel>
    {
        public int Id { get; set; }
    }
}