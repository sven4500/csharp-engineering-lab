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
            /*Page[] pages = { new ManagerStore(), new ManagerStaff(), new ManagerOrders() };

            for (uint i = 0; i < pages.Length; ++i)
            {
                Page page = pages[i];
                
                Frame frame = new Frame();
                frame.Content = page;

                TabItem item = new TabItem();
                item.Content = frame;
                item.Header = page.Title;

                Pages.Items.Add(item);
            }*/
        }

        public MainManager()
        {
            InitializeComponent();
            MakeTabs();
        }

    }
}
