using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class CreateArticleCommand : IRequest
    {
        public CreateNewsViewModel Model { get; set; }

        public string UserId { get; set; }
    }
}