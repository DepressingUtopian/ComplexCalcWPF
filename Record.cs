using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP2
{
    public class Record
    {
        private int p1;
        private int p2;
        private string number1;
        private string number2;
        public Record(int p1, int p2, string n1, string n2)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.number1 = n1;
            this.number2 = n2;
        }
        public override string ToString()
        {
            return "Число: " + number1 + " с основанием: " + Convert.ToString(p1)
                + " конвертировано в число: " + Convert.ToString(number2) + " с основанием: " + Convert.ToString(p2);
        }

    }
}
