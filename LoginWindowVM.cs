using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    class LoginWindowVM
    {
        private LoginWindowModel model = new LoginWindowModel();

        public List<string> Users { get { return model.Users; } }
        public string LoginValidationText { get; private set; }
        public string PasswordText { get; set; }

        public LoginWindowVM()
        {
            
        }

        public bool Validate()
        {
            // TODO: реакция на нажатие кнопки "Войти".
            LoginValidationText = "Неверный логин или пароль";
            return false;
        }

    }
}
