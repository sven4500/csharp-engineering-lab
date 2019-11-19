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

namespace Lab3
{
    public partial class MainWindow: Window
    {
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
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            MakeAlphabeticIndex();
        }
    }
}
