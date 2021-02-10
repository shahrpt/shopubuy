using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PostAdsCShape
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
            if (File.Exists("./config.xml"))
            {
                XElement element = XElement.Load("./config.xml");                
                var node = XElement.Parse(element.ToString());
                        
                X_ECG_UDID.Text = node.Attribute("X_ECG_UDID").Value;
                Authorization.Text = node.Attribute("Authorization").Value;
                AccountId.Text = node.Attribute("AccountId").Value;
                Token.Text = node.Attribute("Token").Value;
                MachineId.Text = node.Attribute("MachineId").Value;
                Email.Text = node.Attribute("Email").Value;
                SessionId.Text = node.Attribute("SessionId").Value;
                
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (false == CheckConfiguration())
            {
                return;
            }

            //Save Configuration
            XDocument xdoc = new XDocument(
                new XElement("Setting",
                new XAttribute("X_ECG_UDID", X_ECG_UDID.Text),
                new XAttribute("Authorization", Authorization.Text),
                new XAttribute("AccountId", AccountId.Text),
                new XAttribute("Token", Token.Text),
                new XAttribute("MachineId", MachineId.Text),
                new XAttribute("Email", Email.Text),
                new XAttribute("SessionId", SessionId.Text)
             ));
            
            xdoc.Save("./config.xml");

            Configuration.X_ECG_UDID = X_ECG_UDID.Text;
            Configuration.Authorization = Authorization.Text;
            Configuration.AccountId = AccountId.Text;
            Configuration.Token = Token.Text;
            Configuration.MachineId = MachineId.Text;
            Configuration.Email = Email.Text;
            Configuration.SessionId = SessionId.Text;

            var picConcat = "";
            var listOfFiles = Directory.GetFiles(csvLocation.Text, "*.csv");
            foreach (var filepath in listOfFiles)
            {
                try
                {
                    List<Dictionary<string, string>> listOfProducts = ReadCSV.GetProductList(filepath);

                    foreach (var product in listOfProducts)
                    {
                        var categoryId = product["categoryId"];
                        var categoryName = product["categoryName"];
                        var description = product["description"];
                        var contactEmail = product["contactEmail"];
                        var contactName = product["contactName"];
                        var amount = product["amount"];
                        var title = product["title"];
                        var location = product["location"];
                        var img = product["listOfPics"];

                        foreach (var pic in img.Split(','))
                        {
                            if (pic == string.Empty)
                                continue;

                            var picXml = PicUpload.UploadPicture(pic.Replace(",", string.Empty).Replace("\"", string.Empty).Replace("\n", string.Empty));
                            if (picXml.Length > 400)
                                picConcat += picXml;
                        }

                        var data = "<ad:ad id=\"\" xmlns:ad=\"http://www.ebayclassifiedsgroup.com/schema/ad/v1\" xmlns:cat=\"http://www.ebayclassifiedsgroup.com/schema/category/v1\" xmlns:loc=\"http://www.ebayclassifiedsgroup.com/schema/location/v1\" xmlns:attr=\"http://www.ebayclassifiedsgroup.com/schema/attribute/v1\" xmlns:types=\"http://www.ebayclassifiedsgroup.com/schema/types/v1\" xmlns:pic=\"http://www.ebayclassifiedsgroup.com/schema/picture/v1\" xmlns:vid=\"http://www.ebayclassifiedsgroup.com/schema/video/v1\" xmlns:user=\"http://www.ebayclassifiedsgroup.com/schema/user/v1\" xmlns:feature=\"http://www.ebayclassifiedsgroup.com/schema/feature/v1\">"
                            + "<ad:account-id>" + AccountId + "</ad:account-id>"
                            + "<ad:adSlots class=\"java.util.ArrayList\"/>"
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
                            PostAd.PostAdvertisement(data);
                        }
                        catch(Exception except)
                        { }

                    }
                }
                catch(Exception exce)
                {
                    MessageBox.Show(exce.Message);
                    return;
                }
                
                
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDlg.ShowDialog();
            if (res == DialogResult.OK)
                csvLocation.Text = folderBrowserDlg.SelectedPath;
        }

        private bool CheckConfiguration()
        {
            if (X_ECG_UDID.Text == string.Empty)
            {
                MessageBox.Show("Please insert X_ECG_UDID!");
                return false;
            }

            if (Authorization.Text == string.Empty)
            {
                MessageBox.Show("Please insert Authorization!");
                return false;
            }

            if (AccountId.Text == string.Empty)
            {
                MessageBox.Show("Please insert AccountID!");
                return false;
            }

            if (Token.Text == string.Empty)
            {
                MessageBox.Show("Please insert Token!");
                return false;
            }

            if (MachineId.Text == string.Empty)
            {
                MessageBox.Show("Please insert MachineId!");
                return false;
            }

            if (Email.Text == string.Empty)
            {
                MessageBox.Show("Please insert Email!");
                return false;
            }

            if (SessionId.Text == string.Empty)
            {
                MessageBox.Show("Please insert SessionId!");
                return false;
            }
       
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
