using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComplexCalculatorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlFacade controlFacade = new ControlFacade();
        public MainWindow()
        {
            //Converter converter = new Converter();
            //converter.ExecuteExpression("7 + 7 =");
            //converter.ExecuteExpression("7i + 7i =");
            //converter.ExecuteExpression("2i =");
            //converter.ExecuteExpression("2+1i + -7+2i / 8+2i =");
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Input_TextBox.Text = controlFacade.DoEdit(Convert.ToInt32((sender as Button).Tag));

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string resultInputBoxDialog = "NoValue";
            if (sender is Button)
            {
                switch ((sender as Button).Tag.ToString())
                {
                    case "ROOT":
                        resultInputBoxDialog = CreateAndShowInputBoxDialog("Введите показатель корня:", "0");
                        break;
                    case "POW":
                        resultInputBoxDialog = CreateAndShowInputBoxDialog("Введите показатель степени:", "0");
                        break;
                }
                if (resultInputBoxDialog != String.Empty && resultInputBoxDialog != "NoValue")
                    Output_TextBox.Text = controlFacade.DoCommand((sender as Button).Tag.ToString(), int.Parse(resultInputBoxDialog));
                else if (resultInputBoxDialog == "NoValue")
                    Output_TextBox.Text = controlFacade.DoCommand((sender as Button).Tag.ToString());

                Input_TextBox.Text = Output_TextBox.Text;
                controlFacade.UpdateEditorExpression(Input_TextBox.Text);
            }
        }
        private string CreateAndShowInputBoxDialog(string question, string defaultAnswer)
        {
            InputBoxDialog inputDialog = new InputBoxDialog(question, defaultAnswer);
            if (inputDialog.ShowDialog() == true)
                return inputDialog.Answer;
            else
                return String.Empty;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            int commandId = -1;
            string operationName = String.Empty;
            if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                commandId = Convert.ToInt32(e.Key.ToString().Substring(1));

            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                commandId = Convert.ToInt32(e.Key.ToString().Substring(6));
            }
            else if (e.Key == Key.Enter)
            {
                commandId = 14;
            }
            else if (e.Key == Key.I)
            {
                commandId = 15;
            }
            else if (e.Key == Key.Add)
            {
                commandId = 10;
            }
            else if (e.Key == Key.Subtract)
            {
                commandId = 11;
            }
            else if (e.Key == Key.Divide)
            {
                commandId = 12;
            }
            else if (e.Key == Key.Multiply)
            {
                commandId = 13;
            }
            else if (e.Key == Key.Delete)
            {
                commandId = 17;
            }
            else if (e.Key == Key.Back)
            {
                commandId = 16;
            }
            else if (e.Key == Key.OemPeriod)
            {
                commandId = 19;
            }
            else if (e.Key == Key.OemOpenBrackets)
            {
                commandId = 20;
            }
            else if (e.Key == Key.OemCloseBrackets)
            {
                commandId = 21;
            }
            try
            {
                if (commandId >= 0 && (commandId <= 21)) Input_TextBox.Text = controlFacade.DoEdit(commandId);
            }
            catch (Exception error)
            {
                if (MessageBox.Show(error.Message, "Ошибка!",
                    MessageBoxButton.OK) == MessageBoxResult.Yes)
                {
                    return;
                }
            }
        }

        private void SelectViewModForInputTextBox(object sender, RoutedEventArgs e)
        {
            this.Input_TextBox.Text = controlFacade.DoCommand((sender as Button).Tag.ToString());
        }
        private void SelectViewModForOutputTextBox(object sender, RoutedEventArgs e)
        {
            this.Output_TextBox.Text = controlFacade.DoCommand((sender as Button).Tag.ToString());
        }

    }
    
}
