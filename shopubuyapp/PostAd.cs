using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Data;

namespace shopubuyapp
{
    public class PostAd
    {
        public static bool isRunningFlag = false;
        public static void DeleteAdvertisement(Configuration configuration, string data)
        {

            HttpWebRequest request = WebRequest.Create(data) as HttpWebRequest;

            // Set up the request properties.
            request.Method = "DELETE";
            request.Host = "ecg-api.gumtree.com.au";
            request.Headers["Authorization"] = configuration.Authorization;
            request.Accept = "*/*";
            request.Headers["X-ECG-VER"] = "1.49";
            request.Headers["X-ECG-AB-TEST-GROUP"] = "GROUP_50;gblandroid_6959_d";
            request.Headers["Accept-Encoding"] = "gzip, deflate";
            request.Headers["X-ECG-UDID"] = configuration.X_ECG_UDID;
            request.Headers["X-ECG-Authorization-User"] = "id=" + configuration.AccountId + ", token=" + configuration.Token;
            request.Headers["Accept-Language"] = "en-AU";

            request.UserAgent = "Gumtree 12.0.0 (iPhone; iOS 12.2; en_AU)";
            request.KeepAlive = false;
            request.Headers["X-ECG-Original-MachineId"] = configuration.MachineId;
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
        public static void StartPosting(DataTable dt, Configuration config, string fileLocation, CancellationToken token, int delay)
        {
            var picConcat = "";
            int j = 1;
            config.Delay = 1;
            isRunningFlag = true;
            var listOfFiles = Directory.GetFiles(fileLocation, "*.csv").OrderBy(f => f).ToList();
            listOfFiles.RemoveAll(name => name.Contains("completed"));
            foreach (var filepath in listOfFiles)
            {
                try
                {
                    var reader = new StreamReader(filepath);
                    //List<Dictionary<string, string>> listOfProducts = ReadCSV.GetProductList(filepath);

                    var line = reader.ReadLine();
                    while(!reader.EndOfStream)
                    //foreach (var product in listOfProducts)
                    {
                        try
                        {
                            DataRow dataRow = dt.NewRow();
                            //_ravi["Selected"] = true;
                            //_ravi["CategoryId"] = "500" + (j++);
                            //dt.Rows.Add(_ravi);

                            line = reader.ReadLine();
                            char[] charArr = line.ToCharArray();
                            //string s = "data{value here} data";
                            //int start = line.IndexOf("\"");
                            //int end = line.IndexOf("\"", start);
                            //string result = s.Substring(start + 1, end - start - 1);
                            //line = line.Replace(result, "your replacement value");


                            var start = line.IndexOf("\"");
                            var end = line.IndexOf("\"", start + 1);
                            for (int i = start; i < end; i++)
                            {
                                if (charArr[i] == ',')
                                {
                                    charArr[i] = '@';
                                }
                            }
                            line = new string(charArr);
                            var tokens = line.Split(',');
                            //line.Replace()
                            var prod = new Product();
                           
                            //_ravi["CategoryId"] = "500";
                            // dataTable.Rows.Add(_ravi);
                            prod.FileName = filepath;
                            if (!string.IsNullOrWhiteSpace(tokens[0]))
                                prod.CategoryId = Convert.ToInt32(tokens[0]);
                            prod.CategoryName = tokens[2];
                            if (string.IsNullOrWhiteSpace(tokens[3]) || tokens[3].Length < 20)
                                throw new Exception("Description length should be min 20 characters");

                            prod.Description = tokens[3];
                            if (!string.IsNullOrWhiteSpace(tokens[4]))
                                prod.Price = Convert.ToDecimal(tokens[4]);
                            if (tokens[5].Length > 1)
                                prod.Title = tokens[5];
                            else
                                throw new Exception("Title length should be min 8 characters");

                            prod.Location = tokens[6];
                            prod.Images = new List<string>();
                            for (int i = 7; i < tokens.Length; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(tokens[i]) && (tokens[i].Contains(".jpg") || tokens[i].Contains(".png") || tokens[i].Contains(".jpeg")))
                                    prod.Images.Add(tokens[i]);
                            }
                            if (isRunningFlag == true && token.IsCancellationRequested == false)
                            {
                                var categoryId = prod.CategoryId;
                                var categoryName = prod.CategoryName;
                                var description = prod.Description;
                                var contactEmail = prod.ContactEmail;
                                var contactName = prod.ContactName;
                                var amount = prod.Price;
                                var title = prod.Title.Replace('&',' ');
                                var location = prod.Location;
                                var img = prod.Images;

                                dataRow["Selected"] = false;
                                dataRow["CategoryId"] = prod.CategoryId;
                                dataRow["Title"] = prod.Title;
                                dataRow["CategoryName"] = prod.CategoryName;
                                dataRow["Price"] = prod.Price;
                                dataRow["Description"] = prod.Description;
                                dataRow["ContactEmail"] = prod.ContactEmail;
                                dataRow["ContactName"] = prod.ContactName;
                                dataRow["Qty"] = "";
                                dataRow["AdId"] = "";
                                dataRow["Account"] = "";
                                dataRow["Location"] = prod.Location;
                                dataRow["Posted"] = "";
                                dataRow["Images"] = "";
                                dataRow["FileName"] = "";
                                dt.Rows.Add(dataRow);

                                foreach (var pic in img)
                                {
                                    if (pic == string.Empty)
                                        continue;

                                    var picXml = PicUpload.UploadPicture(pic.Replace(",", string.Empty).Replace("\"", string.Empty).Replace("\n", string.Empty),config);
                                    if (!string.IsNullOrWhiteSpace(picXml) && picXml.Length > 400)
                                        picConcat += picXml;
                                }

                                var data = "<ad:ad id=\"\" xmlns:ad=\"http://www.ebayclassifiedsgroup.com/schema/ad/v1\" xmlns:cat=\"http://www.ebayclassifiedsgroup.com/schema/category/v1\" xmlns:loc=\"http://www.ebayclassifiedsgroup.com/schema/location/v1\" xmlns:attr=\"http://www.ebayclassifiedsgroup.com/schema/attribute/v1\" xmlns:types=\"http://www.ebayclassifiedsgroup.com/schema/types/v1\" xmlns:pic=\"http://www.ebayclassifiedsgroup.com/schema/picture/v1\" xmlns:vid=\"http://www.ebayclassifiedsgroup.com/schema/video/v1\" xmlns:user=\"http://www.ebayclassifiedsgroup.com/schema/user/v1\" xmlns:feature=\"http://www.ebayclassifiedsgroup.com/schema/feature/v1\">"
                                    + "<ad:account-id>" + config.AccountId + "</ad:account-id>"
                                    + "<ad:adSlots class=\"java.util.ArrayList\"/>"
                                    + "<ad:ad-address><types:full-address>Sydney NSW, Australia</types:full-address><types:radius>1000</types:radius></ad:ad-address>"
                                    + "<attr:attributes class=\"java.util.ArrayList\">"
                                    + "<attr:attribute localized-label=\"Condition\" name=\"" + categoryName + ".condition\" type=\"ENUM\">"
                                    + "<attr:value>new</attr:value></attr:attribute></attr:attributes>"
                                    + "<cat:category id=\"" + categoryId + "\"/>"
                                    + "<ad:description><![CDATA[" + description + "]]></ad:description>"
                                    + "<ad:email>" + contactEmail + "</ad:email>"
                                    + "<loc:locations class=\"java.util.ArrayList\"><loc:location id=\"" + location + "\"/></loc:locations >"
                                    + "<ad:phone></ad:phone>"
                                    + "<pic:pictures class=\"java.util.ArrayList\">" + picConcat + "</pic:pictures>"
                                    + "<ad:poster-contact-email>" + contactEmail + "</ad:poster-contact-email>"
                                    + "<ad:poster-contact-name>" + contactName + "</ad:poster-contact-name>"
                                    + "<ad:price><types:amount>" + amount + "</types:amount>"
                                    + "<types:currency-iso-code><types:value>AUD</types:value></types:currency-iso-code>"
                                    + "<types:price-type><types:value>SPECIFIED_AMOUNT</types:value></types:price-type></ad:price>"
                                    + "<ad:title>" + title + "</ad:title>"
                                    + "<ad:ad-type><ad:value>OFFERED</ad:value></ad:ad-type></ad:ad>";
                                
                                picConcat = "";

                                try
                                {
                                    PostAd.PostAdvertisement(data, config);
                                }
                                catch (Exception except)
                                {
                                    prod.Successful = false;
                                    prod.Posted = false;
                                    prod.Active = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        
                        var cancelled = token.WaitHandle.WaitOne(TimeSpan.FromSeconds(config.Delay));

                        if (cancelled)
                            break;
                    }
                    System.IO.File.Move(filepath, filepath+ "_completed.csv");
                }
                catch (Exception exce)
                {
                    //MessageBox.Show(exce.Message);
                    //return;
                }


            }
        }
        string requestData = "";
        public static HttpStatusCode PostAdvertisement(string data, Configuration config)
        {
            data = File.ReadAllText(@"D:\Projects\PostAds\Source\python\shopubuy\PostAds\python_req_xml.txt");
            var postUrl = "https://ecg-api.gumtree.com.au/api/users/" + config.Email + "/ads";
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            // Set up the request properties.
            request.Method = "POST";
            request.UserAgent = "Gumtree 12.0.0 (iPhone; iOS 12.2; en_AU)";
            request.Headers["Accept-Language"] = "en-AU";
            request.Headers["X-ECG-VER"] = "1.51";
            request.Headers["X-ECG-UDID"] = config.X_ECG_UDID;
            request.Accept = "application/xml";
            request.KeepAlive = false;
            request.ContentType = "application/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(data);
            request.ContentLength = bytes.Length;
            request.Headers["Pragma"] = "no-cache";
            request.Headers["X-ECG-AB-TEST-GROUP"] = "GROUP_50;gblandroid_6959_d";
            request.Headers["Authorization"] = config.Authorization;
            request.Headers["X-ECG-Authorization-User"] = "id=" + config.AccountId + ", token=" + config.Token;
            request.Headers["X-ECG-Original-MachineId"] = config.MachineId;
            request.Headers["X-THREATMETRIX-SESSION-ID"] = config.SessionId;
            request.ContentType = "application/xml; charset=UTF-8";
            request.Host = "ecg-api.gumtree.com.au";
            request.Headers["Accept-Encoding"] = "gzip, deflate";

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
                requestStream.Close();
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var statusCode = response.StatusCode;
            if (response.StatusCode == HttpStatusCode.Created)
            {
                //MessageBox.Show("Ad Posted Successfully");
                /*
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(response.ContentEncoding);
                XmlNode root = xdoc.FirstChild;
                */
            }
            response.Close();
            return statusCode;
        }
    }
}
