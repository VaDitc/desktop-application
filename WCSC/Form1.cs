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
        int Count_WEIGHT = 0;
        scalesEntities bd = Check_Connection_with_Controllers.bd;


        public Form1()
        {
            Check_Connection_with_Controllers ck = new Check_Connection_with_Controllers();
            ck.Show();
            InitializeComponent();
            StartShowTime();
            TestDrawGraph();
            device_list = Check_Connection_with_Controllers.device_list;
            IQueryable<ScalesInformation> query =  bd.ScalesInformation;
            Count_WEIGHT = query.Count();
            
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

            Timer timer2 = new Timer();
            timer2.Tick += new EventHandler(timer2_tick);
            timer2.Interval = 100;
            timer2.Start();

        }

        public void timer2_tick(object sender, EventArgs e)
        {
            DateTime ThToday = DateTime.Now;
            string shift_day_start = "08:00:00";
            string shift_day_end = "20:00:00";
            TimeSpan dt_day = TimeSpan.Parse(shift_day_start);
            TimeSpan dt_end = TimeSpan.Parse(shift_day_end);


            if (ThToday.TimeOfDay > dt_day && ThToday.TimeOfDay < dt_end)
            {
                textBox2.Text = "1-я смена. День";
                

            }
            else
            {
                textBox2.Text = "2-я смена. Ночь";
                
            }
        }

        public void timer1_tick(object sender, EventArgs e)
        {
            DateTime ThToday = DateTime.Now;
            string ThData = ThToday.ToString();
            this.textBox1.Text = ThData;
            

            

            
            //IQueryable<ScalesInformation> query = bd.ScalesInformation;
            //test = query.ToList();
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
                    Values = new ChartValues<double> { 124.4, 125.6, 39, 50, 122},
                    DataLabels = true
                }
            };
      


            //also adding values updates and animates the chart automatically
            //cartesianChart1.Series[0].Values.Add(48d);

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Номер весов",
                Labels = new[] { "1", "2", "3", "4", "5"},
                Separator = new Separator
                {
                    Step = 1
                },
               
                Foreground = Brushes.Black

            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Общий вес по весам",
                LabelFormatter = value => value.ToString("N"),
                Separator = new Separator(),
               
                Foreground = Brushes.Black
            });




            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Вес:",
                    Values = new ChartValues<double> { 124.4, 125.6, 39, 50, 122},
                    DataLabels = true
                }
            };



            //also adding values updates and animates the chart automatically
            //cartesianChart2.Series[0].Values.Add(48d);

            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Номер весов",
                Labels = new[] { "6", "7", "8", "9", "10"},
                Separator = new Separator
                {
                    Step = 1,
                    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))
                },
              
                Foreground = Brushes.Black
                
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Общий вес по весам",
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

        private void button1_Click(object sender, EventArgs e)
        {
            archive form_arhive = new archive();
            form_arhive.Show();
        }
    }
}
