using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Areas.Administration.Controllers
{
    public class ArticlesController : AdminBaseController
    {
        // GET: Administration/Articles
        public ActionResult Index()
        {
            return View();
        }
    }
}