using EssentialNewsMvc.Data.Common;
using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Services.Data.Contracts;

namespace EssentialNewsMvc.Services.Data
{
    public class FileService : IFileService
    {
        private readonly IDbRepository<Image> images;

        public FileService(IDbRepository<Image> images)
        {
            this.images = images;
        }

        public Image GetById(int id)
        {
            return this.images.GetById(id);
        }
    }
}
