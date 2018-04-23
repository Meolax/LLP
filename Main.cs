using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;


namespace LLP
{
    public partial class Main : Form
    {
        public Random random = new Random();
        public Main ()
        {
            InitializeComponent();
        }
       
        private double returnValueFromTextBox (TextBox textBox)
        {
            try
            {
                return Convert.ToDouble(textBox.Text);
            }
            catch
            {
                double result = Math.Round(random.NextDouble() * 10,2);
                textBox.Text = result.ToString();
                return result;
            }
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

        private void createGraphic (double c1, double c2, double c3)
        {
            var index = createNewSeries();
            chartGraphic.Series[index].Points.AddXY(c3 / c1, 0);
            chartGraphic.Series[index].Points.AddXY(0, c3 / c2);
        }

        private void button1_Click (object sender, EventArgs e)
        {

            //createFunc(0.1, c1, c2, c3);
            // createGraphic(c1, c2, c3);
            systemOfConstraintsDataGridView.RowCount = 0;
        }

        private void button2_Click (object sender, EventArgs e)
        {
          
        }

        private void createTable (int kolvoStrok)
        {
            
            string[] row = new string [] { };
            for (int i = 1; i <=kolvoStrok; i++)
            {
                row = new string[] { i.ToString(), "","" };
                systemOfConstraintsDataGridView.Rows.Add(row);
                systemOfConstraintsDataGridView.Rows[i - 1].Cells[3].Value = "<=" ;
            }

        }

        private void solveLLP ()
        {
            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();
            Decision vz = new Decision(Domain.RealNonnegative, "barrels_venezuela");
            Decision sa = new Decision(Domain.RealNonnegative, "barrels_saudiarabia");

            model.AddDecisions(vz, sa);
            model.AddConstraints("limits",
                0 <= vz,
                0 <= sa);

            model.AddConstraints("production",
                0.3 * sa + 0.4 * vz <= 2000,
                0.4 * sa + 0.2 * vz <= 1500,
                0.2 * sa + 0.3 * vz <= 500,
                1 * sa <= 6000,
                1 * vz <= 9000);
            model.AddGoal("cost", GoalKind.Maximize,
                                  20 * sa + 1000 * vz);


            Solution solution = context.Solve(new SimplexDirective());

            Report report = solution.GetReport();
            richTextBox1.Text+= $"{vz} {sa}\n";
            richTextBox1.Text += ("{0}", report);
          
        }
        private void button3_Click (object sender, EventArgs e)
        {
            createTable(5);
            
           

        }

        private void button4_Click (object sender, EventArgs e)
        {
            systemOfConstraintsDataGridView.RowCount = 3;
        }
    }
}
