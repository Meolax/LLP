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
        public Form1 ()
        {
            InitializeComponent();
        }
        //56454
        private double returnValueFromTextBox (TextBox textBox)
        {
            try
            {
                return Convert.ToDouble(textBox.Text);
            }
            catch
            {
                return 0;
            }
        }
        private void createFunc (params double[] num)
        {
           // chart1.Series.Add("q");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; // тут сами поизменяет/повыбирайте тип вывода графика
            chart1.Series[0].Points.Clear();
            for (double i = -10; i < 10; i += 0.1)
            {
                var x = Math.Round(i, 2);
                chart1.Series[0].Points.AddXY(x, ((num[2]) - num[1] * x) / num[0]);
            }
                
            createStrich(chart1);
        }

        private void createStrich (Chart chart)
        {
            var x = chart.Series[0].Points;
            
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
