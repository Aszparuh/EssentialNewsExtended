using AutoMapper;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Infrastructure.Mappings;
using System;
using System.Linq;

namespace EssentialNewsMvc.Web.ViewModels.Partials
{
    public class NewsArticleAsideViewModel : IMapFrom<NewsArticle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? SmallThumbnailId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NewsArticle, NewsArticleAsideViewModel>()
                .ForMember(x => x.SmallThumbnailId, opt => opt.MapFrom(
                    x => x.Images.Where(img => img.Type == ImageType.AsideThumbnail)
                    .FirstOrDefault().Id));
        }
    }
}