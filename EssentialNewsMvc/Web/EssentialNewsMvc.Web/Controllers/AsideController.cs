using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.Partials;
using MediatR;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Controllers
{
    public class AsideController : Controller
    {
        private readonly IMediator mediator;

        public AsideController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public async Task<ActionResult> Index()
        {
            var asideNews = await GetAsideArticles();
            return this.PartialView("_AsidePartial", asideNews);
        }

        private async Task<AsideViewModel> GetAsideArticles()
        {
            return await this.mediator.Send(new AsideArticlesQuery());
        }
    }
}