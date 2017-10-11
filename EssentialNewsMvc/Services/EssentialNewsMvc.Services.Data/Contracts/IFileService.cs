using EssentialNewsMvc.Data.Models;

namespace EssentialNewsMvc.Services.Data.Contracts
{
    public interface IFileService
    {
        Image GetById(int id);
    }
}
