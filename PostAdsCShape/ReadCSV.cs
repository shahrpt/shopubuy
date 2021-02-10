using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostAdsCShape
{
    public class ReadCSV
    {
        public static List<Dictionary<string, string>> GetProductList(string filepath)
        {
            List<Dictionary<string, string>> productList = new List<Dictionary<string, string>>();
            
            
            var csv_reader = File.ReadAllLines(filepath);
            var csvcontent = string.Empty;
            foreach (var row in csv_reader.Skip(1))
            {
                csvcontent += row + "\n";
            }
            
            if (csvcontent != string.Empty)
            {
                Dictionary<string, string> productItem = new Dictionary<string, string>();

                var values = csvcontent.Split(',');

                var categoryName = values[1];
                var categoryId = values[0];


                csvcontent = "";
                foreach (var row in values.Skip(2))
                    csvcontent += row + ",";
                csvcontent.TrimEnd(',');

                var description = csvcontent.Substring(0, csvcontent.IndexOf("\"", 1)+1);
                csvcontent = csvcontent.Substring(csvcontent.IndexOf("\"", 1)+2);

                values = csvcontent.Split(',');
                
                if (description.Length < 20)
                    throw new Exception("Description length should be min 20 characters");

                var email = values[0];
                var contactName = values[1];
                var amount = values[2];
                var title = "";

                if (values[3].Length > 1)
                    title = values[3];
                else
                    throw new Exception("Title length should be min 8 characters");

                var location = values[4];
                var listOfPics =  "";
                foreach (var row in values.Skip(5))
                    listOfPics += row + ",";
                listOfPics.TrimEnd(',');

                productItem.Add("categoryId", categoryId);
                productItem.Add("categoryName", categoryName);
                productItem.Add("description", description);
                productItem.Add("contactEmail", email);
                productItem.Add("contactName", contactName);
                productItem.Add("amount", amount);
                productItem.Add("title", title);
                productItem.Add("location", location);
                productItem.Add("listOfPics", listOfPics);

                productList.Add(productItem);
            }

            return productList;
        }
    }
}
