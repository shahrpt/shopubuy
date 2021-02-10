using System;
using System.Collections.Generic;
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

        private List<Product> LoadCollectionData()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product()
            {
                LiveId = "101",
                SKU = "Mahesh Chand",
                CategoryName = "Graphics Programming with GDI+",
                TimeStamp = new DateTime(1975, 2, 23),
                IsMVP = true
            });

            products.Add(new Product()
            {
                LiveId = "102",
                SKU = "Mahesh Chand",
                CategoryName = "Graphics Programming with GDI+",
                TimeStamp = new DateTime(1975, 2, 23),
                IsMVP = false
            });

            products.Add(new Product()
            {
                LiveId = "103",
                SKU = "Mahesh Chand",
                CategoryName = "Graphics Programming with GDI+",
                TimeStamp = new DateTime(1975, 2, 23),
                IsMVP = false
            });

            return products;
        }
        public MainWindow()
        {
            InitializeComponent();
            McDataGrid.ItemsSource = LoadCollectionData();
        }
    }
}
