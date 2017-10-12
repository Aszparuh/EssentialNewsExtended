using MediatR;
using EssentialNewsMvc.Data.Models;

namespace EssentialNewsMvc.Web.Features.Files
{
    public class ImageByIdQuery : IRequest<Image>
    {
        public int Id { get; set; }
    }
}