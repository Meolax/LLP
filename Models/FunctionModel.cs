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

        public FunctionModel ( double x1, double x2, double c)
        {
            this.X1 = x1;
            this.X2 = x2;
            this.C = c;
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

        public static FunctionModel getPerpendicular (FunctionModel functionModel)
        {
            return new FunctionModel(-functionModel.X2, functionModel.X1, functionModel.C);           
        }
        public static implicit operator FunctionModel (ConstraintModel constraint)
        {
            return new FunctionModel(constraint.x1, constraint.x2, constraint.c);
        }

        public static implicit operator FunctionModel (ObjectFunctionModel functionModel)
        {
            return new FunctionModel(functionModel.x1, functionModel.x2, 0);
        }
    }
}
