using EssentialNewsMvc.Data.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EssentialNewsMvc.Data.Models
{
    public class NewsCategory : BaseModel<int>
    {
        public NewsCategory()
        {
            this.Articles = new HashSet<NewsArticle>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<NewsArticle> Articles { get; set; }
    }
}
