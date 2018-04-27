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
using LLP.Models;



namespace LLP
{
    public partial class Main : Form
    {
        #region Property
        public Random random = new Random();
        double[] x1 = new double[] { };
        double[] x2 = new double[] { };
        string[] sign = new string[] { };
        double[] c = new double[] { };
        #endregion

        #region Method of Main form
        public Main ()
        {
            InitializeComponent();
        }

        private void button1_Click (object sender, EventArgs e)
        {
            readConstraintSystemFromDataGrid();
            SolverLLP llp = new SolverLLP(createModelSystemOfConstraint(),getObjectFunctionFromTable());
            richTextBox1.Text = llp.report.ToString();
            richTextBox1.Text += $"/n {llp.x}  ===  {llp.y}";
        }

        private void Main_Load (object sender, EventArgs e)
        {
            objectFunctionDataGridView.RowCount = 1;
            createConstraintSystemTable(1);
        }

        private void textBox1_KeyPress (object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }

            //Управляющие клавиши <Backspace>, <Enter> и т.д.
            if (Char.IsControl(e.KeyChar))
            {
                return;
            }

            //Остальное запрещено
            e.Handled = true;
        }

        private void textBox1_KeyUp (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                createConstraintSystemTable(Convert.ToInt32(returnValueFromTextBox(sender as TextBox) < 1 ? 1: returnValueFromTextBox(sender as TextBox)));
            }
        }
        #endregion

        #region Methods for working with graph of function
        private double returnValueFromString (string str)
        {
            try
            {
                str.Replace(".", ",");
                return Convert.ToDouble(str);
            }
            catch
            {
                MessageBox.Show("Error convert!!!");
                return 0;
            }
        }

        private double returnValueFromTextBox (TextBox textBox)
        {
            double result = returnValueFromString(textBox.Text);
            textBox.Text = result.ToString();
            return result;
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
        #endregion

        #region Methods for working with constraints system
        private void clearVectorsOfConstraintsSystem()
        {
            x1 = new double[] { };
            x2 = new double[] { };
            sign = new string[] { };
            c = new double[] { };
        }

        private void initializeVectorsOfConstraintsSystem()
        {
            Array.Resize(ref x1, systemOfConstraintsDataGridView.RowCount - 1);
            Array.Resize(ref x2, systemOfConstraintsDataGridView.RowCount - 1);
            Array.Resize(ref sign, systemOfConstraintsDataGridView.RowCount - 1);
            Array.Resize(ref c, systemOfConstraintsDataGridView.RowCount - 1);
        }

        private void readConstraintSystemFromDataGrid ()
        {
            clearVectorsOfConstraintsSystem();
            initializeVectorsOfConstraintsSystem();
            for (int i=0; i<systemOfConstraintsDataGridView.RowCount-1; i++)
            {
                x1[i] = returnValueFromString(systemOfConstraintsDataGridView.Rows[i].Cells[1].Value.ToString());
                x2[i] = returnValueFromString(systemOfConstraintsDataGridView.Rows[i].Cells[2].Value.ToString());
                sign[i] = systemOfConstraintsDataGridView.Rows[i].Cells[3].Value.ToString();
                c[i] = returnValueFromString(systemOfConstraintsDataGridView.Rows[i].Cells[4].Value.ToString());
            }
        }

        private ConstraintsSystemModel createModelSystemOfConstraint ()
        {
            ConstraintsSystemModel constraints = new ConstraintsSystemModel();
            constraints.x1 = x1;
            constraints.x2 = x2;
            constraints.sign = sign;
            constraints.c = c;
            constraints.rowCount = systemOfConstraintsDataGridView.RowCount - 1;
            return constraints;
        }

        private void createConstraintSystemTable (int kolvoStrok)
        {
            systemOfConstraintsDataGridView.RowCount = 0;
            string[] row = new string [] { };
            for (int i = 1; i <=kolvoStrok; i++)
            {
                row = new string[] { i.ToString(), "","","<=" };
                systemOfConstraintsDataGridView.Rows.Add(row);
            }
            row = new string[] {"", "", "x1, x2", ">=", "0" };
            systemOfConstraintsDataGridView.Rows.Add(row);
            systemOfConstraintsDataGridView.Rows[kolvoStrok].ReadOnly = true;
        }
        #endregion

        #region Object function
        private ObjectFunctionModel getObjectFunctionFromTable()
        {
            ObjectFunctionModel objectFunction = new ObjectFunctionModel();
            objectFunction.x1 = returnValueFromString(objectFunctionDataGridView.Rows[0].Cells[1].Value.ToString());
            objectFunction.x2 = returnValueFromString(objectFunctionDataGridView.Rows[0].Cells[2].Value.ToString());
            objectFunction.minimize = objectFunctionDataGridView.Rows[0].Cells[0].Value.ToString() == "F(x)->min" ? true:false;
            return objectFunction;
        }
        #endregion
    }
}
