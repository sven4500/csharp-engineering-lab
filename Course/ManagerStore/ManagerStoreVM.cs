using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection

namespace CoffeeShop
{
    class ManagerStoreVM
    {
        private readonly ManagerStoreModel model = new ManagerStoreModel();

        public ObservableCollection<StoreRecord> StoreRecords { get { return model.Records; } }
        public StoreRecord CurrentRecord { get; set; }

        public ManagerStoreVM()
        { }

        public void Save(object sender, EventArgs e)
        {
            model.Save();
        }

        public void Remove(object sender, EventArgs e)
        {
            model.Records.Remove(CurrentRecord);
        }

    }
}
