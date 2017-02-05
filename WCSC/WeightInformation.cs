using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace WCSC
{
    public partial class WeightInformation : Form
    {
        double MAX_value_speed, MAX_value_weight, MAX_value_power;
        double MIN_value_speed, MIN_value_weight, MIN_value_power;

        public WeightInformation()
        {
            InitializeComponent();
            StartShowTimeDate();
            DrawGrph();
            
        }

        private void WeightInformation_Load(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                MIN_value_speed = Convert.ToDouble(numericUpDown2.Value);
                MAX_value_speed = Convert.ToDouble(numericUpDown1.Value);
                
                
                cartesianChart1.AxisY.RemoveAt(0);
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Скорость, м/с",
                    LabelFormatter = value => value.ToString("N"),
                    Foreground = System.Windows.Media.Brushes.Black,
                    MinValue = MIN_value_speed,
                    MaxValue = MAX_value_speed
                    
                });
                cartesianChart1.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Скорость:",
                        Values = new ChartValues<double> {0.4, 0.4, 0.6, 0.9, 0.9, 0.6, 0.5, 0.5, 0.5, 0.7 }
                    },
                };
                cartesianChart1.Update();
                cartesianChart1.Refresh();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                MIN_value_weight = Convert.ToDouble(numericUpDown4.Value);
                MAX_value_weight = Convert.ToDouble(numericUpDown3.Value);


                cartesianChart2.AxisY.RemoveAt(0);
                cartesianChart2.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Вес:",
                        Values = new ChartValues<double> {40, 40, 60, 90, 90, 96, 100, 100, 150, 170 }
                    },
                };


                cartesianChart2.AxisY.Add(new Axis
                {
                    Title = "Вес, т",
                    LabelFormatter = value => value.ToString("N"),
                    Foreground = System.Windows.Media.Brushes.Black,
                    MinValue = MIN_value_weight,
                    MaxValue = MAX_value_weight
                });
                cartesianChart2.Update();
                cartesianChart2.Refresh();
            }
        }

        private void numericUpDown5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                MIN_value_power = Convert.ToDouble(numericUpDown6.Value);
                MAX_value_power = Convert.ToDouble(numericUpDown5.Value);


                cartesianChart3.AxisY.RemoveAt(0);
                cartesianChart3.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Производительность:",
                        Values = new ChartValues<double> {120, 150, 110, 115, 95, 99, 250, 65, 55, 70 }
                    },
                };

               

                cartesianChart3.AxisY.Add(new Axis
                {
                    Title = "Производительность, т/ч",
                    LabelFormatter = value => value.ToString("N"),
                    Foreground = System.Windows.Media.Brushes.Black,
                    MinValue = MIN_value_power,
                    MaxValue = MAX_value_power
                });
                cartesianChart3.Update();
                cartesianChart3.Refresh();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
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
            MIN_value_speed = Convert.ToDouble(numericUpDown2.Value);
            MAX_value_speed = Convert.ToDouble(numericUpDown1.Value);

            MIN_value_weight = Convert.ToDouble(numericUpDown4.Value);
            MAX_value_weight = Convert.ToDouble(numericUpDown3.Value);

            MIN_value_power = Convert.ToDouble(numericUpDown6.Value);
            MAX_value_power = Convert.ToDouble(numericUpDown5.Value);

            cartesianChart3.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Производительность:",
                    Values = new ChartValues<double> {120, 150, 110, 115, 95, 99, 250, 65, 55, 70 }
                },
            };

            cartesianChart3.AxisX.Add(new Axis
            {
               // Title = "Month",
                Labels = new[] { "12:09:38\n23.11.06", "12:09:38\n23.11.06", "12:10:38\n23.11.06", "12:11:38\n23.11.06", "12:12:38\n23.11.06"
                , "12:13:38\n23.11.06", "12:14:38\n23.11.06", "12:15:38\n23.11.06", "12:16:38\n23.11.06", "12:17:38\n23.11.06" },
                Separator = new Separator
                {
                    Step = 1

                },

                Foreground = System.Windows.Media.Brushes.Black
            });

            cartesianChart3.AxisY.Add(new Axis
            {
                Title = "Производительность, т/ч",
                LabelFormatter = value => value.ToString("N"),
                Foreground = System.Windows.Media.Brushes.Black,
                MinValue = MIN_value_power,
                MaxValue = MAX_value_power
            });
            //////////////////////////////////////////////////////////
            cartesianChart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Вес:",
                    Values = new ChartValues<double> {40, 40, 60, 90, 90, 96, 100, 100, 150, 170 }
                },
            };

            cartesianChart2.AxisX.Add(new Axis
            {
                Labels = new[] { "", "", "", "", ""
                , "", "", "", "", "" }
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Вес, т",
                LabelFormatter = value => value.ToString("N"),
                Foreground = System.Windows.Media.Brushes.Black,
                MinValue = MIN_value_weight,
                MaxValue = MAX_value_weight
            });
            //////////////////////////////////////////
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Скорость:",
                    Values = new ChartValues<double> {0.4, 0.4, 0.6, 0.9, 0.9, 0.6, 0.5, 0.5, 0.5, 0.7 }
                },
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Labels = new[] { "", "", "", "", ""
                , "", "", "", "", "" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Скорость, м/с",
                LabelFormatter = value => value.ToString("N"),
                Foreground = System.Windows.Media.Brushes.Black,
                MinValue = MIN_value_speed,
                MaxValue = MAX_value_speed
            });



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
 }

