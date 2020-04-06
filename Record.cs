using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP2
{
    public class Record
    {
        private string expression;
        private string resultExpression;
        public Record(string expression, string resultExpression)
        {
            this.expression = expression;
            this.resultExpression = resultExpression;
        }
        public override string ToString()
        {
            return "Выражение: " + expression + " результат: " + Convert.ToString(resultExpression);
        }

    }
}
