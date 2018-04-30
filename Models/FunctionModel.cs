using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP.Models
{
    class FunctionModel
    {
        public double x1 { get;}
        public double x2 { get;}
        public double c { get;}
        public FunctionModel ( double _x1, double _x2, double _c)
        {
            x1 = _x1;
            x2 = _x2;
            c = _c;
        }
    }
}
