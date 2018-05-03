using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Solvers;
using LLP.Models;
using System.Windows.Forms;

namespace LLP
{
    class SolverLLP
    {
        private Solution solution;
        private ConstraintSystemModel ConstraintSystem;
        private ObjectFunctionModel objectFunction;
        private Decision x1;
        private Decision x2;
        Model modelOfLLP;
         
        public SolverLLP (ConstraintSystemModel _constraints, ObjectFunctionModel _objectFunction)
        {
            objectFunction = _objectFunction;
            ConstraintSystem = _constraints;
            solveLLP();
        }

        private void solveLLP ()
        {
            #region 1 реализация
            SolverContext context = SolverContext.GetContext();
            context.ClearModel();
            modelOfLLP = context.CreateModel();

            x1 = new Decision(Domain.IntegerNonnegative, "X1");
            x2 = new Decision(Domain.IntegerNonnegative, "X2");

            modelOfLLP.AddDecisions(x1, x2);

            modelOfLLP.AddConstraints("limits", 0 <= x1, 0 <= x2);
            addConstraintsSystem();
            addObjectFunction();
            
            solution = context.Solve(new SimplexDirective());
            #endregion
        }

        public string getResult ()
        {
            string fx = objectFunction.minimize ? "F(X)->min = " : "F(X)->min = ";
            switch (solution.Quality)
            {
                case Microsoft.SolverFoundation.Services.SolverQuality.Optimal:
                    return $"X1: {solution.Decisions.ElementAt(0).ToString()};\n" +
                           $"X2: {solution.Decisions.ElementAt(1).ToString()};\n" +
                           $"{fx}{solution.Goals.ElementAt(0).ToString()};";
                case Microsoft.SolverFoundation.Services.SolverQuality.Unbounded:
                    return  "There is no optimal solution";
            }
            return "Error!!!";
        }
        private void addObjectFunction ()
        {
            if (objectFunction.minimize)
            {
                modelOfLLP.AddGoal("cost", GoalKind.Minimize, objectFunction.x1 * x1 + objectFunction.x2 * x2);
            } else
            {
                modelOfLLP.AddGoal("cost", GoalKind.Maximize, objectFunction.x1 * x1 + objectFunction.x2 * x2);
            }
        }

        private void addConstraintsSystem ()
        {
            for (int i = 0; i < ConstraintSystem.rowCount; i++)
            {
                addConstraint(i, ConstraintSystem.Constraints[i]);
            }
        }

        private void addConstraint (int index, ConstraintModel constraint)
        {
            if (constraint.sign == "<=")
            modelOfLLP.AddConstraint($"constraint_{index}", constraint.x1 * x1 + constraint.x2 * x2 <= constraint.c);
            if (constraint.sign == ">=")
                modelOfLLP.AddConstraint($"constraint_{index}", constraint.x1 * x1 + constraint.x2 * x2 >= constraint.c);
        }
    }
}
