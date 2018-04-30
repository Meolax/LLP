using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LLP.Models;

namespace LLP.Controlers
{
    class Graphices
    {
        #region Properties
        private Chart chartGraphic;
        private ConstraintSystemModel constraintSystem;
        #endregion

        public Graphices (ref Chart chart, ConstraintSystemModel _constraintsSystem)
        {
            chartGraphic = chart;
            constraintSystem = _constraintsSystem;
        }

        public void Draw ()
        {
            chartGraphic.Series.Clear();
            for (int i=0; i<constraintSystem.rowCount; i++)
            {
                createFunc(1, new ConstraintModel { x1 = constraintSystem.x1[i], x2 = constraintSystem.x2[i], c = constraintSystem.c[i] });
            }
        }

        private bool isThePointIncludedIntheConstraint (double x, double y, ConstraintModel constraint)
        {
            if (constraint.sign == ">=")
            {
                if (x*constraint.x1 + y*constraint.x2 >= constraint.c)
                    return true;
            } else if (constraint.sign == "<=")
            {
                if (x * constraint.x1 + y * constraint.x2 <= constraint.c)
                    return true;
            }
            return false;
        }        

        private int createNewSeries ()
        {
            var index = chartGraphic.Series.Count;
            chartGraphic.Series.Add(index.ToString());
            chartGraphic.Series[index].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            return index;
        }

        private void addDotWithStrich (int index, double x, params double[] num)
        {
            chartGraphic.Series[index].Points.AddXY(x, (num[2] - num[1] * x) / num[0]);
            chartGraphic.Series[index].Points.AddXY(x - 1, ((num[2]) - num[1] * x - 1) / (num[0]) - 1);
            chartGraphic.Series[index].Points.AddXY(x, (num[2] - num[1] * x) / num[0]);
        }

        //Придумать как добавить штрих красиво
        private void createFunc (double shag, double c1, double c2, double c)
        {
            var index = createNewSeries();
            for (double i = -10; i <= 30; i += shag)
            {
                var x = Math.Round(i, 2);
                if (x % 0.5 == 0)
                {
                    addDotWithStrich(index, x, c1, c2, c);
                }
                else
                {
                    chartGraphic.Series[index].Points.AddXY(x, (c - c1 * x) / c2);
                }
            }
        }

        private void createFunc (double shag, ConstraintModel constraint)
        {
            if (constraint.x1 == 0 && constraint.x2 == 0)
            {
                throw new Exception("Argument error!");
            }
            else if (constraint.x1 == 0)
            {
                var index = createNewSeries();
                for (double i = -10; i <= 30; i += shag)
                {
                    var x = Math.Round(i, 2);
                    chartGraphic.Series[index].Points.AddXY(x, (constraint.c) / constraint.x2);
                }
            }
            else if (constraint.x2 == 0)
            {
                var index = createNewSeries();
                for (double i = 0; i <= 30; i += shag)
                {
                    var x = Math.Round(i, 2);
                    chartGraphic.Series[index].Points.AddXY((constraint.c) / constraint.x1, x);
                }
            }
            else
            {
                var index = createNewSeries();
                for (double i = -10; i <= 30; i += shag)
                {
                    var x = Math.Round(i, 2);
                    chartGraphic.Series[index].Points.AddXY(x, (constraint.c - constraint.x1 * x) / constraint.x2);
                }
            }
        }

        private bool canIFindCrossPoint (FunctionModel function1, FunctionModel function2)
        {
            var a1 = -function1.x2 / function1.x1;
            var c1 = function1.c / function1.x1;
            var a2 = -function2.x2 / function2.x1;
            var c2 = function2.c / function2.x1;
            if (a1 - a2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //private void findAllCrossPoints (ConstraintsSystemModel constraintsSystem)
        //{
        //    for (int i = 0; i < constraintsSystem.rowCount - 1; i++)
        //        for (int j = i + 1; j < constraintsSystem.rowCount; j++)
        //        {
        //            if (canIFindCrossPoint(new FunctionModel(x1[i], x2[i], c[i]), new FunctionModel(x1[j], x2[j], c[j])))
        //            {
        //                findCrossPoint(new FunctionModel(x1[i], x2[i], c[i]), new FunctionModel(x1[j], x2[j], c[j]));
        //            }
        //        }
        //}

        //private void findCrossPoint (FunctionModel function1, FunctionModel function2)
        //{
        //    var a1 = -function1.x2 / function1.x1;
        //    var c1 = function1.c / function1.x1;
        //    var a2 = -function2.x2 / function2.x1;
        //    var c2 = function2.c / function2.x1;
        //    x1Point.Add((c2 - c1) / (a1 - a2));

        //    a1 = -function1.x1 / function1.x2;
        //    c1 = function1.c / function1.x2;
        //    a2 = -function2.x1 / function2.x2;
        //    c2 = function2.c / function2.x2;
        //    x2Point.Add((c2 - c1) / (a1 - a2));
        //}
        
    }
}
