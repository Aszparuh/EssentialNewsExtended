using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System.Collections.Generic;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class NewsArticlesHomeQuery : IRequest<List<NewsArticleIndexViewModel>>
    {
    }
}