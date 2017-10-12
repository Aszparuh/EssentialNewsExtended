using System.Collections.Generic;

namespace EssentialNewsMvc.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<NewsArticleIndexViewModel> Articles { get; set; }

        public IEnumerable<ArticleCarouselViewModel> TopNews { get; set; }
    }
}