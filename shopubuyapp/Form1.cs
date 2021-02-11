using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace shopubuyapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            tabControl1.Dock = DockStyle.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //flowLayoutPanel2.Controls.Add(dataGridView1);

            var list = LoadCollectionData(@"D:\Projects\PostAds\products.csv");
            var dataTable = ToDataTable(list);
            dataTable.AcceptChanges();
            dataGridView1.DataSource = dataTable;

            //dataGridView1.Rows.Add(new Product() { CategoryId = 44545454 });
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        private List<Product> GetListFromDataTable(DataTable dt)
        {
            List<Product> productList = new List<Product>();
            productList = (from DataRow dr in dt.Rows
                           select new Product()
                           {
                               CategoryId = Convert.ToInt32(dr["CategoryId"]),
                               CategoryName = dr["CategoryName"].ToString(),
                               Description = dr["Description"].ToString(),
                               ContactEmail = dr["ContactEmail"].ToString(),
                               ContactName = dr["ContactName"].ToString(),

                               Price = Convert.ToDecimal(dr["Price"]),
                               Title = dr["Title"].ToString(),
                               Location = dr["Location"].ToString(),

                           }).ToList();
            return productList;
        }
        private List<Product> LoadCollectionData(string fileName)
        {
            List<Product> products = new List<Product>();
            using (var reader = new StreamReader(fileName))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var tokens = line.Split(',');
                    var prod = new Product();
                    if (!string.IsNullOrWhiteSpace(tokens[0]))
                        prod.CategoryId = Convert.ToInt32(tokens[0]);
                    prod.CategoryName = tokens[1];
                    prod.Description = tokens[2];
                    prod.ContactEmail = tokens[3];
                    prod.ContactName = tokens[4];
                    if (!string.IsNullOrWhiteSpace(tokens[5]))
                        prod.Price = Convert.ToDecimal(tokens[5]);
                    prod.Title = tokens[6];
                    prod.Location = tokens[7];
                    prod.Images = new List<string>();
                    for (int i = 8; i < tokens.Length; i++)
                    {
                        prod.Images.Add(tokens[i]);
                    }
                    products.Add(prod);
                }
            }
            return products;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        static void StartPost(CancellationToken token, int delay = 5000)
        {
            while (token.IsCancellationRequested == false)
            {
                // Initial processing

                if (true)
                {
                    // Sleep for 5 seconds, but exit if token is cancelled
                    var cancelled = token.WaitHandle.WaitOne(TimeSpan.FromHours(delay));

                    if (cancelled)
                        break;
                }

                // Continue processing
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                lblStatus.Text = "Processing Posts...";
                button1.Text = "Stop";
                //Start the process
            }
            else if(button1.Text == "Stop")
            {
                button1.Text = "Start";
                Thread.Sleep(1000);
                lblStatus.Text = "";
                //Stop th eprocess
            }
            /*DataTable dt = new DataTable();
            dt = ((DataTable)dataGridView1.DataSource);

            var listN = GetListFromDataTable(dt);
            listN.Add(new Product { CategoryId = 555555 });

            dt = ToDataTable(listN);
            dataGridView1.DataSource = dt;
            Console.WriteLine();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            //StartWebRequest(cts.Token);

            // cancellation will cause the web
            // request to be cancelled
            cts.Cancel();
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
                        catch (Exception except)
                        { }

                    }
                }
                catch (Exception exce)
                {
                    MessageBox.Show(exce.Message);
                    return;
                }


            }
        }
    }
}
