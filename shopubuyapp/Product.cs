using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopubuyapp
{
    class Product
    {
        /*categoryId	categoryName	description	contact_email	contact_name	amount	title	location	listOfPics*/
        public bool Selected { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 9)]
        public string Title { get; set; }
        //public string SKU { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CategoryName { get; set; }
        public int Group { get; set; }
        public decimal Price { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 11)]
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        [Browsable(false)]
        public int Qty { get; set; }
        //public string AdId { get; set; }
        public string Account { get; set; }
        [Browsable(false)]
        public string LiveId { get; set; }
        public string GeneralCheck { get; set; }
        public DateTime TimeStamp { get; set; }
        [Browsable(false)]
        public string Location { get; set; }
        [System.ComponentModel.Browsable(false)]
        public bool Active { get; set; }
        public bool Posted { get; set; }

        public bool ImagesUploaded { get; set; }

        public bool Successful { get; set; }
        [System.ComponentModel.Browsable(false)]
        public List<string> Images { get; set; }
        public string FileName { get; set; }

        
    }
}
