using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Worker_influences.TablesData
{
    public class MainClassTD
    {
       
        public int id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Depart { get; set; }
        public string Company { get; set; }
        public DateTime DateEnter { get; set; }
        public bool caneEdit { get; set; }
        public string NameEnter { get; set; }

    }
}
