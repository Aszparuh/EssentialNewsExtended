using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminBaseController : Controller
    {
    }
}