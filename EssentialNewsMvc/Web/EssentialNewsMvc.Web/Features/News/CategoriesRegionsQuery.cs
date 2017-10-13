using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;

namespace EssentialNewsMvc.Web.Features.News
{
    public class CategoriesRegionsQuery : IRequest<CreateNewsViewModel>
    {
    }
}