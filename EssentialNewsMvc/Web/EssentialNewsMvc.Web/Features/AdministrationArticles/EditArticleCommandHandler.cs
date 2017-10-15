using EssentialNewsMvc.Data;
using MediatR;
using System.Data.Entity;

namespace EssentialNewsMvc.Web.Features.AdministrationArticles
{
    public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, string>
    {
        private readonly ApplicationDbContext context;

        public EditArticleCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }
        public string Handle(EditArticleCommand message)
        {
            var article = context.NewsArticles.Find(message.Model.Id);
            article.Title = message.Model.Title;
            article.Content = message.Model.Content;
            article.CreatedOn = message.Model.CreatedOn;
            article.DeletedOn = message.Model.DeletedOn;
            article.IsDeleted = message.Model.IsDeleted;
            string msg;
            context.Entry(article).State = EntityState.Modified;
            context.SaveChanges();
            msg = "Saved Successfully";
            return msg;
        }
    }
}