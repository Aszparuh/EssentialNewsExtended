using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Web.Features.Files;
using MediatR;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EssentialNewsMvc.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IMediator mediator;

        public FileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: File
        public async Task<ActionResult> Index(int id)
        {
            var fileToRetrieve = await this.mediator.Send(new ImageByIdQuery() { Id = id });
            if (this.CheckStatus304(fileToRetrieve))
            {
                return this.Content(string.Empty);
            }

            return this.File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        private bool CheckStatus304(Image image)
        {
            // http://weblogs.asp.net/jeff/304-your-images-from-a-database
            DateTime lastModified;
            if (image.ModifiedOn == null)
            {
                lastModified = image.CreatedOn;
            }
            else
            {
                lastModified = (DateTime)image.ModifiedOn;
            }

            if (!string.IsNullOrEmpty(this.Request.Headers["If-Modified-Since"]))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var lastMod = DateTime.ParseExact(this.Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();
                if (lastMod == lastModified.AddMilliseconds(-lastModified.Millisecond))
                {
                    this.Response.StatusCode = 304;
                    this.Response.StatusDescription = "Not Modified";
                    return true;
                }
            }

            this.Response.Cache.SetCacheability(HttpCacheability.Public);
            this.Response.Cache.SetLastModified(lastModified);

            return false;
        }
    }
}