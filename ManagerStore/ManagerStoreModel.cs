using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection

namespace CoffeeShop
{
    class ManagerStoreModel
    {
        private readonly string xmlPath = "./store.xml";

        private readonly XmlAdapter<StoreRecord> xmlAdapter = new XmlAdapter<StoreRecord>(
            new[] { "Name", "Category", "Manufacturer", "Count", "Price" },
            (row) =>
            {
                StoreRecord record = new StoreRecord();
                record.Category = Convert.ToString(row["Category"]);
                record.Count = Convert.ToDecimal(row["Count"]);
                record.Manufacturer = Convert.ToString(row["Manufacturer"]);
                record.Name = Convert.ToString(row["Name"]);
                record.Price = Convert.ToDecimal(row["Price"]);
                return record;
            },
            (row, value) =>
            {
                row["Category"] = value.Category;
                row["Count"] = value.Count;
                row["Manufacturer"] = value.Manufacturer;
                row["Name"] = value.Name;
                row["Price"] = value.Price;
            });

        private ObservableCollection<StoreRecord> records = new ObservableCollection<StoreRecord>();
        public ObservableCollection<StoreRecord> Records { get { return records; } }
                
        public ManagerStoreModel()
        {
            xmlAdapter.XmlPath = xmlPath;
            xmlAdapter.TableName = "Record";
            xmlAdapter.DataSetName = "StoreRecords";
            xmlAdapter.Load();

            foreach (StoreRecord record in xmlAdapter.Data)
                records.Add(record);
        }

        public void Save()
        {
            List<StoreRecord> list = new List<StoreRecord>();
            foreach (StoreRecord record in records)
                list.Add(record);
            xmlAdapter.Data = list;
            xmlAdapter.Save();
        }

    }
}
