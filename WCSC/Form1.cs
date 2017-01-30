using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

namespace WCSC
{
    public partial class Form1 : Form
    {
        List<Device> device_list = null;
        public Form1()
        {
            Check_Connection_with_Controllers ck = new Check_Connection_with_Controllers();
            ck.ShowDialog();
            InitializeComponent();
            StartShowTime();
            TestDrawGraph();
            device_list = Check_Connection_with_Controllers.device_list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void StartShowTime()
        {
            Timer timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_tick);
            timer1.Interval = 100;
            timer1.Start();
        }

        public void timer1_tick(object sender, EventArgs e)
        {
            DateTime ThToday = DateTime.Now;
            string ThData = ThToday.ToString();
            this.textBox1.Text = ThData;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TestDrawGraph()
        {
            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Вес:",
                    Values = new ChartValues<double> { 124.4, 128.6, 39, 50, 122, 18.6, 87.6, 1.6, 128.6, 128.6, 12, 8.6, 188.6, 128.6 },
                    DataLabels = true
                }
            };
      


            //also adding values updates and animates the chart automatically
            cartesianChart1.Series[0].Values.Add(48d);

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Номер Секции",
                Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" },
                Separator = new Separator
                {
                    Step = 1
                },
               
                Foreground = Brushes.Black

            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Общий вес по секциям",
                LabelFormatter = value => value.ToString("N"),
                Separator = new Separator(),
               
                Foreground = Brushes.Black
            });




            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Вес:",
                    Values = new ChartValues<double> { 124.4, 128.6, 39, 50, 122, 18.6, 87.6, 1.6, 128.6 },
                    DataLabels = true
                }
            };



            //also adding values updates and animates the chart automatically
            cartesianChart2.Series[0].Values.Add(48d);

            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Номер Секции",
                Labels = new[] { "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" },
                Separator = new Separator
                {
                    Step = 1,
                    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))
                },
              
                Foreground = Brushes.Black
                
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Общий вес по секциям",
                LabelFormatter = value => value.ToString("N"),
                Separator = new Separator(),
                
                Foreground = Brushes.Black
            });
        }

        private void button8_Click(object sender, EventArgs e)
        {
                    Close();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Внимание!",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WeightInformation w = new WeightInformation();
            w.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            Settings s = new Settings();
                s.ShowDialog();
        }
    }
}
