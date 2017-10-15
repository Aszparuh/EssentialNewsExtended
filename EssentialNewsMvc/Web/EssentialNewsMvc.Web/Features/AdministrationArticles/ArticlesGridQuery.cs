using EssentialNewsMvc.Web.Areas.Administration.Models.Grid;
using MediatR;
using System.Collections.Generic;

namespace EssentialNewsMvc.Web.Features.AdministrationArticles
{
    public class ArticlesGridQuery : IRequest<List<GridArticleViewModel>>
    {
        public string Sidx { get; set; }

        public string Sort { get; set; }

        public int Page { get; set; }

        public int Row { get; set; }
    }
}