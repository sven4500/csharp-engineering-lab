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
        
        public void OnClick(object sender, EventArgs e)
        {
            int a = 0;
        }

        public ShoppingCartVM()
        { }

    }
}
