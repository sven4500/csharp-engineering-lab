using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace CoffeeShop
{
    public class LoginWindowVM : ReactiveObject
    {
        private LoginWindowModel model = new LoginWindowModel();

        // В этой работе используется заранее заданное количество и типы пользователей.
        private readonly List<string> users = new List<string>(new string[] { "Менеджер", "Официант" });
        public List<string> Users { get { return users; } }

        public string CurrentUser { get; set; }

        public string LoginValidationText { get; private set; }

        protected readonly ReactiveCommand<Unit, Unit> validateCommand = ReactiveCommand.Create(() => { });
        public ReactiveCommand<Unit, Unit> ValidateCommand { get { return validateCommand; } }

        public LoginWindowVM()
        {
            ValidateCommand.Subscribe(o => Validate("123"));
        }

        public bool Validate(string password)
        {
            if (model.Validate(CurrentUser, password) == true)
            {
                // TODO: продолжаем далее и загружаем правльное для пользователя окно.
                return true;
            }
            else
            {
                LoginValidationText = "Неверный логин или пароль";
                return false;
            }
        }

    }
}
