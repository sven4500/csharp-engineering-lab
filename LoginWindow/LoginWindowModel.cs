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

        // https://stackoverflow.com/questions/3968543/convert-dictionary-to-list-collection-in-c-sharp
        public List<string> Users { get { return userPassword.Select(o => o.Key).ToList(); } }

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
            DataTable table = new DataTable();
            table.TableName = "User";
            table.Columns.Add("Login");
            table.Columns.Add("Password");

            foreach (KeyValuePair<string, string> entry in userPassword)
                table.Rows.Add(entry.Key, entry.Value);

            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "Users";
            dataSet.Tables.Add(table);

            return dataSet;
        }

        public void Update(string login, string password)
        {
            userPassword[login] = password;
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
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;
            return String.Compare(userPassword[login], password) == 0;
        }

    }
}
