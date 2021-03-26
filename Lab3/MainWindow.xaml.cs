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
using System.ComponentModel; // CancelEventArgs
using System.Reactive.Linq; // select, from, where, let, ..
using System.Reactive; // EventPattern

namespace Lab3
{
    public partial class MainWindow: Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MakeAlphabeticIndex();
            InitializeViewModel();
        }

        protected void InitializeViewModel()
        {
            MainWindowVM viewModel = DataContext as MainWindowVM;
            if (viewModel == null)
                return;

            Closing += viewModel.OnClosing;
            RemoveItemButton.Click += viewModel.OnRemoveItem;

            var textInput = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(h => QueryInput.TextChanged += h, h => QueryInput.TextChanged -= h);

            var textQuery =
                from evt in textInput
                let obj = evt.Sender as TextBox
                let text = obj.Text
                select text;

            textQuery.Subscribe(o =>
            {
                viewModel.MergeCollectionWithSelection();
                viewModel.ClearSelection();
            });

            // https://stackoverflow.com/questions/25296270/observable-where-with-async-predicate
            var personObservable =
                from query in textQuery
                let normalQuery = query.ToLower()
                from person in viewModel.PersonCollection
                where person.Name.ToLower().Contains(normalQuery) || person.ContactNumber.Contains(normalQuery) || person.PersonalContactNumber.Contains(normalQuery) ||
                    person.EmailAddress.ToLower().Contains(normalQuery) || person.SkypeAddress.Contains(normalQuery) || person.Comment.ToLower().Contains(normalQuery)
                select person;

            personObservable.Subscribe(o =>
            {
                viewModel.AddPersonToSelection(o);
            });
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
    }
}
