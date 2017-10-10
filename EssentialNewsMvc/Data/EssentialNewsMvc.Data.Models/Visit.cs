using EssentialNewsMvc.Data.Common.Models;

namespace EssentialNewsMvc.Data.Models
{
    public class Visit : BaseModel<int>
    {
        public int NewsArticleId { get; set; }

        public virtual NewsArticle NewsArticle { get; set; }
    }
}
