using EssentialNewsMvc.Web.Features.News;
using EssentialNewsMvc.Web.Features.NewsArticles;
using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly IMediator mediator;

        public NewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: News
        public async Task<ActionResult> Details(int id, string name)
        {
            var article = await this.mediator.Send(new NewsDetailsQuery() { Id = id });

            if (article != null && article.Title == name)
            {
                return this.View(article);
            }
            else
            {
                return new HttpNotFoundResult("Article not found");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Journalist")]
        public async Task<ActionResult> Create()
        {
            var model = await this.mediator.Send(new CategoriesRegionsQuery());
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Journalist")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewsViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                this.mediator.Send(new CreateArticleCommand() { Model = inputModel, UserId = userId });
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(inputModel);
        }
    }
}