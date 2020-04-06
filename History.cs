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
        public void AddRecord(int p1, int p2, string n1, string n2)
        {
            this.ListRecords.Add(new Record(p1, p2, n1, n2));
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
