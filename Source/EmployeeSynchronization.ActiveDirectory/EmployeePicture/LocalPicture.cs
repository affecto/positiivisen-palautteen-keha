using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture
{
    internal class LocalPicture
    {
        private readonly Image image;

        public LocalPicture(Stream pictureStream)
        {
            image = Image.FromStream(pictureStream);
        }

        public MemoryStream GetResizedPicture(Size newSize)
        {
            Image resizedImage = Resize(newSize);

            var stream = new MemoryStream();
            resizedImage.Save(stream, ImageFormat.Jpeg);

            return stream;
        }

        private Image Resize(Size newSize)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;
            double percentWidth = ((double) newSize.Width) / originalWidth;
            double percentHeight = ((double) newSize.Height) / originalHeight;
            double percent = percentHeight > percentWidth ? percentHeight : percentWidth;

            int newWidth = CalculateNewSize(newSize.Width, originalWidth, percent);
            int newHeight = CalculateNewSize(newSize.Height, originalHeight, percent);

            Image resizedImage = image.Resize(newWidth, newHeight);

            if (newWidth == newSize.Width && newHeight == newSize.Height)
            {
                return resizedImage;
            }

            return Crop(resizedImage, newSize.Width, newSize.Height);
        }

        private static int CalculateNewSize(int targetSize, int originalSize, double percent)
        {
            int newLength = (int) Math.Ceiling(originalSize * percent);
            if (newLength == targetSize + 1)
            {
                newLength = targetSize;
            }

            return newLength;
        }

        private static Image Crop(Image image, int newWidth, int newHeight)
        {
            int x = 0;

            if (image.Width > newWidth)
            {
                int excessWidth = image.Width - newWidth;
                x = excessWidth / 2;
            }

            return image.Crop(newWidth, newHeight, x);
        }
    }
}