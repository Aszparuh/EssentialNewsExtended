using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class NewsArticlesHomeQuery : IRequest<HomeViewModel>
    {
    }
}