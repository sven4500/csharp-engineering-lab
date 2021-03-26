using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive; // Unit

namespace CoffeeShop
{
    public class ChangePasswordWindowVM : ReactiveObject
    {
        public LoginWindowModel Model { get; set; }

        public string CurrentUser { get; set; }

        public string CurrentPassword { private get; set; }
        public string NewPassword { private get; set; }
        public string NewRepeatPassword { private get; set; }

        private string notification;
        public string Notification { get { return notification; } set { this.RaiseAndSetIfChanged(ref notification, value); } }

        private ReactiveCommand<Unit, Unit> validateCommand = ReactiveCommand.Create<Unit, Unit>(o => o);
        public ReactiveCommand<Unit, Unit> ValidateCommand { get { return validateCommand; } }

        public ChangePasswordWindowVM()
        {
            ValidateCommand.Subscribe(o =>
            {
                if (Model.Validate(CurrentUser, CurrentPassword) && NewPassword == NewRepeatPassword)
                {
                    Model.Update(CurrentUser, NewPassword);
                    Model.Save();
                    Model.Load();
                    Notification = "Пароль обновлён";
                }
                else
                {
                    Notification = "Неверный пароль";
                }
            });
        }

    }
}
