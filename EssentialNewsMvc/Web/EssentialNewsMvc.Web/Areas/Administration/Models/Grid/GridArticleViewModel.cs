using System;

namespace EssentialNewsMvc.Web.Areas.Administration.Models.Grid
{
    public class GridArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}