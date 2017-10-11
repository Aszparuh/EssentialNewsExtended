using System.Web;

namespace EssentialNewsMvc.Services.Infrastructure.Contracts
{
    public interface IImageProcessService
    {
        byte[] ToByteArray(HttpPostedFileBase upload);

        byte[] Resize(byte[] originalImage, int width, int height);
    }
}
