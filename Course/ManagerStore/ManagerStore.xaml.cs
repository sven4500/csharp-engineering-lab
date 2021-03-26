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
    public partial class ManagerStore : Page
    {
        // ѕочему-то среда разваливаетс€ если контекст данных определ€ем в XAML так что делаем программно.
        private readonly ManagerStoreVM modelVM = new ManagerStoreVM();

        public ManagerStore()
        {
            InitializeComponent();
            DataContext = modelVM;
            SaveButton.Click += modelVM.Save;
            RemoveButton.Click += modelVM.Remove;
        }

    }
}
