using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_influences.TablesData
{
   public class penaliteOpj
    {
        public int id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Depart { get; set; }
        public string Company { get; set; }
        public DateTime DatePenalties { get; set; }
        double valu = 0;
        int va;
        public double value
        {
            get { return valu; }
            set
            {
                if (value < 0)
                {
                    valu = 0;
                    
                }
                else
                {
                    valu = value;
                    va = Convert.ToInt32(valu);
                    valu = va;
                }
            }
        }
        public string Reson { get; set; }
        public string mange { get; set; }
        public string Nots { get; set; }
        public DateTime DateEnter { get; set; }
        public bool caneEdit { get; set; }
        public string NameEnter { get; set; }
        
    }
}
