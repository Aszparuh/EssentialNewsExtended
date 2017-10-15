using MediatR;

namespace EssentialNewsMvc.Web.Features.AdministrationArticles
{
    public class DeleteArticleCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}