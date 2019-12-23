using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // INotifyPropertyChanged

namespace CoffeeShop
{
    class CartRecord /*: INotifyPropertyChanged*/
    {
        /*public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

        private int id;
        public int Id { get { return id; } set { id = value; } }

        private decimal count = 0.000m;
        public decimal Count
        { 
            get 
            { 
                return count; 
            } 
            set 
            { 
                count = value;
                priceTotal = storeRecord.Price * count;
                //OnPropertyChanged("PriceTotal");
            } 
        }

        private decimal priceTotal = 0.00m;
        public decimal PriceTotal { get { return priceTotal; } }

        private StoreRecord storeRecord;
        public StoreRecord StoreRecord
        { 
            get 
            { 
                return storeRecord; 
            } 
            set 
            { 
                storeRecord = value;
                Count = Count;
            } 
        }

        public CartRecord()
        {
            storeRecord = new StoreRecord();
        }

    }
}
