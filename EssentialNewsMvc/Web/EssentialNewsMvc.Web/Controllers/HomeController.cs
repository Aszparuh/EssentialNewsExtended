using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string ModelCacheKey = "ViewModel";
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            if (this.HttpContext.Cache[ModelCacheKey] == null)
            {
                var news = await GetNewsAsync();
                this.HttpContext.Cache.Insert(ModelCacheKey, news, null, DateTime.Now.AddSeconds(30), TimeSpan.Zero);
            }
            var viewModel = this.HttpContext.Cache[ModelCacheKey];
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private async Task<HomeViewModel> GetNewsAsync()
        {
            return await mediator.Send(new NewsArticlesHomeQuery());
        }
    }
}