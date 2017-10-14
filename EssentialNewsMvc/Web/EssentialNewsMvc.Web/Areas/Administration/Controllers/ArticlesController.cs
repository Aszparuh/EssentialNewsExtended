using EssentialNewsMvc.Data;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Web.Areas.Administration.Models.Grid;
using System;
using System.Data.Entity;
using System.Linq;
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

        public JsonResult GetArticles(string sidx, string sort, int page, int rows)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            sort = sort ?? "";
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var articlesList = context.NewsArticles.Select(
                    t => new
                    {
                        t.Id,
                        t.Title,
                        t.Content,
                        t.Author.UserName,
                        t.CreatedOn,
                        t.DeletedOn
                    });
            int totalRecords = articlesList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                articlesList = articlesList.OrderByDescending(t => t.CreatedOn);
                articlesList = articlesList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                articlesList = articlesList.OrderBy(t => t.CreatedOn);
                articlesList = articlesList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = articlesList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public string Edit(GridArticleViewModel model)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var article = context.NewsArticles.Find(model.Id);
            article.Title = model.Title;
            article.Content = model.Content;
            article.CreatedOn = model.CreatedOn;
            article.DeletedOn = model.DeletedOn;
            string msg;
            try
            {
                if (this.ModelState.IsValid)
                {
                    context.Entry(article).State = EntityState.Modified;
                    context.SaveChanges();
                    msg = "Saved Successfully";
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

        public string Delete(string Id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            NewsArticle article = context.NewsArticles.Find(Id);
            article.IsDeleted = true;
            context.SaveChanges();
            return "Deleted successfully";
        }

    }
}