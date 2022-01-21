using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Worker_influences.DataObjects
{
    public class MainDataObj
    {

        public int id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Depart { get; set; }
        public string Company { get; set; }
        public bool NoWork { get; set; }
        public DateTime DateCome { get; set; }
        public string NameJop { get; set; }
        public string DegreeJop { get; set; }
        public double Incentive { get; set; }
        public double Insurance { get; set; }
        public double Salary { get; set; }
        public string Nots { get; set; }
        public string NameEnter { get; set; }

    }
}
