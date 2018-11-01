using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP.Models
{
    public class LLPModel
    {
        #region Constant
        private readonly string nilSolve = "There is no solution!";
        #endregion
        public ConstraintSystemModel Constraints { get; set; }
        public ObjectFunctionModel ObjectFunction { get; set; }
        public bool isSolve { get; set; }

        public Result Solution { get; set; }
        //{
        //    get
        //    {
        //        if (isSolve)
        //            return Solution;
        //        else
        //            throw new Exception(nilSolve);
        //    }

        //    set
        //    {
        //        Solution = value;
        //        isSolve = true;
        //    }

        //}

        public struct Result
        {
            double x1, x2;
            public Result (double x1, double x2)
            {
                this.x1 = x1;
                this.x2 = x2;
            }
        }

        public LLPModel (ConstraintSystemModel constraintSystem, ObjectFunctionModel objectFunction, Result result)
        {
            Constraints = constraintSystem;
            ObjectFunction = objectFunction;
            Solution = result;
        }

    }
}
