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
        private readonly ObservableCollection<CartRecord> records = new ObservableCollection<CartRecord>();
        public ObservableCollection<CartRecord> Records { get { return records; } }

        public ShoppingCartModel()
        { }

        public void Save()
        { }
        
        public void Load()
        { }

    }
}
