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
        private void timer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
            //refresh here...
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 7000;//5 seconds
            timer1.Tick += new System.EventHandler(timer_Tick);
            //dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            timer1.Start();

            X_ECG_UDID.Text = "81771F47-1154-42FD-AAEF-38C6E1FE5113";
            Authorization.Text = "Basic YXVfaXBob25lX2FwcDplY2dhcGkhZ2xvYmFs";
            AccountId.Text = "1932228975447";
            Token.Text = "5e8717859b3f7f02149439872fce1614c8804c0fa1758c552b181b10bf451898";
            MachineId.Text = "SpJwk3wiPE16ziFZqQK0mw760ACZxYEhY47xl-BXvw7nipT9VCPUIvPYs7nzbeHiXEgMrhhmXoDHmr6ufUEMc4cx1eINyZsiozCA";
            Email.Text = "iansydney77@gmail.com";
            SessionId.Text = "82ec95928304463f8d27b38501145ed7";
            txtName.Text = "SydAds";
            csvLocation.Text = @"D:\Projects\PostAds";
           
            this.WindowState = FormWindowState.Maximized;
            tabControl1.Dock = DockStyle.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //flowLayoutPanel2.Controls.Add(dataGridView1);

            //var list = LoadCollectionData(@"D:\Projects\PostAds\products.csv");
            //var dataTable = ToDataTable(list);
            //dataTable.AcceptChanges();
            //dataGridView1.DataSource = dataTable;

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
                               Sku = dr["Sku"].ToString(),
                               CategoryId = dr["CategoryId"].ToString(),
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
                        prod.CategoryId = tokens[1];
                    prod.CategoryName = tokens[2];
                    prod.Description = tokens[4];
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
            try
            {
                if (e.ColumnIndex == 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    var adId = row.Cells["AdId"].Value.ToString();
                    if (string.IsNullOrWhiteSpace(adId))
                    {
                        MessageBox.Show("AdId is missing");
                        return;
                    }
                    var data = "https://ecg-api.gumtree.com.au/api/users/" + configuration.AccountId + "/ads/" + adId;
                    var response = PostAd.DeleteAdvertisement(configuration, data);
                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["StatusCode"].Value = "Deleted";
                    }
                        
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show("Clicked");
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public static void StartBatch()
        {

        }
        public static void StartBatch(DataTable dt, Configuration config, string csvLocation, CancellationToken token, int delay = 2)
        {
           
            PostAd.StartPosting(dt, config, csvLocation, token,delay);
        }
        Configuration configuration = new Configuration();
        CancellationTokenSource cts = null;
        DataTable dataTable = new DataTable();
        System.ComponentModel.BackgroundWorker backgroundWorker1;
        private void button1_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            button1.Enabled = true;
            if (button1.Text == "Start")
            {
                CancellationTokenSource token = new CancellationTokenSource();
                Configuration config = new Configuration();
                dataGridView1.DataSource = dataTable;
                var deleteButton = new DataGridViewButtonColumn();
                deleteButton.Name = "dataGridViewDeleteButton";
                deleteButton.HeaderText = "Delete";
                deleteButton.Text = "Delete";
                deleteButton.UseColumnTextForButtonValue = true;
                this.dataGridView1.Columns.Add(deleteButton);

                dataTable.Clear();
                dataTable.Columns.Clear();
                    //dataTable.Columns.Add("Deleted");
                dataTable.Columns.Add("CategoryId");
                dataTable.Columns.Add("Title");
                dataTable.Columns.Add("CategoryName");
                //dataTable.Columns.Add("Group");
                dataTable.Columns.Add("Price");
                dataTable.Columns.Add("Description");
                dataTable.Columns.Add("ContactEmail");
                dataTable.Columns.Add("ContactName");
                dataTable.Columns.Add("Qty");
                dataTable.Columns.Add("AdId");
                dataTable.Columns.Add("Account");
                //dataTable.Columns.Add("LiveId");
                //dataTable.Columns.Add("GeneralCheck");
                //dataTable.Columns.Add("TimeStamp");
                dataTable.Columns.Add("Location");
                //dataTable.Columns.Add("Active");
                dataTable.Columns.Add("Posted");
                dataTable.Columns.Add("Images");
                //dataTable.Columns.Add("Successful");
                dataTable.Columns.Add("FileName");
                dataTable.Columns.Add("StatusCode");
                dataTable.Columns.Add("Repost");
               
                //dataTable.Columns.Add("Name");
                //dataTable.Columns.Add("Marks");
                DataRow _ravi = dataTable.NewRow();
                //_ravi["Selected"] = false;
                //_ravi["CategoryId"] = "500";
               // dataTable.Rows.Add(_ravi);

                //StartBatch(dataTable, config, csvLocation.Text, token, 5000);
                //lblStatus.Text = "Posting Ads in Progress...";
                toolStripStatusLabel2.Text = "Posting Ads in Progress...";

                //button1.Text = "Stop";


                configuration.X_ECG_UDID = X_ECG_UDID.Text;
                configuration.Authorization = Authorization.Text;
                configuration.AccountId = AccountId.Text;
                configuration.Token = Token.Text;
                configuration.MachineId = MachineId.Text;
                configuration.Email = Email.Text;
                configuration.SessionId = SessionId.Text;
                configuration.Delay = 1;
                Thread t = new Thread(new ThreadStart(() => StartBatch(dataTable, configuration, csvLocation.Text, cts.Token, 2)));
                t.Start();

                ;
            }
            /*else if(button1.Text == "Stop")
            {
                button1.Text = "Start";
                Thread.Sleep(1000);
                lblStatus.Text = "";
                //Stop th eprocess
            }*/
            /*DataTable dt = new DataTable();
            dt = ((DataTable)dataGridView1.DataSource);

            var listN = GetListFromDataTable(dt);
            listN.Add(new Product { CategoryId = 555555 });

            dt = ToDataTable(listN);
            dataGridView1.DataSource = dt;
            Console.WriteLine();*/
        }

        private void PostDeleteHandler(object obj)
        {
            MessageBox.Show("Hello");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cts != null)
                cts.Cancel();
            List<DataGridViewRow> toDelete = new List<DataGridViewRow>();

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("No Row Selected");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                var adId = row.Cells["AdId"].Value.ToString();
                if (string.IsNullOrWhiteSpace(adId))
                    continue;
                var data = "https://ecg-api.gumtree.com.au/api/users/" + configuration.AccountId + "/ads/" + adId;
                var response = PostAd.DeleteAdvertisement(configuration, data);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    
                }
                else
                {
                    row.Cells["StatusCode"].Value = "Deleted";
                } 
                dataGridView1.Rows.Remove(row);
            }
                   
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
            Configuration configuration = new Configuration();
            configuration.X_ECG_UDID = X_ECG_UDID.Text;
            configuration.Authorization = Authorization.Text;
            configuration.AccountId = AccountId.Text;
            configuration.Token = Token.Text;
            configuration.MachineId = MachineId.Text;
            configuration.Email = Email.Text;
            configuration.SessionId = SessionId.Text;

          
            PostAd.StartPosting(dataTable, configuration, csvLocation.Text, cts.Token,configuration.Delay);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Refresh();
            //lblStatus.Text = "Posting Stopped";
            toolStripStatusLabel2.Text = "Posting Stopped";
            cts.Cancel();
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cts != null)
                cts.Cancel();
            DataTable dt = new DataTable();
            dt = (DataTable)dataGridView1.DataSource;
            if (dt != null)
                ToCSV(dt, "posted.csv");
        }
        public void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        
    }
}
