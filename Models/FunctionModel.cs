using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP.Models
{
    class FunctionModel
    {
        #region Properties
        public double X1 { get; }
        public double X2 { get; }
        public double C { get; }
        public typeOfFuncton Type { get; private set; }
        string ArgumentError = $"Это не график а фигня!!!";
        #endregion

        public enum typeOfFuncton
        {
            Default = 1,
            Vertical = 2,
            Gorizontal = 3
        }

        public FunctionModel ( double _x1, double _x2, double _c)
        {
            X1 = _x1;
            X2 = _x2;
            C = _c;
            setTypeOfFunction();
        }

        private void setTypeOfFunction ()
        {
            if (X1 == 0 && X2 == 0)
            {
                throw new Exception(ArgumentError);
            } else if (X1 == 0)
            {
                Type = typeOfFuncton.Gorizontal;
            } else if (X2 == 0)
            {
                Type = typeOfFuncton.Vertical;
            } else
            {
                Type = typeOfFuncton.Default;
            }
        }
    }
}
