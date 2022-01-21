using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_influences.TablesData
{
    public class DeductOpj2
    {
        public int idH { get; set; }
        public int id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Depart { get; set; }
        public string Company { get; set; }
        public DateTime DateDeduct { get; set; }
        double valu = 0;
        public double value
        {
            get { return valu; }
            set
            {
                if (value < 0)
                {
                    valu = 0;
                }else
                {
                    valu = value;
                }

            }
        }
        public string deductFrom { get; set; }
        public string Reson { get; set; }
        public string Nots { get; set; }
        public DateTime DateEnter { get; set; }
        public string NameEnter { get; set; }
        public bool caneEdit { get; set; }
    }
}
