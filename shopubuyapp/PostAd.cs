using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace shopubuyapp
{
    public class PostAd
    {
        public static void DeleteAdvertisement(string data)
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
                requestStream.Write(Encoding.ASCII.GetBytes(xml), 0, xml.Length);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void PostAdvertisement(string data)
        {
            var postUrl = "https://ecg-api.gumtree.com.au/api/users/" + Configuration.Email + "/ads";
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            // Set up the request properties.
            request.Method = "POST";
            request.UserAgent = "Gumtree 12.0.0 (iPhone; iOS 12.2; en_AU)";
            request.Headers["Accept-Language"] = "en-AU";
            request.Headers["X-ECG-VER"] = "1.51";
            request.Headers["X-ECG-UDID"] = Configuration.X_ECG_UDID;
            request.Accept = "application/xml";
            request.KeepAlive = false;
            request.Headers["Pragma"] = "no-cache";
            request.Headers["X-ECG-AB-TEST-GROUP"] = "GROUP_50;gblandroid_6959_d";
            request.Headers["Authorization"] = Configuration.Authorization;
            request.Headers["X-ECG-Authorization-User"] = "id=" + Configuration.AccountId + ", token=" + Configuration.Token;
            request.Headers["X-ECG-Original-MachineId"] = Configuration.MachineId;
            request.Headers["X-THREATMETRIX-SESSION-ID"] = Configuration.SessionId;
            request.ContentType = "application/xml; charset=UTF-8";
            request.Host = "ecg-api.gumtree.com.au";
            request.Headers["Accept-Encoding"] = "gzip, deflate";

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
                requestStream.Close();
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.Created)
            {
                MessageBox.Show("Ad Posted Successfully");
                /*
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(response.ContentEncoding);
                XmlNode root = xdoc.FirstChild;
                */
            }
            response.Close();
        }
    }
}
