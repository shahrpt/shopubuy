using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;

namespace PostAdsCShape
{
    public static class FormUpload
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData);
        }
        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            // Set up the request properties.
            request.Method = "POST";            
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.Headers["Accept-Language"] = "en-AU";
            request.Headers["X-ECG-VER"] = "1.51";
            request.Headers["X-ECG-UDID"] = Configuration.X_ECG_UDID;
            request.Accept = "application/xml";
            request.Headers["Pragma"] = "no-cache";
            request.KeepAlive = false;
            request.Headers["X-ECG-AB-TEST-GROUP"] = "GROUP_50;gblandroid_6959_d";
            request.Headers.Add("Authorization", Configuration.Authorization);
            request.Headers["X-ECG-Authorization-User"] = "id=" + Configuration.AccountId + ", token=" + Configuration.Token;
            request.Headers["X-ECG-Original-MachineId"] = Configuration.MachineId;
            request.Host = "ecg-api.gumtree.com.au";
            request.Headers["Accept-Encoding"] = "gzip, deflate";
            request.ContentLength = formData.Length;

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                            boundary,
                            param.Key,
                            fileToUpload.FileName ?? param.Key,
                            fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
    public class PicUpload
    {
        public static string UploadPicture(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename", "adUploadImage.jpg");
            postParameters.Add("fileformat", "image/jpg");
            postParameters.Add("file", new FormUpload.FileParameter(data, "adUploadImage.jpg", "multipart/form-data"));

            string postURL = "https://ecg-api.gumtree.com.au/api/pictures";
            string userAgent = "com.ebay.gumtree.au 6.2.0 (Genymotion Google Nexus 5X - 6.0; Android 6.0; en_US)";
            try
            {
                HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters);
                if (webResponse.StatusCode == HttpStatusCode.Created)
                {
                    var fullResponse = "";
                    Stream resStream = webResponse.GetResponseStream();

                    var t = ReadFully(resStream);
                    var y = Decompress(t);

                    using (var ms = new MemoryStream(y))
                    using (var streamReader = new StreamReader(ms))
                    {
                        fullResponse = streamReader.ReadToEnd();                        
                    }
                    webResponse.Close();

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(fullResponse);

                    var thumbnail = "";
                    var normal = "";
                    var large = "";
                    var extraLarge = "";
                    var extraExtraLarge = "";

                    foreach (XmlNode Node in xdoc.ChildNodes)
                    {
                        if (Node is XmlElement)
                        {
                            foreach(XmlElement childNode in Node)
                            {
                                if (childNode.Attributes["rel"].Value == "thumbnail")
                                    thumbnail = childNode.Attributes["href"].Value;
                                if (childNode.Attributes["rel"].Value == "normal")
                                    normal = childNode.Attributes["href"].Value;
                                if (childNode.Attributes["rel"].Value == "large")
                                    large = childNode.Attributes["href"].Value;
                                if (childNode.Attributes["rel"].Value == "extraLarge")
                                    extraLarge = childNode.Attributes["href"].Value;
                                if (childNode.Attributes["rel"].Value == "extraExtraLarge")
                                    extraExtraLarge = childNode.Attributes["href"].Value;

                            }                            
                        }                        
                    }

                    var finalXML = "<pic:picture><pic:link href=\"" + extraExtraLarge + "\" rel=\"extraExtraLarge\"/>"
                     + "<pic:link href=\"" + normal + "\" rel=\"normal\"/>"
                     + "<pic:link href=\"" + thumbnail + "\" rel=\"thumbnail\"/>"
                     + "<pic:link href=\"" + extraLarge + "\" rel=\"extraLarge\"/>"
                     + "<pic:link href=\"" + large + "\" rel=\"large\"/></pic:picture>";

                    return finalXML;

                }
                else
                { }

                webResponse.Close();
            }
            catch(Exception e)
            {
                string err = e.Message;
                int a = 1;
            }

            return null;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
