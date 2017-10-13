using AutoMapper;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Infrastructure.Mappings;
using System;
using System.Linq;

namespace EssentialNewsMvc.Web.ViewModels.News
{
    public class DetailsViewModel : IMapFrom<NewsArticle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public int? ImageId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NewsArticle, DetailsViewModel>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.ImageId, opt => opt.MapFrom(
                    x => x.Images.Where(img => img.Type == ImageType.Normal)
                    .FirstOrDefault().Id));
        }
    }
}