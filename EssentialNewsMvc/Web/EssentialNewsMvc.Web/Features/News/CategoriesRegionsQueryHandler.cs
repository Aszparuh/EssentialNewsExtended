using EssentialNewsMvc.Data;
using EssentialNewsMvc.Web.ViewModels.News;
using MediatR;
using System.Linq;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Features.News
{
    public class CategoriesRegionsQueryHandler : IRequestHandler<CategoriesRegionsQuery, CreateNewsViewModel>
    {
        private readonly ApplicationDbContext context;

        public CategoriesRegionsQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public CreateNewsViewModel Handle(CategoriesRegionsQuery message)
        {
            var categories = this.context.Categories
                .Where(x => !x.IsDeleted)
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
            var regions = this.context.Regions
                .Where(x => !x.IsDeleted)
                .Select(r => new SelectListItem() { Text = r.Name, Value = r.Id.ToString() }).ToList();
            var model = new CreateNewsViewModel()
            {
                NewsCategories = categories,
                Regions = regions
            };

            return model;
        }
    }
}