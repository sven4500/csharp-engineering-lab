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
using System.Security; // SecureString

namespace CoffeeShop
{
    // https://reactiveui.net/docs/handbook/data-binding/windows-presentation-foundation
    public partial class LoginWindow : Window, IViewFor<LoginWindowVM>
    {
        public LoginWindowVM ViewModel { get { return DataContext as LoginWindowVM; } set { } }
        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as LoginWindowVM; } }

        public LoginWindow()
        {
            InitializeComponent();

            // Так как элемент управления PasswordBox не позволяет использовать связывание поэтому используем здесь "плохой" способ.
            // https://willspeak.me/2017/04/14/the-pragmatic-passwordbox.html
            var passwordObservable = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(h => PasswordTextBox.PasswordChanged += h, h => PasswordTextBox.PasswordChanged -= h)
                .Select(o =>
                {
                    PasswordBox pb = o.Sender as PasswordBox;
                    if (pb == null)
                        return "";
                    return pb.Password;
                });

            // WhenActivated позволяет правильно очистить ресурсы.
            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.ValidateCommand, x => x.ValidateButton, passwordObservable);
                this.BindCommand(ViewModel, x => x.ChangePasswordCommand, x => x.ChangePasswordButton);
            });
        }
    }
}
