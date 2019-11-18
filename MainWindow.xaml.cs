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
            //var grid = new Grid();
            //AlphabeticStack.Children.Add(grid);
            AlphabeticStack.Orientation = Orientation.Horizontal;

            for (char beginChar = 'А', endChar = 'Я', indexChar = beginChar; indexChar <= endChar; ++indexChar)
            {
                var button = new Button();
                //button.VerticalAlignment = VerticalAlignment.Top;
                //button.HorizontalAlignment = HorizontalAlignment.Left;
                //button.HorizontalAlignment = FigureHorizontalAnchor.ColumnLeft;
                button.Content = indexChar;
                button.Name = "Index" + indexChar;
                AlphabeticStack.Children.Add(button);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            MakeAlphabeticIndex();
        }
    }
}
