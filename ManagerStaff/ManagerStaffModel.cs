using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection

namespace CoffeeShop
{
    class ManagerStaffModel
    {
        private readonly string xmlPath = "./staff.xml";

        private readonly XmlAdapter<StaffRecord> xmlAdapter = new XmlAdapter<StaffRecord>(row =>
        {
            StaffRecord record = new StaffRecord();
            record.Id = Convert.ToUInt32(row["Id"]);
            record.LastName = Convert.ToString(row["LastName"]);
            record.FirstName = Convert.ToString(row["FirstName"]);
            record.MiddleName = Convert.ToString(row["MiddleName"]);
            record.ContactNumber = Convert.ToString(row["ContactNumber"]);
            record.Position = Convert.ToString(row["Position"]);
            record.EmploymentDate = Convert.ToDateTime(row["EmploymentDate"] as string);
            return record;
        }, (row, value) =>
        {
            row["Id"] = value.Id;
            row["LastName"] = value.LastName;
            row["FirstName"] = value.FirstName;
            row["MiddleName"] = value.MiddleName;
            row["ContactNumber"] = value.ContactNumber;
            row["Position"] = value.Position;
            row["EmploymentDate"] = value.EmploymentDate;
        });

        private ObservableCollection<StaffRecord> records = new ObservableCollection<StaffRecord>();
        public ObservableCollection<StaffRecord> Records { get { return records; } }

        public ManagerStaffModel()
        {
            xmlAdapter.XmlPath = xmlPath;
            xmlAdapter.ColumnHeaders.AddRange(new[] { "Id", "LastName", "FirstName", "MiddleName", "ContactNumber", "Position", "EmploymentDate" });
            xmlAdapter.TableName = "Member";
            xmlAdapter.DataSetName = "Staff";
            xmlAdapter.Load();

            // https://stackoverflow.com/questions/5561156/convert-listt-to-observablecollectiont-in-wp7
            foreach (StaffRecord record in xmlAdapter.Data)
                records.Add(record);
        }

        public void Save()
        {
            List<StaffRecord> list = new List<StaffRecord>();
            foreach (StaffRecord record in records)
                list.Add(record);
            xmlAdapter.Data = list;
            xmlAdapter.Save();
        }

    }
}
