using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Areas.Administration.Controllers
{
    public class AdministrationController : AdminBaseController
    {
        // GET: Administration/Administration
        public ActionResult Index()
        {
            return View();
        }
    }
}