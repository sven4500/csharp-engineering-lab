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
using System.Windows.Shapes;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency; // DispatcherScheduler

namespace CoffeeShop
{
    public partial class ChangePasswordWindow : Window, IViewFor<ChangePasswordWindowVM>
    {
        public ChangePasswordWindowVM ViewModel { get { return DataContext as ChangePasswordWindowVM; } set { } }
        object IViewFor.ViewModel { get { return ViewModel; } set { } }

        // У этого окна нет своей модели. Оно использует модель LoginWindow.
        public LoginWindowModel Model { get { return ViewModel.Model; } set { ViewModel.Model = value; } }

        public string CurrentUser { get { return ViewModel.CurrentUser; } set { ViewModel.CurrentUser = value; } }

        public ChangePasswordWindow()
        {
            InitializeComponent();

            // Наблюдаем за изменением пароля во всех трёх формах сразу. При помощи Select выбираем только тот элемент который нас интересует.
            // Наблюдается ошибка многопоточности при использовании Throttle. Подробнее по ссылке.
            // https://stackoverflow.com/questions/8766812/cross-thread-exception-when-using-rx-throttle
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(h =>
                {
                    CurrentPasswordBox.PasswordChanged += h;
                    NewPasswordBox.PasswordChanged += h;
                    NewRepeatPasswordBox.PasswordChanged += h;
                },
                h =>
                {
                    CurrentPasswordBox.PasswordChanged -= h;
                    NewPasswordBox.PasswordChanged -= h;
                    NewRepeatPasswordBox.PasswordChanged -= h;
                })
                .Select(o => o)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .ObserveOn(DispatcherScheduler.Current)
                .Subscribe(o =>
                {
                    PasswordBox box = o.Sender as PasswordBox;
                    if (box.Equals(CurrentPasswordBox))
                        ViewModel.CurrentPassword = box.Password;
                    else if (box.Equals(NewPasswordBox))
                        ViewModel.NewPassword = box.Password;
                    else if (box.Equals(NewRepeatPasswordBox))
                        ViewModel.NewRepeatPassword = box.Password;
                });

            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.ValidateCommand, x => x.ValidateButton);
            });
        }

    }
}
