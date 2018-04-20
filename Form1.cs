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

        private void createFunc (params double[] num)
        {
            var index = createNewSeries();
            for (double i = -10; i <= 30; i += 0.1)
            {
                var x = Math.Round(i, 2);
                while ((num[2] - num[1] * x - 1)/num[0] < 0)
                {
                    x -= 0.0001;
                }
            
                if (x % 0.5 == 0)
                {
                    addDotWithStrich(index, x, num);                    
                }
                else
                {
                    chartGraphic.Series[index].Points.AddXY(x, (num[2] - num[1] * x) / num[0]);
                }
            }
        }

        private void button1_Click (object sender, EventArgs e)
        {
            double c1 = returnValueFromTextBox(textBox1);
            double c2 = returnValueFromTextBox(textBox2);
            double c3 = returnValueFromTextBox(textBox3);
            createFunc(c1, c2, c3);
        }
    }
}
