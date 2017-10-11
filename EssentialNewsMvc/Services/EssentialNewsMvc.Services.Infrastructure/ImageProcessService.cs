using EssentialNewsMvc.Services.Infrastructure.Contracts;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using System.Drawing;
using System.IO;
using System.Web;

namespace EssentialNewsMvc.Services.Infrastructure
{
    public class ImageProcessService : IImageProcessService
    {
        private const int ImageQuality = 70;

        public byte[] Resize(byte[] originalImage, int width, int height)
        {
            using (var originalImageStream = new MemoryStream(originalImage))
            {
                using (var resultImage = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory())
                    {
                        var createdImage = imageFactory
                                .Load(originalImageStream);

                        createdImage = createdImage
                            .Resize(new ResizeLayer(new Size(width, height), ResizeMode.Crop));

                        createdImage
                            .Format(new JpegFormat { Quality = ImageQuality })
                            .Save(resultImage);
                    }

                    return resultImage.GetBuffer();
                }
            }
        }

        public byte[] ToByteArray(HttpPostedFileBase upload)
        {
            byte[] resultArray = null;

            using (var binaryReader = new BinaryReader(upload.InputStream))
            {
                resultArray = binaryReader.ReadBytes(upload.ContentLength);
            }

            return resultArray;
        }
    }
}
