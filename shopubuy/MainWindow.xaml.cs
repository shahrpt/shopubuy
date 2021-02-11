using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shopubuy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>  
        /// List of Authors  
        /// </summary>  
        /// <returns></returns>  

        
        private List<Product> LoadCollectionData(string fileName)
        {
            List<Product> products = new List<Product>();
            using (var reader = new StreamReader(fileName))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var tokens = line.Split(",");
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
            /*List<Product> products = new List<Product>();
            products.Add(new Product()
            {
                CategoryId = 21007,
                SKU = "Mahesh Chand",
                CategoryName = "Graphics Programming with GDI+",
                TimeStamp = new DateTime(1975, 2, 23),
                Active = true
            });

            products.Add(new Product()
            {
                LiveId = "102",
                SKU = "Mahesh Chand",
                CategoryName = "Graphics Programming with GDI+",
                TimeStamp = new DateTime(1975, 2, 23),
                Active = false
            });

            products.Add(new Product()
            {
                LiveId = "103",
                SKU = "Mahesh Chand",
                CategoryName = "Graphics Programming with GDI+",
                TimeStamp = new DateTime(1975, 2, 23),
                Active = false
            });*/

            //return products;
        }
        public MainWindow()
        {
            InitializeComponent();
            ;
            McDataGrid.ItemsSource = LoadCollectionData(@"D:\Projects\PostAds\products.csv");
            
        }
    }
}
