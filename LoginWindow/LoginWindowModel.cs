using System;
using System.Collections.Generic; // Dictionary
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // DataSet

namespace CoffeeShop
{
    public class LoginWindowModel
    {
        private string xmlPath = "./users.xml";
        public string XmlPath { get { return xmlPath; } set { xmlPath = value; } }

        private Dictionary<string, string> userPassword = new Dictionary<string, string>();

        private XmlAdapter<UserData> xmlAdapter;

        // https://stackoverflow.com/questions/3968543/convert-dictionary-to-list-collection-in-c-sharp
        public List<string> Users { get { return userPassword.Select(o => o.Key).ToList(); } }

        public LoginWindowModel()
        {
            xmlAdapter = new XmlAdapter<UserData>(
                new[] { "Login", "Password" },
                (row) =>
                {
                    UserData user = new UserData();
                    user.UserName = Convert.ToString(row["Login"]);
                    user.Passwrod = Convert.ToString(row["Password"]);
                    return user;
                },
                (row, value) =>
                {
                    row["Login"] = value.UserName;
                    row["Password"] = value.Passwrod;
                });

            xmlAdapter.XmlPath = xmlPath;
            xmlAdapter.DataSetName = "Users";
            xmlAdapter.TableName = "User";

            Load();
        }

        public void Update(string login, string password)
        {
            userPassword[login] = password;
        }

        public void Save()
        {
            xmlAdapter.Data = userPassword
                .Select(o => 
                {
                    UserData user = new UserData();
                    user.UserName = o.Key;
                    user.Passwrod = o.Value;
                    return user; 
                })
                .ToList();
            xmlAdapter.Save();
        }

        public void Load()
        {
            xmlAdapter.Load();
            userPassword = xmlAdapter
                .Data
                .ToDictionary(o => o.UserName, o => o.Passwrod);
        }

        public bool Validate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;
            return String.Compare(userPassword[login], password) == 0;
        }

    }
}
