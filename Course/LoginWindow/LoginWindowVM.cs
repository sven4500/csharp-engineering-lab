using System;
using System.Windows; // Window
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

        // Команда на выполнения входа в систему.
        protected readonly ReactiveCommand<string, string> validateCommand = ReactiveCommand.Create<string, string>(o => o);
        public ReactiveCommand<string, string> ValidateCommand { get { return validateCommand; } }

        // Команда на желание пользователя сменить пароль.
        protected readonly ReactiveCommand<Unit, Unit> changePasswordCommand = ReactiveCommand.Create<Unit, Unit>(o => o);
        public ReactiveCommand<Unit, Unit> ChangePasswordCommand { get { return changePasswordCommand; } }

        public LoginWindowVM()
        {
            ValidateCommand.Subscribe(o =>
            {
                if (model.Validate(CurrentUser, o) == true)
                {
                    // TODO: продолжаем далее и загружаем правльное для пользователя окно.
                    LoginValidationText = "";
                    Window dialog = null;
                    switch (Users.IndexOf(CurrentUser))
                    { 
                        case 0:
                            dialog = new MainManager();
                            break;
                        case 1:
                            dialog = new ShoppingCart();
                            break;
                        default:
                            dialog = null;
                            break;
                    }
                    dialog.Title = "Пользователь: " + CurrentUser;
                    dialog.ShowDialog();
                }
                else
                {
                    LoginValidationText = "Неверный логин или пароль";
                }
            });

            ChangePasswordCommand.Subscribe(o =>
            {
                // https://stackoverflow.com/questions/2796470/wpf-create-a-dialog-prompt
                var dialog = new ChangePasswordWindow();
                dialog.Model = model;
                dialog.CurrentUser = CurrentUser;
                dialog.ShowDialog();
            });
        }
    }
}
