using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic; // Dictionary
using System.Data; // DataSet

namespace CoffeeShop
{
    class LoginWindowModel
    {
        private string xmlPath = "./users.xml";
        public string XmlPath { get { return xmlPath; } set { xmlPath = value; } }

        private Dictionary<string, string> userPassword = new Dictionary<string, string>();

        public LoginWindowModel()
        {
            Load();
        }

        public Dictionary<string, string> Deserizalize(DataSet dataSet)
        {
            return dataSet
                .Tables[0]
                .AsEnumerable()
                .Select(row => new Tuple<string, string>(Convert.ToString(row["Login"]), Convert.ToString(row["Password"])))
                .ToDictionary(o => o.Item1, o => o.Item2);
        }

        public DataSet Serialize(Dictionary<string, string> userPassword)
        {
            // TODO: алгоритм сериализации.
            return new DataSet();
        }

        public void Save()
        {
            DataSet dataSet = Serialize(userPassword);
            dataSet.WriteXml(xmlPath);
        }

        public void Load()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlPath);
            userPassword = Deserizalize(dataSet);
        }

        public bool Validate(string login, string password)
        {
            return String.Compare(userPassword[login], password) == 0;
        }

    }
}
