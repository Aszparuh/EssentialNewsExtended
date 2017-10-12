using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.Home;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var news = await GetNewsAsync();
            var viewModel = new HomeViewModel()
            {
                Articles = news,
                TopNews = null
            };

            return View(news);
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

        private async Task<List<NewsArticleIndexViewModel>> GetNewsAsync()
        {
            return await mediator.Send(new NewsArticlesHomeQuery());
        }
    }
}