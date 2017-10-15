using Bytes2you.Validation;
using EssentialNewsMvc.Web.Caching;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string ModelCacheKey = "ViewModel";
        private readonly INewsCacheService newsCacheService;

        public HomeController(INewsCacheService newsCacheService)
        {
            Guard.WhenArgument(newsCacheService, "news cache service").IsNull().Throw();
            this.newsCacheService = newsCacheService;
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = await this.newsCacheService.IndexArticles();
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
    }
}