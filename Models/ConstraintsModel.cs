using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP.Models
{
    class ConstraintsModel
    {
        public int rowCount { get; set; }
        public double [] x1 { get; set;}
        public double[] x2 { get; set; }
        public double[] c { get; set; }
        public string[] sign { get; set; }
    }
}
