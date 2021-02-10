using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DeleteAdsCShape
{
    public class PostAd
    {
        public static void PostAdvertisement(string data)
        {
            
            HttpWebRequest request = WebRequest.Create(data) as HttpWebRequest;

            // Set up the request properties.
            request.Method = "DELETE";
            request.Host = "ecg-api.gumtree.com.au";
            request.Headers["Authorization"] = Configuration.Authorization;
            request.Accept = "*/*";
            request.Headers["X-ECG-VER"] = "1.49";
            request.Headers["X-ECG-AB-TEST-GROUP"] = "GROUP_50;gblandroid_6959_d";
            request.Headers["Accept-Encoding"] = "gzip, deflate";
            request.Headers["X-ECG-UDID"] = Configuration.X_ECG_UDID;
            request.Headers["X-ECG-Authorization-User"] = "id=" + Configuration.AccountId + ", token=" + Configuration.Token;
            request.Headers["Accept-Language"] = "en-AU";
            
            request.UserAgent = "Gumtree 12.0.0 (iPhone; iOS 12.2; en_AU)";
            request.KeepAlive = false;
            request.Headers["X-ECG-Original-MachineId"] = Configuration.MachineId;
            request.ContentType = "application/xml";

            var xml = "<delete-ad xmlns=\"http://www.ebayclassifiedsgroup.com/schema/ad/v1\"><reason>NO_REASON</reason></delete-ad>";
            request.ContentLength = xml.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(Encoding.ASCII.GetBytes(xml), 0, xml.Length );
                requestStream.Close();
            }

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    MessageBox.Show("Something went wrong while deleting ad\n" + data);
                }
                else
                {
                    MessageBox.Show("Ad Deleted Successfully");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
