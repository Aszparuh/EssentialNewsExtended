using EssentialNewsMvc.Data;
using EssentialNewsMvc.Web.Features.AdministrationArticles;
using MediatR;
using System;

namespace EssentialNewsMvc.Web.Features
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, string>
    {
        private readonly ApplicationDbContext context;

        public DeleteArticleCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string Handle(DeleteArticleCommand message)
        {
            int articleId = int.Parse(message.Id);
            var article = this.context.NewsArticles.Find(articleId);
            article.IsDeleted = true;
            article.DeletedOn = DateTime.Now;
            context.SaveChanges();
            return "Deleted successfully";
        }
    }
}