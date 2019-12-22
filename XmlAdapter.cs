using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // DataSet, DataRow, ..

namespace CoffeeShop
{
    public class XmlAdapter<T>
    {
        public string XmlPath { get; set; }

        private string DataSetName { get; set; }
        private string TableName { get; set; }
        public List<string> ColumnHeaders { get; set; }

        private List<T> data = null;
        public List<T> Data { get { return data; } }

        public delegate T OnDeserialize(DataRow row);
        public delegate DataRow OnSerialize(T value);

        protected OnDeserialize onDeserialize;
        protected OnSerialize onSerialize;

        public XmlAdapter(OnDeserialize onDeserialize, OnSerialize onSerialize)
        {
            this.onDeserialize = onDeserialize;
            this.onSerialize = onSerialize;
        }

        private List<T> Deserialize(DataSet dataSet)
        {
            List<T> list = new List<T>();

            if (dataSet.Tables.Count == 0)
                return list;

            foreach (DataRow row in dataSet.Tables[0].Rows)
                list.Add(onDeserialize(row));

            return list;
        }

        private DataSet Serialize(List<T> data)
        {
            DataTable table = new DataTable();
            table.TableName = TableName;

            foreach (string header in ColumnHeaders)
                table.Columns.Add(header);

            foreach (T value in data)
                table.Rows.Add(onSerialize(value));

            DataSet dataSet = new DataSet();
            dataSet.DataSetName = DataSetName;
            dataSet.Tables.Add(table);

            return dataSet;
        }

        public void Load()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(XmlPath);
            data = Deserialize(dataSet);
        }

        public void Save()
        {
            DataSet dataSet = Serialize(data);
            dataSet.WriteXml(XmlPath);
        }

    }
}
