using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            Image newImage = Resize(newSize);

            var stream = new MemoryStream();
            newImage.Save(stream, ImageFormat.Jpeg);

            return stream;
        }

        private Image Resize(Size newSize, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;

            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = ((float) newSize.Width) / originalWidth;
                float percentHeight = ((float) newSize.Height) / originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int) Math.Ceiling(originalWidth * percent);
                newHeight = (int) Math.Ceiling(originalHeight * percent);
            }
            else
            {
                newWidth = newSize.Width;
                newHeight = newSize.Height;
            }

            return CreateNewImage(newWidth, newHeight);
        }

        private Image CreateNewImage(int newWidth, int newHeight)
        {
            Image newImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.High;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
    }
}