using EssentialNewsMvc.Web.ViewModels.Home;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialNewsMvc.Web.Caching
{
    public interface INewsCacheService
    {
        Task<HomeViewModel> IndexArticles();
    }
}