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
using System.Collections.ObjectModel; // ObservableCollection
using System.Data; // DataSet
using System.ComponentModel; // CancelEventArgs
using System.Reactive.Linq; // select, from, where, let, ..
using System.Reactive; // EventPattern

namespace Lab3
{
    public partial class MainWindow: Window
    {
        static string xmlPath = "person-data-base.xml";

        // Две коллекции. Исходная и наблюдаемая в зависимости от выборки.
        List<PersonData> personCollection;
        ObservableCollection<PersonData> personSelection;

        static List<PersonData> Serialize(DataSet xmlDataSet)
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

        /*void Deserialize(List<PersonData> list)
        { }*/

        // https://stackoverflow.com/questions/46849221/how-to-read-an-xml-file-using-xmldataprovider-in-wpf-c-sharp
        // https://stackoverflow.com/questions/27179373/xml-binding-to-datagrid-in-wpf
        void BindXml()
        {
            DataSet xmlDataSet = new DataSet();
            xmlDataSet.ReadXml(xmlPath);

            if (xmlDataSet.Tables.Count == 0)
                xmlDataSet.Tables.Add("PersonList");

            personCollection = Serialize(xmlDataSet);
            personSelection = new ObservableCollection<PersonData>(personCollection);

            PersonDataList.ItemsSource = personSelection;

            // https://stackoverflow.com/questions/4879689/how-to-determine-if-wpf-datagrid-cell-is-in-edit-mode/4879799
            // По каким-то причинам DataGrid не обновляет состояние скорого дня
            // рождения поэтому обновляем состояние сами.
            Observable.FromEventPattern<SelectedCellsChangedEventHandler, SelectedCellsChangedEventArgs>(
                h => PersonDataList.SelectedCellsChanged += h,
                h => PersonDataList.SelectedCellsChanged -= h)
                .Subscribe(o =>
                {
                    // Таким хитрым способом проверяем находится ли таблица
                    // сейчас в режиме добавления новой записи.
                    if (personSelection.Last() != PersonDataList.CurrentItem)
                        PersonDataList.Items.Refresh();
                });

            var textInput = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(h => QueryInput.TextChanged += h, h => QueryInput.TextChanged -= h);

            var textQuery =
                from evt in textInput
                let obj = evt.Sender as TextBox
                let text = obj.Text
                select text;

            // https://stackoverflow.com/questions/4493858/elegant-way-to-combine-multiple-collections-of-elements
            textQuery.Subscribe(o => { personCollection = personCollection.Union(personSelection).ToList(); });
            textQuery.Subscribe(o => { personSelection.Clear(); });

            // https://stackoverflow.com/questions/25296270/observable-where-with-async-predicate
            var personObservable =
                from query in textQuery
                let normalQuery = query.ToLower()
                from person in personCollection
                let isSatisfying = person.Name.ToLower().Contains(normalQuery)
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
