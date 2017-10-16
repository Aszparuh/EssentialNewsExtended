using Bytes2you.Validation;
using EssentialNewsMvc.Services.Infrastructure.Contracts;
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
        private readonly ISanitizeService sanitizeService;

        public NewsController(IMediator mediator, ISanitizeService sanitizeService)
        {
            Guard.WhenArgument(mediator, "mediator").IsNull().Throw();
            Guard.WhenArgument(sanitizeService, "sanitize service").IsNull().Throw();
            this.mediator = mediator;
            this.sanitizeService = sanitizeService;
        }

        // GET: News
        public async Task<ActionResult> Details(int id, string name)
        {
            var article = await this.mediator.Send(new NewsDetailsQuery() { Id = id });

            if (article != null && article.Title == name)
            {
                article.Content = sanitizeService.Sanitize(article.Content);
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