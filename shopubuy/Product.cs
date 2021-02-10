using System;
using System.Collections.Generic;
using System.Text;

namespace shopubuy
{
    class Product
    {
        public string Title { get; set; }
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string Group { get; set; }
        public string Price { get; set; }

        public string QOH { get; set; }
        public string AdId { get; set; }
        public string LiveId { get; set; }
        public string GeneralCheck { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public bool IsMVP { get; set; }
    }
}
