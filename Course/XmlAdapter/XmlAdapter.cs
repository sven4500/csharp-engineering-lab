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

        public string DataSetName { get; set; }
        public string TableName { get; set; }

        private readonly string[] columnHeaders;
        
        private List<T> data = null;
        public List<T> Data { get { return data; } set { data = value; } }

        public delegate T OnDeserialize(DataRow row);
        public delegate void OnSerialize(DataRow row, T value);

        private readonly OnDeserialize onDeserialize;
        private readonly OnSerialize onSerialize;

        public XmlAdapter(string[] columnHeaders, OnDeserialize onDeserialize, OnSerialize onSerialize)
        {
            this.columnHeaders = columnHeaders;
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

            foreach (string header in columnHeaders)
                table.Columns.Add(header);

            foreach (T value in data)
            {
                DataRow row = table.NewRow();
                onSerialize(row, value);
                table.Rows.Add(row);
            }

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
