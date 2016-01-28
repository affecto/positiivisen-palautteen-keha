using System.IO;
using System.Net;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture
{
    internal class RemotePicture
    {
        public static Stream Download(string pictureUrl)
        {
            if (string.IsNullOrWhiteSpace(pictureUrl))
            {
                return null;
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.UseDefaultCredentials = true;
                    byte[] data = webClient.DownloadData(pictureUrl);
                    return new MemoryStream(data);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
