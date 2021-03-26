using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection

namespace CoffeeShop
{
    class ManagerStaffVM
    {
        private readonly ManagerStaffModel model = new ManagerStaffModel();

        public ObservableCollection<StaffRecord> Records { get { return model.Records; } }
        public StaffRecord CurrentRecord { get; set; }

        public ManagerStaffVM()
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
