using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculatorWPF
{
    public class Editor
    {
        //Поле для хранения редактируемого числа.
        private string expression = "";
        //Разделитель целой и мнимой частей.
        private const string delim = "i";
        //Ноль.
        private const string zero = "0";

        public string Number { get => Expression; set => Expression = value; }
        public string Expression { get => expression; set => expression = value; }

        public string Delim { get => delim; }

        //Свойствое для чтения редактируемого числа.

        //Добавить цифру.
        public string AddDigit(int n)
        {
            this.Expression += Convert.ToString(n);
            return this.Number;
        }
        //Добавить ноль.
        public string AddZero()
        {
            this.Expression += zero;
            return this.Number;
        }
        //Добавить разделитель.
        public string AddDelim()
        {

            if (Number.Last().ToString().Equals(Delim) || Number.Length == 0)
                return this.Number;
            else
                this.Number += Delim;
            return this.Number;
        }
        //Добавить точку.
        public string AddPoint()
        {

            if (Number.Last().ToString().Equals(Delim) || Number.Length == 0)
                return this.Number;
            else
                this.Number += ".";
            return this.Number;
        }
        //Удалить символ справа.
        public string Bs()
        {
            if (this.Number.Length > 0)
                this.Number = this.Number.Remove(this.Number.Length - 1, 1);
            return this.Number;
        }
        //Очистить редактируемое число.
        public string Clear()
        {
            this.Number = String.Empty;
            return this.Number;
        }
        public string AddSign(int id)
        {
            string sign = GetSingByID(id);
            if (Expression.Length > 0)
                if (isSing(Expression.Last().ToString()) || this.Number.Length == 0)
                    return this.Number;

            return this.Number += sign;
        }
        private bool isSing(string text)
        {
            return (text == "+" || text == "-" || text == "*" || text == "/");
        }
        private string GetSingByID(int id)
        {
            switch (id)
            {
                case 10:
                    return "+";
                case 11:
                    return "-";
                case 12:
                    return "/";
                case 13:
                    return "*";
                case 14:
                    return "=";
                default:
                    return String.Empty;
            }
        }
        private string AddLeftBracket()
        {
            return this.Number += "(";
        }
        private string AddRightBracket()
        {
            return this.Number += ")";
        }
        //Выполнить команду редактирования.
        public string DoEdit(int j)
        {

            if (j >= 0 && j < 10)
                return AddDigit(j);
            else if (j >= 10 && j < 15)
                return AddSign(j);
            else
                switch (j)
                {
                    case 15:
                        return AddDelim();
                    case 16:
                        return Bs();
                    case 17:
                        return Clear();
                    case 18:
                        return AddZero();
                    case 19:
                        return AddPoint();
                    case 20:
                        return AddLeftBracket();
                    case 21:
                        return AddRightBracket();
                    default:
                        throw new Exception("Неизвестный номер команды!");
                }
        }
    }
}
