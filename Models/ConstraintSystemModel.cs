using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP.Models
{
    public class ConstraintSystemModel
    {
        public int rowCount { get; }
        public ConstraintModel [] Constraints { get;}
        public ConstraintSystemModel (double[] x1, double[] x2, double[] c, string[] sign)
        {
            rowCount = x1.Length;
            Constraints = new ConstraintModel[x1.Length];
            setConstraints(x1, x2, c, sign);
        }

        public void setConstraints (double[] x1, double[] x2, double[] c, string[] sign)
        {
            for (int i = 0; i < x1.Length; i++)
            {
                Constraints[i] = new ConstraintModel(x1[i], x2[i], c[i], sign[i]);
            }
        }
        //public double []  x1 { get; set;}
        //public double [] x2 { get; set; }
        //public double [] c { get; set; }
        //public string [] sign { get; set; }

        
    }
}
