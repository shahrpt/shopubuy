using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopubuyapp
{
    class Product
    {
        /*categoryId	categoryName	description	contact_email	contact_name	amount	title	location	listOfPics*/

        public int CategoryId { get; set; }
        public string Title { get; set; }
        //public string SKU { get; set; }
        public string CategoryName { get; set; }
        public int Group { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }

        //public int QOH { get; set; }
        //public string AdId { get; set; }
        public string Account { get; set; }
        public string LiveId { get; set; }
        public string GeneralCheck { get; set; }
        public DateTime TimeStamp { get; set; }
        [Browsable(false)]
        public string Location { get; set; }
        [System.ComponentModel.Browsable(false)]
        public bool Active { get; set; }
        [System.ComponentModel.Browsable(false)]
        public List<string> Images { get; set; }
    }
}
