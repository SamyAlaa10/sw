using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_influences.CalculatedSalary
{
    public class calculateDataSalary
    {
        public double slary = 0;
        public int id { get; set; }
        public int id_E { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string depart { get; set; }
        public string company { get; set; }
        public int dateMonth { get; set; }
        public int dateYear { get; set; }
        public double sumBenefits { get; set; }
        public double sumDeductions { get; set; }
        public double ruselt { get; set; }
    }
}
