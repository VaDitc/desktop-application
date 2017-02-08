using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brushes = System.Windows.Media.Brushes;

using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace WCSC
{
    public partial class WeightInformation : Form
    {
        bool first_start = false;
        
        ChartValues<double> chart1DataY = new ChartValues<double>();
        ChartValues<double> chart2DataY = new ChartValues<double>();
        ChartValues<double> chart3DataY = new ChartValues<double>();
        List<string> chart1DataX = new List<string>();
        List<string> chart2DataX = new List<string>();
        List<string> chart3DataX = new List<string>();

        public WeightInformation()
        {
            InitializeComponent();
            Form1.DataLive += Form1_DataLive;
            StartShowTimeDate();
            LoadComboBoxDevice();
            DrawGrph();
            cartesianChart2.DisableAnimations = true;
            cartesianChart3.DisableAnimations = true;
            //The next code simulates data changes every 500 ms

        }

        private void Form1_DataLive(double weightReal, double powerReal)
        {
            textBox4.Text = weightReal.ToString();
            textBox5.Text = powerReal.ToString();
            if (chart2DataY.Count > 12)
            {
                chart3DataX.RemoveAt(0);
                chart3DataY.RemoveAt(0);
                chart2DataY.RemoveAt(0);
                DateTime dat = DateTime.Now;
                string DateLabel = dat.ToString("HH:mm:ss") + "\n" + dat.ToString("dd.MM.yy");

                chart3DataX.Add(DateLabel);
                chart3DataY.Add(powerReal);
                chart2DataY.Add(weightReal);

                cartesianChart2.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Вес:",
                        Values = chart2DataY,
                        LineSmoothness = 0,
                        DataLabels = true
                    },
                };
                cartesianChart2.AxisX.Clear();
                cartesianChart2.AxisX.Add(new Axis
                {
                    Labels = chart3DataX,
                    MinValue = 0,
                    Separator = new Separator
                    {
                        Step = 1
                    },
                    Foreground = Brushes.Black
                });
                /////////////////////////////////
                cartesianChart3.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Производительность:",
                        Values = chart3DataY,
                        LineSmoothness = 0,
                        DataLabels = true
                    },
                };
                cartesianChart3.AxisX.Clear();
                cartesianChart3.AxisX.Add(new Axis
                {
                    Labels = chart3DataX,
                    MinValue = 0,
                    IsEnabled = false,
                    Separator = new Separator
                    {
                        Step = 1
                    },
                    Visibility = System.Windows.Visibility.Hidden,
                    Foreground = Brushes.Black
                });
            }
            else
            {
                DateTime dat = DateTime.Now;
                string DateLabel = dat.ToString("HH:mm:ss") + "\n" + dat.ToString("dd.MM.yy");

                chart3DataX.Add(DateLabel);
                chart3DataY.Add(powerReal);
                chart2DataY.Add(weightReal);
                chart2DataX.Add("");


                cartesianChart2.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Вес:",
                        Values = chart2DataY,
                        LineSmoothness = 0,
                        DataLabels = true
                    },
                };
                cartesianChart2.AxisX.Clear();
                cartesianChart2.AxisX.Add(new Axis
                {
                    Labels = chart3DataX,
                    MinValue = 0,
                    IsEnabled = false,
                    Separator = new Separator
                    {
                        Step = 1
                    },
                    Visibility = System.Windows.Visibility.Hidden,
                    Foreground = Brushes.Black
                });
                /////////////////////////////////
                cartesianChart3.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Производительность:",
                        Values = chart3DataY,
                        LineSmoothness = 0,
                        DataLabels = true
                    },
                };
                cartesianChart3.AxisX.Clear();
                cartesianChart3.AxisX.Add(new Axis
                {
                    Labels = chart3DataX,
                    MinValue = 0,
                    IsEnabled = false,
                    Separator = new Separator
                    {
                        Step = 1
                    },
                    Visibility = System.Windows.Visibility.Hidden,
                    Foreground = Brushes.Black
                });
            }

        }

        ///////////////////////////////////////////////////////////////////
        /// </summary>

        private void LoadComboBoxDevice()
        {
            List<Device> list_con_device = Check_Connection_with_Controllers.device_list;
            comboBox1.Items.Clear();
            foreach (var item in list_con_device)
            {
                comboBox1.Items.Add(item.device_number);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            
        }

        private void WeightInformation_Load(object sender, EventArgs e)
        {
           
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void StartShowTimeDate()
        {
            Timer timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_tick);
            timer1.Interval = 100;
            timer1.Start();
        }

        public void timer1_tick(object sender, EventArgs e)
        {
            DateTime ThToday = DateTime.Now;
            string ThData = ThToday.Date.ToShortDateString();
            string ThTime = ThToday.ToLongTimeString();
            this.textBox1.Text = ThData;
            this.textBox2.Text = ThTime;
        }

        public void DrawGrph()
        {

            //cartesianChart3.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title = "Производительность:",
            //        Values = new ChartValues<double> {120, 150, 110, 115, 95, 99, 250, 65, 55, 70 }
            //    },
            //};

            //cartesianChart3.AxisX.Add(new Axis
            //{
            //   // Title = "Month",
            //    Labels = new[] { "12:09:38\n23.11.06", "12:09:38\n23.11.06", "12:10:38\n23.11.06", "12:11:38\n23.11.06", "12:12:38\n23.11.06"
            //    , "12:13:38\n23.11.06", "12:14:38\n23.11.06", "12:15:38\n23.11.06", "12:16:38\n23.11.06", "12:17:38\n23.11.06" },
            //    Separator = new Separator
            //    {
            //        Step = 1

            //    },

            //    Foreground = System.Windows.Media.Brushes.Black
            //});

            cartesianChart3.AxisY.Add(new Axis
            {
                Title = "Производительность, т/ч",
                LabelFormatter = value => value.ToString("N"),
                Foreground = System.Windows.Media.Brushes.Black,
                MinValue = 0

            });
            //////////////////////////////////////////////////////////

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Вес, т",
                LabelFormatter = value => value.ToString("N"),
                Foreground = System.Windows.Media.Brushes.Black,
                MinValue = 0
                
               
            });
            //////////////////////////////////////////
            //cartesianChart1.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title = "Скорость:",
            //        Values = new ChartValues<double> {0.4, 0.4, 0.6, 0.9, 0.9, 0.6, 0.5, 0.5, 0.5, 0.7 }
            //    },
            //};

            //cartesianChart1.AxisX.Add(new Axis
            //{
            //    Labels = new[] { "", "", "", "", ""
            //    , "", "", "", "", "" }
            //});

            //cartesianChart1.AxisY.Add(new Axis
            //{
            //    Title = "Скорость, м/с",
            //    LabelFormatter = value => value.ToString("N"),
            //    Foreground = System.Windows.Media.Brushes.Black,
            //    MinValue = 0,
                
            //});



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

    }
    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }

}

