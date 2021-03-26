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

        // Используем модель менеджера для того чтобы загрузить список всех возможных товаров.
        private readonly ManagerStoreModel managerStoreModel = new ManagerStoreModel();
        public ManagerStoreModel ManagerStoreModel { get { return managerStoreModel; } }

        private readonly ObservableCollection<CartRecord> records = new ObservableCollection<CartRecord>();
        public ObservableCollection<CartRecord> Records { get { return records; } }

        public ShoppingCartModel()
        {
            xmlAdapter.DataSetName = "Bill";
            xmlAdapter.TableName = "Position";
        }

        public void Create()
        {
            // https://stackoverflow.com/questions/5622854/how-do-i-show-a-save-as-dialog-in-wpf
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "bill";
            dialog.DefaultExt = ".xml";
            dialog.Filter = "XML files|*.xml";

            if (dialog.ShowDialog() == true)
            {
                xmlAdapter.XmlPath = dialog.FileName;
                xmlAdapter.Data = records.ToList();
                xmlAdapter.Save();

                // Обновляем информацию на складе. Так как везде используем ссылки, то не нужно желать ничего для актуализации информации.
                // Просто сохраняем данные в магазин.
                managerStoreModel.Save();

                records.Clear();
            }
        }

    }
}
