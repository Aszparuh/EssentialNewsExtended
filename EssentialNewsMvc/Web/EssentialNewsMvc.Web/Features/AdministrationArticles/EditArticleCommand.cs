using EssentialNewsMvc.Web.Areas.Administration.Models.Grid;
using MediatR;

namespace EssentialNewsMvc.Web.Features.AdministrationArticles
{
    public class EditArticleCommand : IRequest<string>
    {
        public GridArticleViewModel Model { get; set; }
    }
}