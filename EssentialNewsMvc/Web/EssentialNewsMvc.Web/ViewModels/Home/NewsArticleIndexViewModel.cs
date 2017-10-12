using AutoMapper;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Infrastructure.Mappings;
using System;
using System.Linq;

namespace EssentialNewsMvc.Web.ViewModels.Home
{
    public class NewsArticleIndexViewModel : IMapFrom<NewsArticle>, IMapTo<NewsArticle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string SampleContent { get; set; }

        public string UserName { get; set; }

        public int? ThumbnailId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NewsArticle, NewsArticleIndexViewModel>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.ThumbnailId, opt => opt.MapFrom(x => x.Images.Where(img => img.Type == ImageType.Thumbnail).FirstOrDefault().Id));
        }
    }
}