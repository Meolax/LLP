﻿using System;
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
        #region Constant
        private const string errorOFArgument = "Algorithm findCrossPoint";
        #endregion
        #region Properties
        private Chart chartGraphic;
        private LLPModel LLP;
        private List<double> x1Point = new List<double>();
        private List<double> x2Point = new List<double>();
        private delegate void creatingGraphic (double shag, ConstraintModel constraint);
        private double a1, a2, c1, c2, xMax, xMin, yMax, yMin;
        #endregion

        public Graphices (ref Chart chart, LLPModel LLP)
        {
            chartGraphic = chart;
            this.LLP = LLP;
        }

        public void Draw ()
        {
            chartGraphic.Series.Clear();
            for (int i=0; i<LLP.Constraints.rowCount; i++)
            {
                addGraphic(1, LLP.Constraints.Constraints[i]);
            }
            createGraphicOfObjectFunction(LLP.ObjectFunction);
            setBounds();
        }

        private bool isThePointIncludedIntheConstraint (double x, double y, ConstraintModel constraint)
        {
            if (constraint.sign == ">=" && x * constraint.x1 + y * constraint.x2 >= constraint.c)
            {
                return true;
            } else if (constraint.sign == "<=" && x * constraint.x1 + y * constraint.x2 <= constraint.c)
            {
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

        private int createNewSeries (bool castomName = false, string nameOfSeries = "Default")
        {
            var index = chartGraphic.Series.Count;
            chartGraphic.Series.Add(castomName ? nameOfSeries +" "+index.ToString(): index.ToString());
            chartGraphic.Series[index].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            return index;
        }

        //private void addDotWithStrich (int index, double x, params double[] num)
        //{
        //    chartGraphic.Series[index].Points.AddXY(x, (num[2] - num[1] * x) / num[0]);
        //    chartGraphic.Series[index].Points.AddXY(x - 1, ((num[2]) - num[1] * x - 1) / (num[0]) - 1);
        //    chartGraphic.Series[index].Points.AddXY(x, (num[2] - num[1] * x) / num[0]);
        //}

        //Придумать как добавить штрих красиво
        //private void addGraphic (double shag, double c1, double c2, double C)
        //{
        //    var index = createNewSeries();
        //    for (double i = -10; i <= 30; i += shag)
        //    {
        //        var x = Math.Round(i, 2);
        //        if (x % 0.5 == 0)
        //        {
        //            addDotWithStrich(index, x, c1, c2, C);
        //        }
        //        else
        //        {
        //            chartGraphic.Series[index].Points.AddXY(x, (C - c1 * x) / c2);
        //        }
        //    }
        //}

        #region Creating graphices

        private void createGraphicOfObjectFunction (ObjectFunctionModel objectFunction)
        {
            addGraphic(1, objectFunction, true, "F(x)");

            addGraphic(1, FunctionModel.getPerpendicular(objectFunction), true, "Cost");
        }

        private void addGraphic (double shag, ConstraintModel constraint, bool castomName=false, string nameOfSeries = "Default")
        {
            if (constraint.x1 == 0 && constraint.x2 == 0)
            {
                throw new Exception("Argument error!");
            }
            else if (constraint.x1 == 0)
            {
                createGorizontalGraphic(shag, constraint, castomName, nameOfSeries) ;
            }
            else if (constraint.x2 == 0)
            {
                createVerticalGraphic(shag, constraint, castomName, nameOfSeries);
            }
            else
            {
                createDefaultGraphic(shag, constraint, castomName, nameOfSeries);
            }
        }

        private void createGorizontalGraphic (double shag, ConstraintModel constraint, bool castomName = false, string nameOfSeries = "Default")
        {
            var index = createNewSeries(castomName, nameOfSeries);
            for (double i = -10; i <= 50; i += shag)
            {
                var x = Math.Round(i, 2);
                chartGraphic.Series[index].Points.AddXY(x, (constraint.c) / constraint.x2);
            }
        }

        private void createVerticalGraphic (double shag, ConstraintModel constraint, bool castomName = false, string nameOfSeries = "Default")
        {
            var index = createNewSeries(castomName, nameOfSeries);
            for (double i = 0; i <= 50; i += shag)
            {
                var x = Math.Round(i, 2);
                chartGraphic.Series[index].Points.AddXY((constraint.c) / constraint.x1, x);
            }
        }

        private void createDefaultGraphic (double shag, ConstraintModel constraint, bool castomName = false, string nameOfSeries = "Default")
        {
            var index = createNewSeries(castomName, nameOfSeries);
            for (double i = -10; i <= 50; i += shag)
            {
                var x = Math.Round(i, 2);
                chartGraphic.Series[index].Points.AddXY(x, (constraint.c - constraint.x1 * x) / constraint.x2);
            }
        }
        #endregion

        #region All with cross point

        private void findAllCrossPoints (ConstraintSystemModel constraintsSystem)
        {
            for (int i = 0; i < constraintsSystem.rowCount - 1; i++)
                for (int j = i + 1; j < constraintsSystem.rowCount; j++)
                {
                    if (canIFindCrossPoint(constraintsSystem.Constraints[i], constraintsSystem.Constraints[j]))
                    {
                        findCrossPoint(constraintsSystem.Constraints[i], constraintsSystem.Constraints[j]);
                    }
                }
        }

        private bool canIFindCrossPoint (FunctionModel function1, FunctionModel function2)
        {
            if (function1.Type == FunctionModel.typeOfFuncton.Default && function1.Type == FunctionModel.typeOfFuncton.Default)
            {
                findFunctionCoefficientsX1(function1, function2);
                return concurrencyCheck(a1, a2);
            } else if (function1.Type != function2.Type)
            {
                return true;
            }
            return false;
        }

        private bool concurrencyCheck (double a1, double a2)
        {
            if (a1 - a2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void findFunctionCoefficientsX1 (FunctionModel function1, FunctionModel function2)
        {
            a1 = -function1.X2 / function1.X1;
            c1 = function1.C / function1.X1;
            a2 = -function2.X2 / function2.X1;
            c2 = function2.C / function2.X1;
        }

        private void findFunctionCoefficientsX2 (FunctionModel function1, FunctionModel function2)
        {
            a1 = -function1.X1 / function1.X2;
            c1 = function1.C / function1.X2;
            a2 = -function2.X1 / function2.X2;
            c2 = function2.C / function2.X2;
        }

        private void getBounds ()
        {
            try
            {
                xMax = x1Point.Max() + 5;
                xMin = (x1Point.Min() < -10 ? x1Point.Min() : -10) - 2;
                yMax = x2Point.Max() + 5;
                yMin = (x2Point.Min() < -10 ? x2Point.Min() : -10) - 2;
                yMin = 0;
            }
            catch
            {
                xMax = 15;
                xMin = -5;
                yMax = 15;
                yMin = -5;
            }
        }

        private void setBounds ()
        {
            findAllCrossPoints(LLP.Constraints);
            getBounds();
            chartGraphic.ChartAreas.ElementAt(0).AxisX.Maximum = xMax;
            chartGraphic.ChartAreas.ElementAt(0).AxisX.Minimum = xMin;
            chartGraphic.ChartAreas.ElementAt(0).AxisY.Maximum = yMax;
            chartGraphic.ChartAreas.ElementAt(0).AxisY.Minimum = yMin;
        }

        private void findCrossPoint (FunctionModel function1, FunctionModel function2)
        {
            if (function1.Type == function2.Type)
            {
                if (function1.Type != FunctionModel.typeOfFuncton.Default) throw new Exception();
                findCrossPointInDefaultGraphices(function1, function2);
            } 

            if (function1.Type == FunctionModel.typeOfFuncton.Default)
            {
                if (function2.Type == FunctionModel.typeOfFuncton.Vertical) findCrossPointInVerticalAndDefaultGraphies(function1, function2);
                if (function2.Type == FunctionModel.typeOfFuncton.Gorizontal) findCrossPointINGorizontalAndDefaultGraphies(function1, function2);
            }

            
            if (function1.Type == FunctionModel.typeOfFuncton.Vertical)
            {
                if (function2.Type == FunctionModel.typeOfFuncton.Default) findCrossPointInVerticalAndDefaultGraphies(function2, function1);
                if (function2.Type == FunctionModel.typeOfFuncton.Gorizontal) findCrossPointInVerticalAndGorizontalGraphies(function1, function2);
            }

            if (function2.Type == FunctionModel.typeOfFuncton.Gorizontal)
            {
                if (function2.Type == FunctionModel.typeOfFuncton.Vertical) findCrossPointInVerticalAndGorizontalGraphies(function2, function1);
                if (function2.Type == FunctionModel.typeOfFuncton.Default) findCrossPointINGorizontalAndDefaultGraphies(function2, function1);
            }
        }

        private void findCrossPointInVerticalAndGorizontalGraphies (FunctionModel verticalFunction, FunctionModel gorizontalFunction)
        {
            x1Point.Add(verticalFunction.C / verticalFunction.X1);
            x2Point.Add(gorizontalFunction.C/gorizontalFunction.X2);
        }

        private void findCrossPointINGorizontalAndDefaultGraphies (FunctionModel defaultFunction, FunctionModel gorizontalFunction)
        {   //x1(x)=0
            findFunctionCoefficientsX1(defaultFunction, gorizontalFunction);
            x1Point.Add((c2-c1)/a1);
            x2Point.Add(c2);
        }

        private void findCrossPointInVerticalAndDefaultGraphies (FunctionModel defaultFunction, FunctionModel verticalFunction)
        {
            //x2(y) = 0
            findFunctionCoefficientsX1(defaultFunction, verticalFunction);
            double x = verticalFunction.C / verticalFunction.X1;
            x1Point.Add(x);
            x2Point.Add((a1 * x)  + c1);
        }

        private void findCrossPointInDefaultGraphices (FunctionModel defaultFunction1, FunctionModel defaultFunction2)
        {
            findFunctionCoefficientsX1(defaultFunction1, defaultFunction2);
            x1Point.Add((c2 - c1) / (a1 - a2));
            findFunctionCoefficientsX2(defaultFunction1, defaultFunction2);
            x2Point.Add((c2 - c1) / (a1 - a2));
        }
        #endregion

        private void drawResult ()
        {

        }
    }
}
