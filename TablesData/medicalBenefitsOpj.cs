using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_influences.TablesData
{
    public class medicalBenefitsOpj : MainClassTD
    {
        double valu = 0;
        public DateTime DateMedicalBenefits { get; set; }
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
                }
            }
        }
        public string Reson { get; set; }
        public string NotsGift { get; set; }
    }
}
