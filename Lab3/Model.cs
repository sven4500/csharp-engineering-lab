using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection
using System.Data; // DataSet

namespace Lab3
{
    class Model
    {
        static readonly string xmlPath = "./person-data-base.xml";

        // Две коллекции. Исходная и наблюдаемая которая зависит от выборки.
        public List<PersonData> PersonCollection { get; private set; }
        public ObservableCollection<PersonData> PersonSelection { get; private set; }

        // https://stackoverflow.com/questions/46849221/how-to-read-an-xml-file-using-xmldataprovider-in-wpf-c-sharp
        private static List<PersonData> Serialize(DataSet xmlDataSet)
        {
            if (xmlDataSet.Tables.Count == 0)
                return new List<PersonData>();

            return xmlDataSet.Tables[0].AsEnumerable()
                .Select(dataRow =>
                {
                    // https://stackoverflow.com/questions/7104675/difference-between-getting-value-from-datarow
                    return new PersonData
                    {
                        Name = Convert.ToString(dataRow["Name"]),
                        // Тут не совсем ясно почему программа выдаёт ошибку
                        // InvalidCastException если не использовать as string?
                        DateOfBirth = Convert.ToDateTime(dataRow["DateOfBirth"] as string),
                        ContactNumber = Convert.ToString(dataRow["ContactNumber"]),
                        PersonalContactNumber = Convert.ToString(dataRow["PersonalContactNumber"]),
                        EmailAddress = Convert.ToString(dataRow["EmailAddress"]),
                        SkypeAddress = Convert.ToString(dataRow["SkypeAddress"]),
                        Comment = Convert.ToString(dataRow["Comment"])
                    };
                })
                .ToList();
        }

        private static DataSet Deserialize(List<PersonData> list)
        {
            DataTable table = new DataTable();
            table.TableName = "Person";

            table.Columns.Add("Name");
            table.Columns.Add("DateOfBirth");
            table.Columns.Add("ContactNumber");
            table.Columns.Add("PersonalContactNumber");
            table.Columns.Add("EmailAddress");
            table.Columns.Add("SkypeAddress");
            table.Columns.Add("Comment");
            table.Columns.Add("IsBirthdaySoon");

            foreach (PersonData person in list)
                table.Rows.Add(person.Name, person.DateOfBirth, person.ContactNumber, person.PersonalContactNumber, person.EmailAddress, person.SkypeAddress, person.Comment);

            DataSet xmlDataSet = new DataSet();
            xmlDataSet.DataSetName = "PersonList";
            xmlDataSet.Tables.Add(table);

            return xmlDataSet;
        }

        public Model()
        {
            DataSet xmlDataSet = new DataSet();
            xmlDataSet.ReadXml(xmlPath);

            PersonCollection = Serialize(xmlDataSet);
            PersonSelection = new ObservableCollection<PersonData>(PersonCollection);
        }

        public void MergeCollectionWithSelection()
        {
            // https://stackoverflow.com/questions/4493858/elegant-way-to-combine-multiple-collections-of-elements
            PersonCollection = PersonCollection.Union(PersonSelection).ToList();
        }

        public void SaveXml()
        {
            MergeCollectionWithSelection();
            DataSet xmlDataSet = Deserialize(PersonCollection);
            xmlDataSet.WriteXml(xmlPath);
        }

        public void RemoveItem(PersonData person)
        {
            PersonCollection.Remove(person);
            PersonSelection.Remove(person);
        }
    }
}
