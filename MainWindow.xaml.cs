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
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive; // EventPattern

namespace Lab3
{
    public partial class MainWindow: Window
    {
        static string xmlPath = "person-data-base.xml";
        DataSet xmlDataSet = new DataSet();

        // Две коллекции. Исходная и наблюдаемая в зависимости от выборки.
        ObservableCollection<PersonData> personCollection;
        ObservableCollection<PersonData> personSelection;

        static List<PersonData> Serialize(DataSet xmlDataSet)
        {
            return xmlDataSet.Tables[0].AsEnumerable()
                .Select(dataRow =>
                {
                    return new PersonData
                    {
                        Name = dataRow.IsNull("Name") ? "" : dataRow.Field<string>("Name"),
                        //DateOfBirth = dataRow.Field<DateTime>("DateOfBirth")
                        ContactNumber = dataRow.Field<string>("ContactNumber"),
                        PersonalContactNumber = dataRow.Field<string>("PersonalContactNumber"),
                        EmailAddress = dataRow.Field<string>("EmailAddress"),
                        SkypeAddress = dataRow.Field<string>("SkypeAddress"),
                        Comment = dataRow.Field<string>("Comment")
                    };
                })
                .ToList();
        }

        /*void Deserialize(List<PersonData> list)
        { }*/

        // https://stackoverflow.com/questions/46849221/how-to-read-an-xml-file-using-xmldataprovider-in-wpf-c-sharp
        // https://stackoverflow.com/questions/27179373/xml-binding-to-datagrid-in-wpf
        void BindXml()
        {
            xmlDataSet.ReadXml(xmlPath);

            if (xmlDataSet.Tables.Count == 0)
                xmlDataSet.Tables.Add("PersonList");

            // Сериализуем данные. Сперва превращаем в List<> а после в
            // Collection<> и ObservableCollection<>.
            var list = Serialize(xmlDataSet);

            personCollection = new ObservableCollection<PersonData>(list);
            personSelection = new ObservableCollection<PersonData>(list);

            PersonDataList.ItemsSource = personSelection;

            var textInput = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(h => QueryInput.TextChanged += h, h => QueryInput.TextChanged -= h);

            var textQuery =
                from evt in textInput
                let obj = evt.Sender as TextBox
                let text = obj.Text
                select text;

            textQuery.Subscribe(o => personSelection.Clear());

            // https://stackoverflow.com/questions/25296270/observable-where-with-async-predicate
            var personObservable =
                from query in textQuery
                from person in personCollection
                let isSatisfying = person.Name.ToLower().Contains(query.ToLower())
                where isSatisfying == true
                select person;

            personObservable.Subscribe(o => personSelection.Add(o));
        }

        private void OnClosing(object sender, EventArgs e)
        {
            //xmlDataSet.WriteXml(xmlPath);
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
                Grid.SetColumn(button, indexChar - beginChar);

                AlphabeticGrid.Children.Add(button);

                // Создали наблюдаемую переменную. Теперь когда вызовем
                // Subscribe наблюдателя, то будет вызвна лямбда.
                var buttonClick = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(h => button.Click += h, h => button.Click -= h);

                buttonClick.Subscribe(arg =>
                {
                    Button but = arg.Sender as Button;
                    if (but == null)
                        return;
                    QueryInput.Text += but.Content.ToString().ToLower();
                });
            }
        }

        void RemoveItem(object sender, EventArgs e)
        {
            PersonData person = PersonDataList.SelectedItem as PersonData;
            if (person != null)
            {
                personCollection.Remove(person);
                personSelection.Remove(person);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            MakeAlphabeticIndex();
            BindXml();
        }
    }
}
