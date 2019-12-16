using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    class LoginWindowModel
    {
        private readonly List<string> users = new List<string>(new string[] { "Менеджер", "Официант" });
        public List<string> Users { get { return users; } }

        public LoginWindowModel()
        {

        }

        public bool Validate(string password)
        {
            // TODO: проверка пароля.
            return false;
        }

    }
}
