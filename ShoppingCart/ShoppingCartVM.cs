using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection

namespace CoffeeShop
{
    class ShoppingCartVM
    {
        private readonly ShoppingCartModel model = new ShoppingCartModel();

        // Используем модель менеджера для того чтобы загрузить список всех возможных товаров.
        private readonly ManagerStoreModel managerStoreModel = new ManagerStoreModel();

        public ObservableCollection<StoreRecord> AvailableProducts { get { return managerStoreModel.Records; } }

        public ObservableCollection<CartRecord> Records { get { return model.Records; } }

        public CartRecord CurrentRecord { get; set; }
        
        public void RemoveButtonClick(object sender, EventArgs e)
        {
            Records.Remove(CurrentRecord);
        }

        public void CreateButtonClick(object sender, EventArgs e)
        {
            model.Save();
        }

        public void EnumerateElements(object sender, EventArgs e)
        {
            int i = 0;
            foreach(CartRecord record in Records)
                record.Id = i++;
        }

        public ShoppingCartVM()
        {
            // https://stackoverflow.com/questions/11293607/does-wpf-datagrid-fire-an-event-when-a-row-is-added-removed/41667368
            Records.CollectionChanged += EnumerateElements;
        }

    }
}
