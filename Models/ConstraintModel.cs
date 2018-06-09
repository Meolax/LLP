using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP.Models
{
    public class ConstraintModel
    {
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double c { get; set; }
        public string sign { get; set; }

        public ConstraintModel (double x1, double x2, double c, string sign )
        {
            this.x1 = x1;
            this.x2 = x2;
            this.c = c;
            this.sign = sign;
        }

        public static implicit operator ConstraintModel (ObjectFunctionModel functionModel)
        {
            return new ConstraintModel (functionModel.x1, functionModel.x2, 0, null) ;
        }

        public static implicit operator ConstraintModel (FunctionModel functionModel)
        {
            return new ConstraintModel(functionModel.X1, functionModel.X2, functionModel.C, null);
        }
    }
}
