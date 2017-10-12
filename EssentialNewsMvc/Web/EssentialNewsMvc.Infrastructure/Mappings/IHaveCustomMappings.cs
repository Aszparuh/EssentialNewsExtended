using AutoMapper;

namespace EssentialNewsMvc.Infrastructure.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
