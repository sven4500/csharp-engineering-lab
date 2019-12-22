using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel; // INotifyPropertyChanged

namespace CoffeeShop
{
    class StoreRecord /*: INotifyPropertyChanged*/
    {
        public string Name { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }

        public StoreRecord()
        {
            Name = "";
            Manufacturer = "";
            Category = "";
        }

    }
}
