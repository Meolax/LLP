﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Solvers;
using LLP.Models;

namespace LLP
{
    class SolverLLP
    {
        public Report report;
        private ConstraintsSystemModel constraints;
        private ObjectFunctionModel objectFunction;
        private Decision x1;
        private Decision x2;
        public double x;
        public double y;
        Model modelOfLLP;
         
        public SolverLLP (ConstraintsSystemModel _constraints, ObjectFunctionModel _objectFunction)
        {
            objectFunction = _objectFunction;
            constraints = _constraints;
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
            //modelOfLLP.AddGoal("cost", GoalKind.Maximize, 20 * x1 + 1000 * x2);

            Solution solution = context.Solve(new SimplexDirective());
            report = solution.GetReport();
            x = x1.GetDouble();
            y = x2.ToDouble();
            #endregion
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
            for (int i = 0; i < constraints.rowCount; i++)
            {
                addConstraint(i);
            }
        }

        private void addConstraint (int index)
        {
            if (constraints.sign[index] == "<=")
            modelOfLLP.AddConstraint($"constraint_{index}", constraints.x1[index] * x1 + constraints.x2[index] * x2 <= constraints.c[index]);
            if (constraints.sign[index] == ">=")
                modelOfLLP.AddConstraint($"constraint_{index}", constraints.x1[index] * x1 + constraints.x2[index] * x2 >= constraints.c[index]);
        }
    }
}
