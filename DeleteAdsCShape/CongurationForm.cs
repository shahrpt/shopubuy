using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace DeleteAdsCShape
{
    public partial class CongurationForm : Form
    {
        public CongurationForm()
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
                Password.Text = node.Attribute("Password").Value;
            }            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDlg.ShowDialog();
            if (res == DialogResult.OK)
                csvLocation.Text = folderBrowserDlg.SelectedPath;
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (false == CheckConfiguration())
                return;

            //Save Configuration
            XDocument xdoc = new XDocument(
                new XElement("root",
                new XAttribute("X_ECG_UDID", X_ECG_UDID.Text),
                new XAttribute("Authorization", Authorization.Text),
                new XAttribute("AccountId", AccountId.Text),
                new XAttribute("Token", Token.Text),
                new XAttribute("MachineId", MachineId.Text),
                new XAttribute("Email", Email.Text),
                new XAttribute("SessionId", SessionId.Text),
                new XAttribute("Password", Password.Text)
             ));
            
            xdoc.Save("./config.xml");

            //Set environment
            Configuration.X_ECG_UDID = X_ECG_UDID.Text;
            Configuration.Authorization = Authorization.Text;
            Configuration.AccountId = AccountId.Text;
            Configuration.Token = Token.Text;
            Configuration.MachineId = MachineId.Text;
            Configuration.Email = Email.Text;
            Configuration.SessionId = SessionId.Text;
            Configuration.Password = Password.Text;

            var listOfFiles = Directory.GetFiles(csvLocation.Text, "*.txt");
            foreach (var filepath in listOfFiles)
            {

                List<Dictionary<string, string>> listOfProducts = ReadCSV.GetProductList(filepath);

                foreach (var product in listOfProducts)
                {
                    var adId = product["adId"];
                    var data = "https://ecg-api.gumtree.com.au/api/users/" + Configuration.AccountId + "/ads/" + adId;

                    PostAd.PostAdvertisement(data);

                    try
                    {
                        PostAd.PostAdvertisement(data);
                    }
                    catch (Exception except)
                    {
                        MessageBox.Show(except.Message);
                    }

                }
            }
        }

        private bool CheckConfiguration()
        {
            if (X_ECG_UDID.Text == string.Empty)
            {
                MessageBox.Show("Please insert X_ECG_UDID!");
                return false;
            }
                
            if( Authorization.Text == string.Empty)
            {
                MessageBox.Show("Please insert Authorization!");
                return false;
            }

            if (AccountId.Text == string.Empty)
            {
                MessageBox.Show("Please insert AccountID!");
                return false;
            }

            if (Token.Text ==string.Empty)
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

            if (Password.Text == string.Empty)
            {
                MessageBox.Show("Please insert Password!");
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
