using EssentialNewsMvc.Data;
using EssentialNewsMvc.Web.Areas.Administration.Models.Grid;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EssentialNewsMvc.Web.Features.AdministrationArticles
{
    public class ArticlesGridQueryHandler : IRequestHandler<ArticlesGridQuery, List<GridArticleViewModel>>
    {
        private readonly ApplicationDbContext context;

        public ArticlesGridQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<GridArticleViewModel> Handle(ArticlesGridQuery message)
        {
            message.Sort = message.Sort ?? "";
            int pageIndex = Convert.ToInt32(message.Page) - 1;
            int pageSize = message.Row;

            var articlesList = context.NewsArticles.Select(
                    t => new GridArticleViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Content = t.Content,
                        UserName = t.Author.UserName,
                        CreatedOn = t.CreatedOn,
                        DeletedOn = t.DeletedOn,
                        IsDeleted = t.IsDeleted
                    });
            int totalRecords = articlesList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)message.Row);
            if (message.Sort.ToUpper() == "DESC")
            {
                articlesList = articlesList.OrderByDescending(t => t.CreatedOn);
                articlesList = articlesList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                articlesList = articlesList.OrderBy(t => t.CreatedOn);
                articlesList = articlesList.Skip(pageIndex * pageSize).Take(pageSize);
            }

            return articlesList.ToList();
        }
    }
}