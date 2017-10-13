using System.Collections.Generic;

namespace EssentialNewsMvc.Web.ViewModels.Partials
{
    public class AsideViewModel
    {
        public IEnumerable<NewsArticleAsideViewModel> RecentArticles { get; set; }

        public IEnumerable<NewsArticleAsideViewModel> MostVisitedArticles { get; set; }
    }
}