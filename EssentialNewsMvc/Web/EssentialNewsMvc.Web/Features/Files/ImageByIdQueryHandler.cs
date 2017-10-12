using MediatR;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Data;

namespace EssentialNewsMvc.Web.Features.Files
{
    public class ImageByIdQueryHandler : IRequestHandler<ImageByIdQuery, Image>
    {
        private readonly ApplicationDbContext context;

        public ImageByIdQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Image Handle(ImageByIdQuery message)
        {
            return context.Images.Find(message.Id);
        }
    }
}