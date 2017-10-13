using AutoMapper;
using EssentialNewsMvc.Data;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Infrastructure.Mappings;
using EssentialNewsMvc.Services.Infrastructure.Contracts;
using MediatR;

namespace EssentialNewsMvc.Web.Features.NewsArticles
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly IImageProcessService imageProcessService;

        public CreateArticleCommandHandler(ApplicationDbContext context, IImageProcessService imageProcessService)
        {
            this.context = context;
            this.imageProcessService = imageProcessService;
        }
        public void Handle(CreateArticleCommand message)
        {
            var modelToSave = this.Mapper.Map<NewsArticle>(message.Model);
            modelToSave.ApplicationUserId = message.UserId;

            var originalImageContent = this.imageProcessService.ToByteArray(message.Model.Upload);
            var thumbnailImageContent = this.imageProcessService.Resize(originalImageContent, 260, 180);
            var qualityImageContent = this.imageProcessService.Resize(originalImageContent, 640, 360);
            var asideThumbnailImageContent = this.imageProcessService.Resize(originalImageContent, 141, 106);
            var name = message.Model.Upload.FileName;
            var contentType = message.Model.Upload.ContentType;

            modelToSave.Images.Add(
                new Image()
                {
                    FileName = name,
                    ContentType = contentType,
                    Content = originalImageContent,
                    Type = ImageType.Original
                });

            modelToSave.Images.Add(
                new Image()
                {
                    FileName = name,
                    ContentType = "image/jpeg",
                    Content = thumbnailImageContent,
                    Type = ImageType.Thumbnail
                });

            modelToSave.Images.Add(
                new Image()
                {
                    FileName = name,
                    ContentType = "image/jpeg",
                    Content = qualityImageContent,
                    Type = ImageType.Normal
                });

            modelToSave.Images.Add(
                new Image()
                {
                    FileName = name,
                    ContentType = "image/jpeg",
                    Content = asideThumbnailImageContent,
                    Type = ImageType.AsideThumbnail
                });

            this.context.NewsArticles.Add(modelToSave);
            this.context.SaveChanges();
        }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}