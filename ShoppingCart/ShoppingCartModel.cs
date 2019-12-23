using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection

namespace CoffeeShop
{
    class ShoppingCartModel
    {
        /*private string xmlPath;
        public string XmlPath { get { return xmlPath; } set { xmlPath = value; } }*/

        private readonly XmlAdapter<CartRecord> xmlAdapter = new XmlAdapter<CartRecord>(
            new[] { "Id", "Name", "Manufacturer", "Count", "Price", "PriceTotal" },
            (row) =>
            {
                CartRecord record = new CartRecord();
                record.Id = Convert.ToInt32(row["Id"]);
                record.StoreRecord.Name = Convert.ToString(row["Name"]);
                record.StoreRecord.Manufacturer = Convert.ToString(row["Manufacturer"]);
                record.Count = Convert.ToInt32(row["Count"]);
                record.StoreRecord.Price = Convert.ToDecimal(row["Price"]);
                record.PriceTotal = Convert.ToDecimal(row["PriceTotal"]);
                return record;
            },
            (row, value) =>
            {
                row["Id"] = value.Id;
                row["Name"] = value.StoreRecord.Name;
                row["Manufacturer"] = value.StoreRecord.Manufacturer;
                row["Count"] = value.Count;
                row["Price"] = value.StoreRecord.Price;
                row["PriceTotal"] = value.PriceTotal;
            });

        private readonly ObservableCollection<CartRecord> records = new ObservableCollection<CartRecord>();
        public ObservableCollection<CartRecord> Records { get { return records; } }

        public ShoppingCartModel()
        {
            xmlAdapter.DataSetName = "Bill";
            xmlAdapter.TableName = "Position";
        }

        public void Create()
        {
            xmlAdapter.XmlPath = "./bill.xml";
            xmlAdapter.Data = records.ToList();
            xmlAdapter.Save();
        }

    }
}
