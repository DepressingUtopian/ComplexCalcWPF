using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculatorWPF
{
    public class Record
    {
        private string expression;
        private string resultExpression;
        private string operation;
        public Record(string expression, string resultExpression)
        {
            this.expression = expression;
            this.resultExpression = resultExpression;
        }
        public Record(string expression,string operation, string resultExpression)
        {
            this.expression = expression;
            this.operation = operation;
            this.resultExpression = resultExpression;
           
        }
        public override string ToString()
        {
            if(operation != null)
                return "Аргумент: " + expression + " операция:"+ operation +" результат: " + resultExpression;
            else
                return "Выражение: " + expression + " результат: " + resultExpression;
        }   

    }
}
