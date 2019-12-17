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

        public List<string> Users { get { return model.Users; } }
        public string CurrentUser { get; set; }

        // Нужно использовать RaiseAndSetIfChanged чтобы оповестить View о том что свойство изменилось.
        private string loginValidationText;
        public string LoginValidationText { get { return loginValidationText; } private set { this.RaiseAndSetIfChanged(ref loginValidationText, value); } }

        protected readonly ReactiveCommand<string, string> validateCommand = ReactiveCommand.Create<string, string>(o => o);
        public ReactiveCommand<string, string> ValidateCommand { get { return validateCommand; } }

        public LoginWindowVM()
        {
            ValidateCommand.Subscribe(o =>
            {
                if (model.Validate(CurrentUser, o) == true)
                {
                    // TODO: продолжаем далее и загружаем правльное для пользователя окно.
                }
                else
                {
                    LoginValidationText = "Неверный логин или пароль";
                }
            });
        }
    }
}
