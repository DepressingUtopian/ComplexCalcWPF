using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP2
{
    class History
    {
        private List<Record> ListRecords;
        public Record this[int i]
        {
            get
            {
                return ListRecords[i];
            }
            set
            {
                ListRecords[i] = value;
            }
        }
        public void AddRecord(string expression, string resultExpression)
        {
            this.ListRecords.Add(new Record(expression, resultExpression));
        }
        public void Clear() { this.ListRecords.Clear(); }
        public int Count() { return this.ListRecords.Count; }
        public History() { this.ListRecords = new List<Record>(); }
        public List<String> GetHistoryLog()
        {
            List<String> result = new List<string>();
            foreach (var elem in ListRecords)
                result.Add(elem.ToString());
            return result;
        }

    }
}
