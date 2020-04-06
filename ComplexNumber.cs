using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComplexCalculatorWPF
{
    public class ComplexNumber
    {
        private Complex number;

        public Complex Number { get => number; set => number = value; }

        public ComplexNumber()
        {
            Number = Complex.Zero;
        }
        public ComplexNumber(double real, double img)
        {
            this.Number = new Complex(real, img);
        }
        public ComplexNumber(string value)
        {

            string regexPattern =
                // Сопоставить с любым числом с плавающей точкой, отрицательным или положительным, сгруппировать
                @"([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)" +
                // ... возможно после этого с пробелами
                @"\s*" +
                // ... с последующим плюсом
                @"\+" +
                // и возможно больше пробелов:
                @"\s*" +
                // Подходим любой другой тип с плавающей точкой и сохраняем его
                @"([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)" +
                // ... заканчивается 'i'
                @"i";
            Regex regex = new Regex(regexPattern);

            Match match = regex.Match(value);

            double real = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
            double img = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
            Number = new Complex(real, img);

        }
        public void SetReal(double value)
        {
            this.Number = new Complex(value, this.Number.Imaginary);
        }
        public void SetImag(double value)
        {
            this.Number = new Complex(this.Number.Real, value);
        }
        public double GetReal()
        {
            return this.Number.Real;
        }
        public double GetImag()
        {
            return this.Number.Imaginary;
        }
        public string Plus(ComplexNumber arg)
        {
            this.Number += arg.Number;
            return this.ToString();
        }
        public string Minus(ComplexNumber arg)
        {
            this.Number -= arg.Number;
            return this.ToString();
        }
        public string Divide(ComplexNumber arg)
        {
            this.Number /= arg.Number;
            return this.ToString();
        }
        public string Multiply(ComplexNumber arg)
        {
            this.Number *= arg.Number;
            return this.ToString();
        }
        public string Power(int powerValue)
        {
            this.Number = Complex.Pow(this.Number, powerValue);
            return this.ToString();
        }
        public string Root(int rootValue)
        {
            //var Z = this.Number.Magnitude;
            //var Fi = this.Number.Phase * 180 / Math.PI;
            //for (int i = 0; i < rootValue; i++)
            //{
            //    this.Number = Math.S
            //}
            if (rootValue > 0)
                this.Number = Complex.FromPolarCoordinates((double)Math.Pow(this.Number.Magnitude, (double)1 / (double)rootValue), this.Number.Phase / rootValue);
            return this.ToString();
        }
        public string GetRadians()
        {
            return this.ToString();
        }
        public string GetDegrees()
        {
            string result = String.Empty;
            if (this.Number.Real != 0)
                result += RadianValueToDegreesValue(this.Number.Real).ToString();
            if (this.Number.Imaginary > 0)
                result += "+" + RadianValueToDegreesValue(this.Number.Imaginary).ToString() + "i";
            else if (this.Number.Imaginary < 0)
                result += RadianValueToDegreesValue(this.Number.Imaginary).ToString() + "i";
            else
                if (this.Number.Imaginary == 0 && this.Number.Real == 0)
                result = "0";
            return result;
        }
        private double RadianValueToDegreesValue(double value)
        {
            return value * 180 / Math.PI;
        }
        public string Abs()
        {
            this.Number = Complex.Abs(this.Number);
            return this.Number.ToString();
        }
        public void Clear()
        {
            Number = Complex.Zero;
        }
        public override string ToString()
        {
            string result = String.Empty;
            if (this.Number.Real != 0)
                result += this.Number.Real.ToString().Replace(",", ".");
            if (this.Number.Imaginary > 0)
            {
                if (this.Number.Real != 0)
                    result += "+";
                result += this.Number.Imaginary.ToString().Replace(",", ".") + "i";
            }
            else if (this.Number.Imaginary < 0)
                result += this.Number.Imaginary.ToString().Replace(",", ".") + "i";
            else
            if (this.Number.Imaginary == 0 && this.Number.Real == 0)
                result = "0";
            if (this.Number.Imaginary != 0 && this.Number.Real != 0)
                result = "(" + result + ")";

            return result;
        }
    }

}
