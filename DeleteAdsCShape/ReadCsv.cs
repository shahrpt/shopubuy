using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DeleteAdsCShape
{
    public class ReadCSV
    {
        public static List<Dictionary<string, string>> GetProductList(string filepath)
        {
            List<Dictionary<string, string>> productList = new List<Dictionary<string, string>>();


            var csv_reader = File.ReadAllLines(filepath);

            try
            {
                foreach (var row in csv_reader)
                {
                    var values = row.Split(',');
                    foreach (var value in values)
                    {
                        Dictionary<string, string> item = new Dictionary<string, string>();
                        item.Add("adId", value);
                        productList.Add(item);
                    }
                }
            }
            catch
            { }
            
            return productList;
        }
    }
}
