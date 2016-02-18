using System.Drawing;
using System.IO;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture
{
    internal class PictureHandler
    {
        public virtual byte[] DownloadAndResizePicture(string pictureUrl)
        {
            const int pictureWidth = 200;
            const int pictureHeight = 267;

            byte[] picture = null;
            using (Stream stream = RemotePicture.Download(pictureUrl))
            {
                if (stream != null)
                {
                    LocalPicture originalPicture = new LocalPicture(stream);
                    using (MemoryStream resizedPicture = originalPicture.GetResizedPicture(new Size(pictureWidth, pictureHeight)))
                    {
                        picture = resizedPicture.ToArray();
                    }
                }
            }
            return picture;
        }
    }
}
