using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data; // DataSet
using System.ComponentModel; // CancelEventArgs

namespace Lab3
{
    public partial class MainWindow: Window
    {
        string xmlPath = "person-data-base.xml";
        DataSet xmlDataSet = new DataSet();
        DataView dataView;
        
        // https://stackoverflow.com/questions/46849221/how-to-read-an-xml-file-using-xmldataprovider-in-wpf-c-sharp
        // https://stackoverflow.com/questions/27179373/xml-binding-to-datagrid-in-wpf
        void BindXml()
        {
            xmlDataSet.ReadXml(xmlPath);
            dataView = new DataView(xmlDataSet.Tables[0]);
            PersonDataList.ItemsSource = dataView;
        }

        private void OnClosing(object sender, EventArgs e)
        {
            xmlDataSet.WriteXml(xmlPath);
        }

        protected void MakeAlphabeticIndex()
        {
            //AlphabeticGrid.ShowGridLines = true;
            //AlphabeticGrid.Background = new SolidColorBrush(Colors.Aqua);

            for (char beginChar = 'А', endChar = 'Я', indexChar = beginChar; indexChar <= endChar; ++indexChar)
            {
                // Добавляем новый столбец.
                var definition = new ColumnDefinition();
                definition.Width = new GridLength(1, GridUnitType.Star);
                AlphabeticGrid.ColumnDefinitions.Add(definition);

                var button = new Button();
                button.Content = indexChar;
                button.Name = "Index" + indexChar;
                //button.Click +=
                Grid.SetColumn(button, indexChar - beginChar);

                AlphabeticGrid.Children.Add(button);
            }
        }

        void RemoveItem(object sender, EventArgs e)
        {
            var index = PersonDataList.Items.IndexOf(PersonDataList.SelectedItem);
            if (index >= 0 && index < dataView.Count)
                dataView.Delete(index);
        }

        public MainWindow()
        {
            InitializeComponent();
            MakeAlphabeticIndex();
            BindXml();
        }
    }
}
