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

namespace LLP
{
    public partial class Form1 : Form
    {
        public Random random = new Random();
        public Form1 ()
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
                if ((c1+(c2*x)-1)/c == 0)
                {                    
                    return;
                }
                if ((c1 + (c2 * x) - 1) / c < 0)
                {
                    chartGraphic.Series[index].Points.AddXY((c - c2 * 0) / c1, 0);
                }

                 if (x % 0.5 == 0)
                {
                    addDotWithStrich(index, x, c1,c2,c);                    
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
            double c1 = returnValueFromTextBox(textBox1);
            double c2 = returnValueFromTextBox(textBox2);
            double c3 = returnValueFromTextBox(textBox3);
            //createFunc(0.1, c1, c2, c3);
            createGraphic(c1, c2, c3);
        }

        private void button2_Click (object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
