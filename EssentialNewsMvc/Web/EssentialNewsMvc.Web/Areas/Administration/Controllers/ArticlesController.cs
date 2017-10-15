using Bytes2you.Validation;
using EssentialNewsMvc.Web.Areas.Administration.Models.Grid;
using EssentialNewsMvc.Web.Features.AdministrationArticles;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Areas.Administration.Controllers
{
    public class ArticlesController : AdminBaseController
    {
        private readonly IMediator mediator;

        public ArticlesController(IMediator mediator)
        {
            Guard.WhenArgument(mediator, "mediator").IsNull().Throw();
            this.mediator = mediator;
        }

        // GET: Administration/Articles
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetArticles(string sidx, string sort, int page, int rows)
        {
            var articlesList = await mediator
                .Send(new ArticlesGridQuery() { Sidx = sidx, Sort = sort, Page = page, Row = rows });

            int totalRecords = articlesList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = articlesList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> Edit(GridArticleViewModel model)
        {
            string msg;
            try
            {
                if (this.ModelState.IsValid)
                {
                    msg = await this.mediator.Send(new EditArticleCommand() { Model = model });
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }

            return msg;
        }

        public async Task<string> Delete(string Id)
        {
            return await this.mediator.Send(new DeleteArticleCommand() { Id = Id });
        }

    }
}