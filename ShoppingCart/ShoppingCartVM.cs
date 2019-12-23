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

        private readonly List<string> products;
        public List<string> Products { get { return products; } }

        public ObservableCollection<CartRecord> Records { get { return model.Records; } }

        public ShoppingCartVM()
        {
            products = managerStoreModel.Records.Select(o => o.Name + " / " + o.Manufacturer).ToList();
        }

    }
}
