using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeeShop
{
    public partial class MainManager : Window
    {
        // https://stackoverflow.com/questions/15589298/how-to-add-wpf-page-to-tabcontrol
        void MakeTabs()
        {
            Page[] pages = { new ManagerStore(), new ManagerStaff(), new ManagerOrders() };

            for (uint i = 0; i < pages.Length; ++i)
            {
                Page page = pages[i];

                Frame frame1 = new Frame();
                frame1.Content = page;

                TabItem item1 = new TabItem();
                item1.Content = frame1;
                item1.Header = page.Title;

                Pages.Items.Add(item1);
            }
        }

        public MainManager()
        {
            InitializeComponent();
            MakeTabs();
        }

    }
}
