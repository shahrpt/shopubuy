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
using HtmlAgilityPack;
using System.Xml;
using System.IO.Compression;

namespace shopubuyapp
{
    public class PostAd
    {
        public static bool isRunningFlag = false;
        public static HttpWebResponse DeleteAdvertisement(Configuration configuration, string data)
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
                return request.GetResponse() as HttpWebResponse;
                
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        public static int RemoveNodesButKeepChildren(HtmlNode rootNode, string xPath)
        {
            HtmlNodeCollection nodes = rootNode.SelectNodes(xPath);
            if (nodes == null)
                return 0;
            foreach (HtmlNode node in nodes)
                RemoveButKeepChildren(node);
            return nodes.Count;
        }

        public static void RemoveButKeepChildren(HtmlNode node)
        {
            foreach (HtmlNode child in node.ChildNodes)
                node.ParentNode.InsertBefore(child, node);
            node.Remove();
        }

        public static bool TestYourSpecificExample()
        {
            string html = "<p>my paragraph <div>and my <b>div</b></div> are <i>italic</i> and <b>bold</b></p>";
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            RemoveNodesButKeepChildren(document.DocumentNode,"//div");
            RemoveNodesButKeepChildren(document.DocumentNode, "//strong");
            RemoveNodesButKeepChildren(document.DocumentNode, "//br");
            RemoveNodesButKeepChildren(document.DocumentNode,"//p");
            return document.DocumentNode.InnerHtml == "my paragraph and my <b>div</b> are <i>italic</i> and <b>bold</b>";
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
                            prod.Sku = tokens[0];
                            prod.CategoryId = tokens[1];
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
                                var contactEmail = "iansydney77@gmail.com";
                                if (!string.IsNullOrEmpty(prod.ContactEmail))
                                    contactEmail = prod.ContactEmail;
                                var contactName = "Shopubuy";
                                if (!string.IsNullOrEmpty(prod.ContactName))
                                {
                                    contactName = prod.ContactName;
                                }
                                var amount = prod.Price;
                                var title = prod.Title.Replace('&',' ');
                                var location = prod.Location;
                                var img = prod.Images;

                                //dataRow["Selected"] = false;
                                dataRow["CategoryId"] = prod.CategoryId;
                                dataRow["Title"] = prod.Title;
                                dataRow["CategoryName"] = prod.CategoryName;
                                dataRow["Price"] = prod.Price;
                                
                                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                                document.LoadHtml(prod.Description);
                                RemoveNodesButKeepChildren(document.DocumentNode, "//div");
                                RemoveNodesButKeepChildren(document.DocumentNode, "//strong");
                                RemoveNodesButKeepChildren(document.DocumentNode, "//br");
                                RemoveNodesButKeepChildren(document.DocumentNode, "//p");
                                var cleanDesc = document.DocumentNode.InnerHtml;// == "my paragraph and my <b>div</b> are <i>italic</i> and <b>bold</b>";
                                cleanDesc = cleanDesc.Replace('@', ',');
                                description = cleanDesc;
                                dataRow["Description"] = cleanDesc;
                                dataRow["ContactEmail"] = prod.ContactEmail;
                                dataRow["ContactName"] = prod.ContactName;
                                dataRow["Qty"] = "";
                                dataRow["AdId"] = "";
                                dataRow["Account"] = "";
                                dataRow["Location"] = prod.Location;
                                dataRow["Posted"] = "";
                                dataRow["Images"] = "";
                                

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
                                    var dataFormat = File.ReadAllText(@"python_req_format.txt");
                                    dataFormat = dataFormat.Replace("{0}", config.AccountId).Replace("{1}", location).Replace("{2}", contactEmail).Replace("{3}", contactName).Replace("{4}", title);
                                    dataFormat = dataFormat.Replace("{5}", amount.ToString()).Replace("{6}", categoryName).Replace("{7}", description).Replace("{8}", contactEmail);
                                    dataFormat = dataFormat.Replace("{9}", picConcat).Replace("{10}", categoryId); ;

                                    //data = string.Format(dataFormat, config.AccountId, categoryName, categoryId, description, contactEmail, location, picConcat, contactEmail, contactName, amount, title);
                                    data = dataFormat;  
                                    var response = PostAd.PostAdvertisement(data, config);
                                    var statusCode = response.StatusCode;
                                    if (response.StatusCode == HttpStatusCode.Created)
                                    {
                                        // Get the stream associated with the response.
                                        Stream receiveStream = response.GetResponseStream();

                                        using (var str = response.GetResponseStream())
                                        using (var gsr = new GZipStream(str, CompressionMode.Decompress))
                                        using (var sr = new StreamReader(gsr))

                                        {
                                            string s = sr.ReadToEnd();
                                            XmlDocument xmlDoc = new XmlDocument();
                                            xmlDoc.LoadXml(s);
                                            XmlNodeList elemList = xmlDoc.GetElementsByTagName("ad:link");
                                            for (int i = 0; i < elemList.Count; i++)
                                            {
                                                string keyname = elemList[i].Attributes["rel"].Value;
                                                string keyvalue = elemList[i].Attributes["href"].Value;
                                                var AdId = keyvalue.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
                                                dataRow["AdId"] = AdId;
                                                
                                            }
                                        }
                                    }
                                    response.Close();
                                    dataRow["StatusCode"] = statusCode;

                                    dt.Rows.Add(dataRow);
                                }
                                catch (Exception except)
                                {
                                    dataRow["StatusCode"] = except.Message;
                                    dt.Rows.Add(dataRow);

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
        public static HttpWebResponse PostAdvertisement(string data, Configuration config)
        {
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
            return response;
           
        }
    }
}
