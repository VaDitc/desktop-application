﻿using System;
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
        //scalesEntities bd = Check_Connection_with_Controllers.bd;
        List<DataByShift> data_shift = null;
        ChartValues<double> chart1DataY = new ChartValues<double>();
        ChartValues<double> chart2DataY = new ChartValues<double>();
        List<string> chart1DataX = new List<string>();
        List<string> chart2DataX = new List<string>();

        public Form1()
        {
            Check_Connection_with_Controllers ck = new Check_Connection_with_Controllers();
            //ck.Show();
            InitializeComponent();
            StartShowTime();
           
            Get_Info_Data_shit();
            device_list = Check_Connection_with_Controllers.device_list;


            cartesianChart1.DisableAnimations = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Get_Info_Data_shit()
        {
            cartesianChart1.DisableAnimations = true;
            scalesEntities bd = new scalesEntities();
            IQueryable<ScalesInformation> query = bd.ScalesInformation;
            Count_WEIGHT = query.Count();
            IQueryable<DataByShift> query_data_shift = bd.DataByShift.OrderByDescending(x => x.ID).Take(Count_WEIGHT).OrderBy(x => x.ID);
            data_shift = query_data_shift.ToList();
            DrawGraph(data_shift);
            
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

        private void InsertData()
        {
            Random r = new Random();
            Random WP = new Random();
            scalesEntities bd = new scalesEntities();
            MeasurementData a = null;
            try
            {
                a = bd.MeasurementData.OrderByDescending(e => e.ID).First();
            }
            catch (Exception)
            {
                a = new MeasurementData();
                a.ID = 0; 
            }
            
            MeasurementData inf1 = new MeasurementData();
            inf1.ID = a.ID + 1;
            inf1.ScalesNumberID = 1;
            inf1.CurrentSpeed = r.Next(3, 6);
            inf1.CurrentWeight = WP.Next(10, 55);
            inf1.CurrentProductivity = WP.Next(100, 141);
            inf1.TimeOfMeasurement = DateTime.Now;
            bd.MeasurementData.Add(inf1);

            MeasurementData inf2 = new MeasurementData();
            inf2.ID = a.ID + 2;
            inf2.ScalesNumberID = 2;
            inf2.CurrentSpeed = r.Next(3, 6);
            inf2.CurrentWeight = WP.Next(10, 55);
            inf2.CurrentProductivity = WP.Next(100, 141);
            inf2.TimeOfMeasurement = DateTime.Now;
            bd.MeasurementData.Add(inf2);

            MeasurementData inf3 = new MeasurementData();
            inf3.ID = a.ID + 3;
            inf3.ScalesNumberID = 3;
            inf3.CurrentSpeed = r.Next(3, 6);
            inf3.CurrentWeight = WP.Next(10, 55);
            inf3.CurrentProductivity = WP.Next(100, 141);
            inf3.TimeOfMeasurement = DateTime.Now;
            bd.MeasurementData.Add(inf3);
            bd.SaveChanges();
            bd.Dispose();
        }

        private void RefreshGraph()
        {
            
            chart1DataX.Clear();
            chart2DataX.Clear();
            chart1DataY.Clear();
            chart2DataY.Clear();
            scalesEntities bd = new scalesEntities();
            IQueryable<ScalesInformation> query = bd.ScalesInformation;
            Count_WEIGHT = query.Count();
            IQueryable<DataByShift> query_data_shift = bd.DataByShift.OrderByDescending(x => x.ID).Take(Count_WEIGHT).OrderBy(x => x.ID);
            List<DataByShift> ds_info = query_data_shift.ToList();
            if (Count_WEIGHT > 5)
            {
                for (int i = 0; i < ds_info.Count; i++)
                {
                    if (i < 5)
                    {
                        chart1DataY.Add(ds_info[i].Weight.Value);
                        chart1DataX.Add(ds_info[i].ScalesNumberID.ToString());
                    }
                    else
                    {
                        chart2DataY.Add(ds_info[i].Weight.Value);
                        chart2DataX.Add(ds_info[i].ScalesNumberID.ToString());
                    }
                }
                cartesianChart1.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Вес:",
                        Values = chart1DataY,
                        DataLabels = true
                    }
                };

                cartesianChart2.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Вес:",
                        Values = chart2DataY,
                        DataLabels = true
                    }
                };
            }
            else
            {
                foreach (var item in ds_info)
                {
                    chart1DataY.Add(item.Weight.Value);
                    chart1DataX.Add(item.ScalesNumberID.ToString());
                }
                cartesianChart1.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Вес:",
                        Values = chart1DataY,
                        DataLabels = true
                    }
                };
              }
            bd.Dispose();
        }

        private void DrawGraph(List<DataByShift> ds_info)
        {

            if (Count_WEIGHT > 5)
            {
                for (int i = 0; i < ds_info.Count; i++)
                {
                    if (i < 5)
                    {
                        chart1DataY.Add(ds_info[i].Weight.Value);
                        chart1DataX.Add(ds_info[i].ScalesNumberID.ToString());
                    }
                    else
                    {
                        chart2DataY.Add(ds_info[i].Weight.Value);
                        chart2DataX.Add(ds_info[i].ScalesNumberID.ToString());
                    }
                }
                cartesianChart1.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Вес:",
                        Values = chart1DataY,
                        DataLabels = true
                    }
                };

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Номер весов",
                    Labels = chart1DataX,
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
                        Values = chart2DataY,
                        DataLabels = true
                    }
                };

                cartesianChart2.AxisX.Add(new Axis
                {
                    Title = "Номер весов",
                    Labels = chart2DataX,
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
            else
            {
                foreach (var item in ds_info)
                {
                    chart1DataY.Add(item.Weight.Value);
                    chart1DataX.Add(item.ScalesNumberID.ToString());
                }
                cartesianChart1.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Вес:",
                        Values = chart1DataY,
                        DataLabels = true
                    }
                };

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Номер весов",
                    Labels = chart1DataX,
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

                cartesianChart2.AxisX.Add(new Axis
                {
                    Title = "Номер весов",
                    MinValue = 0,
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
                    
                    Separator = new Separator(),
                    MinValue = 0,
                    Foreground = Brushes.Black
                });
            }

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
            RefreshGraph();
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

        private void button3_Click(object sender, EventArgs e)
        {
            InsertData();
        }
    }
}
