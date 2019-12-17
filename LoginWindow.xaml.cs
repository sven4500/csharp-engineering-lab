using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace CoffeeShop
{
    // https://reactiveui.net/docs/handbook/data-binding/windows-presentation-foundation
    public partial class LoginWindow : Window, IViewFor<LoginWindowVM>
    {
        //public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(LoginWindowVM), typeof(LoginWindow));

        public LoginWindowVM ViewModel { get { return DataContext as LoginWindowVM; } set { } }
        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as LoginWindowVM; } }

        public LoginWindow()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.ValidateCommand, x => x.ValidateButton);
            });
        }
    }
}
