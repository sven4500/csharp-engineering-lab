using System;
using System.Diagnostics; // Debug.Assert
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

namespace Lab2
{
    /// <summary>
    /// MainWindow.xaml.cs описывает поведение конкретного окна MainWindow.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but == null)
                return;
            String num = but.Name.Substring(but.Name.Length - 1);
            TextBox expr = this.FindName("Expr") as TextBox;
            if (expr == null)
                return;
            if (expr.Text == "0")
            {
                if (num != "0")
                {
                    expr.Text = num;
                }
            }
            else
            {
                expr.Text += num;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but == null)
                return;
            TextBox expr = this.FindName("Expr") as TextBox;
            if (expr == null)
                return;
            Debug.Assert(expr.Text.Length >= 1);
            if (expr.Text.Length == 1)
            {
                expr.Text = "0";
            }
            else
            {
                // Проверяем что было введено последним: цифра или операция. Операция всегда находится в промежутке между пробелами.
                if (expr.Text[expr.Text.Length - 1] == ' ')
                { 
                    expr.Text = expr.Text.Substring(0, expr.Text.Length - 3);
                }
                else
                {
                    expr.Text = expr.Text.Substring(0, expr.Text.Length - 1);
                }
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but == null)
                return;
            TextBox expr = this.FindName("Expr") as TextBox;
            if (expr == null)
                return;
            Debug.Assert(expr.Text.Length >= 1);
            // Если последним введённым символом была комманда, то новую комманду не добавляем.
            if (expr.Text[expr.Text.Length - 1] != ' ')
            {
                String oper = but.Name.Substring(but.Name.Length - 3);
                switch (oper)
                {
                    case "add":
                        expr.Text += " + ";
                        break;
                    case "sub":
                        expr.Text += " - ";
                        break;
                    case "mul":
                        expr.Text += " * ";
                        break;
                    case "div":
                        expr.Text += " / ";
                        break;
                    default:
                        break;
                }
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but == null)
                return;
            TextBox expr = this.FindName("Expr") as TextBox;
            TextBox hist = this.FindName("Hist") as TextBox;
            if (expr == null || hist == null)
                return;
            // Запрещаем вычисление если последний введённый символ это операция.
            if (expr.Text[expr.Text.Length - 1] == ' ')
                return;
            String result = Parse(expr.Text);
            hist.Text += expr.Text + " = " + result + "\n";
            hist.ScrollToEnd();
            expr.Text = result;
        }

        static String Parse(String expr)
        {
            String[] tokens = expr.Substring(0).Split(' ');
            UInt32 pos = 0;
            return ParseSub(tokens, ref pos);
        }
        
        static String ParseSub(String[] tokens, ref UInt32 pos)
        {
            Int32 result = Convert.ToInt32(tokens[pos++]);
            while (pos < tokens.Length)
            {
                String operation = tokens[pos++];
                String nextOperation = (pos + 1 < tokens.Length) ? tokens[pos + 1] : "";
                Int32 operand = ((nextOperation != "*" && nextOperation != "/") && (operation == "+" || operation == "-")) ? Convert.ToInt32(tokens[pos++]) : Convert.ToInt32(ParseSub(tokens, ref pos));
                switch (operation)
                { 
                    case "+":
                        result += operand;
                        break;
                    case "-":
                        result -= operand;
                        break;
                    case "*":
                        result *= operand;
                        break;
                    case "/":
                        result /= operand;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            return result.ToString();
        }
    }
}
