using System.Drawing;
using System.Drawing.Drawing2D;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture
{
    internal static class ImageExtensions
    {
        public static Image Resize(this Image image, int width, int height)
        {
            Image resizedImage = new Bitmap(width, height);

            using (Graphics graphicsHandle = Graphics.FromImage(resizedImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.High;
                graphicsHandle.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        public static Image Crop(this Image image, int width, int height, int x = 0, int y = 0)
        {
            Rectangle cropArea = new Rectangle(x, y, width, height);
            Image croppedImage = new Bitmap(width, height);

            using (Graphics graphicsHandle = Graphics.FromImage(croppedImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.High;
                graphicsHandle.DrawImage(image, new Rectangle(0, 0, width, height), cropArea, GraphicsUnit.Pixel);
            }

            return croppedImage;
        }
    }
}