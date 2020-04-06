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
    public class Executor
    {
        //Буфер который запоминает значение последней выполненой последовательности 
        private ComplexNumber lastResult;
        private static String lastExpressionWithoutFirstArg;
        private bool noSaveLastExpression;

        private const int posRealInMatchComplexNumber = 2;
        private const int posImagInMatchComplexNumber = 3;
        private const int posRealInMatchSingleNumber = 5;
        private const int posImagInMatchSingleNumber = 4;
        private const int posSignInMatch = 6;

        public Executor()
        {
            lastResult = new ComplexNumber();
        }
        public ComplexNumber MemoryBuffer { get => lastResult; set => lastResult = value; }
        public string ExecuteExpression(string expression, bool noRepeat = false)
        {
            ComplexNumber number = new ComplexNumber();
            string mixedNumberPattern = @"(\(([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)\s*([-+]\d*\.?\d*[i])\)|([-+]?\s*\d*\.?\d*[i])|([-]?\d+\.?\d*|[-]?\d+\.?\d+))\s*([\-\+\/\*=])?\s*";
            string repeatExecutionPattern = @"(\(([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)\s*([-+]\d*\.?\d*[i])\)|([-+]?\s*\d*\.?\d*[i])|([-]?\d+\.?\d*|[-]?\d+\.?\d+))\s*\=";

           //if (expression.Last().ToString() != "=")
           //         return MemoryBuffer.ToString();
            Regex regexMixedNumber = new Regex(mixedNumberPattern);
            var matchesMixedNumber = regexMixedNumber.Matches(expression);

            double real = 0;
            double imag = 0;

            string realNumberTemp;
            string imagNumberTemp;

            string operation = String.Empty;

            if (matchesMixedNumber.Count == 1 && !noRepeat)
            {
                Regex regexExecutionPattern = new Regex(repeatExecutionPattern);
                var matches = regexExecutionPattern.Matches(expression);

                if (matches.Count > 0)
                {
                    expression = lastResult.ToString() + lastExpressionWithoutFirstArg;
                    matchesMixedNumber = regexMixedNumber.Matches(expression);
                    noSaveLastExpression = true;
                }
                else
                {
                    noSaveLastExpression = false;
                    lastExpressionWithoutFirstArg = String.Empty;
                }

            }
            else
            {
                noSaveLastExpression = false;
                lastExpressionWithoutFirstArg = String.Empty;
            }
            foreach (Match match in matchesMixedNumber)
            {
                realNumberTemp = String.Empty;
                imagNumberTemp = String.Empty;

                if (match.Success)
                {
                    if (match.Groups[posImagInMatchComplexNumber].Value.Length > 0)
                        imagNumberTemp = match.Groups[posImagInMatchComplexNumber].Value;
                    else if(match.Groups[posImagInMatchSingleNumber].Value.Length > 0)
                        imagNumberTemp = match.Groups[posImagInMatchSingleNumber].Value;

                    if (match.Groups[posRealInMatchComplexNumber].Value.Length > 0)
                        realNumberTemp = match.Groups[posRealInMatchComplexNumber].Value;
                    else if (match.Groups[posRealInMatchSingleNumber].Value.Length > 0)
                        realNumberTemp = match.Groups[posRealInMatchSingleNumber].Value;


                    if (imagNumberTemp.Length > 0)
                    {
                        if (imagNumberTemp.ToString() == "i" || imagNumberTemp.ToString() == "+i")
                            imag = 1;
                        else if (imagNumberTemp.ToString() == "-i")
                            imag = -1;
                        else
                            imag = Convert.ToDouble(imagNumberTemp.Substring(0, imagNumberTemp.Length - 1),
                                CultureInfo.InvariantCulture);
                    }
                    if (realNumberTemp.Length > 0 || realNumberTemp.Length > 0)
                    {
                        real = Convert.ToDouble(realNumberTemp,CultureInfo.InvariantCulture);
                    }
                    if (match.Groups[posSignInMatch].Value.Length > 0)
                    {
                        if (operation != String.Empty)
                        {
                            ExecuteOperation(number, operation, real, imag);

                            if (!noSaveLastExpression)
                            {
                                lastExpressionWithoutFirstArg += operation;
                                lastExpressionWithoutFirstArg += match.Value;
                            }
                            operation = match.Groups[posSignInMatch].Value;

                        }
                        else
                        {

                            number.SetReal(real);
                            number.SetImag(imag);

                        }

                        operation = match.Groups[posSignInMatch].Value;
                        real = 0;
                        imag = 0;
                    }
                }
            }
            if (operation != String.Empty)
                ExecuteOperation(number, operation, number.Number.Real, number.Number.Imaginary);
            //else if (IsSign(expression[0].ToString()) && expression.Last().ToString() == "=")
            //   ExecuteOperation(memoryBuffer, expression[0].ToString(), memoryBuffer.Number.Real, memoryBuffer.Number.Imaginary);
            return number.ToString().Replace(",", ".");
        }
        public String ExecuteCommand(string id, int value = 0)
        {
            switch (id)
            {
                case "RAD":
                    return MemoryBuffer.GetRadians();
                case "GRAD":
                    return MemoryBuffer.GetDegrees();
                case "ROOT":
                    return MemoryBuffer.Root(value);
                case "MDL":
                    return MemoryBuffer.Abs();
                case "POW":
                    return MemoryBuffer.Power(value);
                case "IMAG":
                    return MemoryBuffer.Number.Imaginary.ToString();
                case "REAL":
                    return MemoryBuffer.Number.Real.ToString();
                case "DEFAULT":
                    return MemoryBuffer.ToString();
                default:
                    throw new ArgumentException("Неизвестный операция над числом");
            }
        }
        //public String ConvertNumberToForm(string expression)
        //{
        //    string mixedNumberPattern = @"(\(([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)\s*([-+]\d*\.?\d*[i])\)|([-+]?\s*\d*\.?\d*[i])|([-]?\d+\.?\d*|[-]?\d+\.?\d+))\s*([\-\+\/\*=])?\s*";
        //    Regex regexMixedNumber = new Regex(mixedNumberPattern);
        //    var matchesMixedNumber = regexMixedNumber.Matches(expression);
        //    ComplexNumber number = new ComplexNumber();

        //    double real = 0;
        //    double imag = 0;

        //    string realNumberTemp;
        //    string imagNumberTemp;

        //    string resultConvertation;

        //    string operation = String.Empty;

        //    foreach (Match match in matchesMixedNumber)
        //    {
        //        realNumberTemp = String.Empty;
        //        imagNumberTemp = String.Empty;

        //        if (match.Success)
        //        {
        //            if (match.Groups[posImagInMatchComplexNumber].Value.Length > 0)
        //                imagNumberTemp = match.Groups[posImagInMatchComplexNumber].Value;
        //            else if (match.Groups[posImagInMatchSingleNumber].Value.Length > 0)
        //                imagNumberTemp = match.Groups[posImagInMatchSingleNumber].Value;

        //            if (match.Groups[posRealInMatchComplexNumber].Value.Length > 0)
        //                realNumberTemp = match.Groups[posRealInMatchComplexNumber].Value;
        //            else if (match.Groups[posRealInMatchSingleNumber].Value.Length > 0)
        //                realNumberTemp = match.Groups[posRealInMatchSingleNumber].Value;


        //            if (imagNumberTemp.Length > 0)
        //            {
        //                if (imagNumberTemp.ToString() == "i" || imagNumberTemp.ToString() == "+i")
        //                    imag = 1;
        //                else if (imagNumberTemp.ToString() == "-i")
        //                    imag = -1;
        //                else
        //                    imag = Convert.ToDouble(imagNumberTemp.Substring(0, imagNumberTemp.Length - 1),
        //                        CultureInfo.InvariantCulture);
        //            }
        //            if (realNumberTemp.Length > 0 || realNumberTemp.Length > 0)
        //            {
        //                real = Convert.ToDouble(realNumberTemp, CultureInfo.InvariantCulture);
        //            }
        //            if (match.Groups[posSignInMatch].Value.Length > 0)
        //            {
        //                if (operation != String.Empty)
        //                {
                            

        //                    if (!noSaveLastExpression)
        //                    {
        //                        lastExpressionWithoutFirstArg += operation;
        //                        lastExpressionWithoutFirstArg += match.Value;
        //                    }
        //                    operation = match.Groups[posSignInMatch].Value;

        //                }
        //                else
        //                {

        //                    number.SetReal(real);
        //                    number.SetImag(imag);

        //                }

        //                operation = match.Groups[posSignInMatch].Value;
        //                real = 0;
        //                imag = 0;
        //            }
        //        }
        //    }
        //}
        private void ExecuteOperation(ComplexNumber number, string operation, double real, double imag)
        {
            switch (operation)
            {

                case "+":
                    number.Plus(new ComplexNumber(real, imag));
                    break;
                case "-":
                    number.Minus(new ComplexNumber(real, imag));
                    break;
                case "/":
                    number.Divide(new ComplexNumber(real, imag));
                    break;
                case "*":
                    number.Multiply(new ComplexNumber(real, imag));
                    break;
                case "=":
                    MemoryBuffer = number;
                    break;
                default:
                    throw new ArgumentException("Неизвестный знак");
            }

        }
        private bool IsSign(string sign)
        {
            if (sign == "*" || sign == "/" || sign == "+" || sign == "-" || sign == "=")
                return true;
            else
                return false;
        }
        public void Clear()
        {
            noSaveLastExpression = false;
            lastExpressionWithoutFirstArg = String.Empty;
            lastResult.Clear();
        }
    }
}
