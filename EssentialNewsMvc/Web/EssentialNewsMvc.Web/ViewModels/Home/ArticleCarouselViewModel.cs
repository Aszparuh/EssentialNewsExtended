using AutoMapper;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Infrastructure.Mappings;
using System.Linq;

namespace EssentialNewsMvc.Web.ViewModels.Home
{
    public class ArticleCarouselViewModel : IMapFrom<NewsArticle>, IMapTo<NewsArticle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? ImageId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NewsArticle, ArticleCarouselViewModel>()
                .ForMember(x => x.ImageId, opt => opt.MapFrom(x => x.Images.Where(img => img.Type == ImageType.Normal)
                .FirstOrDefault().Id));
        }
    }
}