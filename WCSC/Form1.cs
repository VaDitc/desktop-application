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
using System.Net.Sockets;

namespace WCSC
{
    public partial class Form1 : Form
    {
        // Объявляем делегат
        public delegate void DataChangeHandler();
        // Событие, возникающее при выводе денег
        public static event DataChangeHandler AllGraphRefresh;

        // Объявляем делегат
        public delegate void DataChangeFORwi(double weightReal, double powerReal);
        // Событие, возникающее при выводе денег
        public static event DataChangeFORwi DataLive;


        List<Device> device_list = null;
        int Count_WEIGHT = 0;
        //scalesEntities bd = Check_Connection_with_Controllers.bd;
        List<DataByShift> data_shift = null;
        ChartValues<double> chart1DataY = new ChartValues<double>();
        ChartValues<double> chart2DataY = new ChartValues<double>();
        List<string> chart1DataX = new List<string>();
        List<string> chart2DataX = new List<string>();

        // FOR CONTROLLER


        //

        public Form1()
        {
            Check_Connection_with_Controllers ck = new Check_Connection_with_Controllers();
            ck.ShowDialog();
            InitializeComponent();
            StartShowTime();
           
            Get_Info_Data_shit();
            device_list = Check_Connection_with_Controllers.device_list;


            cartesianChart1.DisableAnimations = true;

            // FOR CONTROLLER
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Get_Info_Data_shit()
        {
            cartesianChart1.DisableAnimations = true;
            scalesEntities1 bd = new scalesEntities1();
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

            Timer timer3 = new Timer();
            timer3.Tick += new EventHandler(timer3_tick);
            timer3.Interval = 1000;
            timer3.Start();

            Timer timer4 = new Timer();
            timer4.Tick += new EventHandler(timer4_tick);
            timer4.Interval = 1000;
            timer4.Start();

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

        public void timer3_tick(object sender, EventArgs e)
        {
            //InsertData();
            //if (AllGraphRefresh != null)
             //   AllGraphRefresh();

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
            scalesEntities1 bd = new scalesEntities1();
           // MeasurementData a = null;
            //try
            //{
            //    a = bd.MeasurementData.OrderByDescending(e => e.ID).First();
            //}
            //catch (Exception)
            //{
            //    a = new MeasurementData();
            //    a.ID = 0; 
            //}
            
            MeasurementData inf1 = new MeasurementData();
            //inf1.ID = a.ID + 1;
            inf1.ScalesNumberID = 1;
            inf1.CurrentSpeed = r.Next(3, 6);
            inf1.CurrentWeight = WP.Next(10, 55);
            inf1.CurrentProductivity = WP.Next(100, 141);
            inf1.TimeOfMeasurement = DateTime.Now;
            bd.MeasurementData.Add(inf1);

            MeasurementData inf2 = new MeasurementData();
            //inf2.ID = a.ID + 2;
            inf2.ScalesNumberID = 2;
            inf2.CurrentSpeed = r.Next(3, 6);
            inf2.CurrentWeight = WP.Next(10, 55);
            inf2.CurrentProductivity = WP.Next(100, 141);
            inf2.TimeOfMeasurement = DateTime.Now;
            bd.MeasurementData.Add(inf2);

            MeasurementData inf3 = new MeasurementData();
            //inf3.ID = a.ID + 3;
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
            scalesEntities1 bd = new scalesEntities1();
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
            WeightHour wh = new WeightHour();
            wh.Show();
            //RefreshGraph();
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
            WeightDay wd = new WeightDay();
            wd.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WeightMonth wm = new WeightMonth();
            wm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // FOR CONTROLLER

        private void timer4_tick(object sender, EventArgs e)
        {

            Get_info_async();
        }

        private async void Get_info_async()
        {
            await GetInfo();
        }

        private async Task GetInfo()
        {
            foreach (var item in device_list)
            {
                NetworkStream serverStream = default(NetworkStream);
                try
                {
                    //clientSocket.Connect(item.device_ip, 9761);
                    serverStream = item.clientSocket.GetStream();

                    String[] message = new String[] { item.device_number, "03", "00", "12", "00", "02", "64", "0E" };
                    Byte[] mes = new Byte[128];         //переменная, которая будет содержать данные для отправки
                    int i = 0;                      // счетчик

                    for (i = 0; i < 8; i++)
                    {

                        mes[i] = StrHexToByte(message[i]);

                    }

                    serverStream.Write(mes, 0, 8);
                    serverStream.Flush();

                    serverStream = item.clientSocket.GetStream();            //получаем поток
                    int buffSize = 0;
                    int bytesRead = 0;
                    byte[] inStream = new byte[10025];                  // инициализируем массив для приема данных
                    buffSize = item.clientSocket.ReceiveBufferSize;          //получаем размер буфера
                    bytesRead = serverStream.Read(inStream, 0, buffSize);//считываем данные из потока

                    if (bytesRead > 0)
                    {
                        string answer_Weight = "";
                        string answer_Power = "";
                        for (int j = 3; j < 7; j++)
                        {
                            answer_Weight = answer_Weight + ByteToStrHex(inStream[j]);
                            //формируем сообщения для вывода в ListView в 16-ричном виде
                        }
                        for (int z = 7; z < 11; z++)
                        {
                            answer_Power = answer_Power + ByteToStrHex(inStream[z]);
                            //формируем сообщения для вывода в ListView в 16-ричном виде
                        }
                        int weight_geter = int.Parse(answer_Weight,System.Globalization.NumberStyles.AllowHexSpecifier);
                        double REALY_WEIGHT_VARIABLE = weight_geter * 0.001;

                        int power_geter = int.Parse(answer_Power, System.Globalization.NumberStyles.AllowHexSpecifier);
                        double REALY_POWER_VARIABLE = weight_geter * 0.001;

                        if (REALY_WEIGHT_VARIABLE < 0)
                        {
                            REALY_WEIGHT_VARIABLE = 0;
                        }

                        if (REALY_POWER_VARIABLE < 0)
                        {
                            REALY_POWER_VARIABLE = 0;
                        }
                        if (DataLive != null)
                            DataLive(REALY_WEIGHT_VARIABLE,REALY_POWER_VARIABLE);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private byte StrHexToByte(string sHex)
        {
            try
            {
                byte ret = 0;
                //bNoError = true;

                string hxH = "";
                string hxL = "";
                if (sHex.Length == 2)
                {
                    hxH = sHex.Substring(0, 1);
                    hxL = sHex.Substring(1, 1);
                }
                else if (sHex.Length == 1)
                {
                    hxL = sHex.Substring(0, 1);
                }
                else
                {
                    //bNoError = false;
                    return 0;
                }

                if (hxH == "0") ret = 0;
                else if (hxH == "1") ret = 16;
                else if (hxH == "2") ret = 16 * 2;
                else if (hxH == "3") ret = 16 * 3;
                else if (hxH == "4") ret = 16 * 4;
                else if (hxH == "5") ret = 16 * 5;
                else if (hxH == "6") ret = 16 * 6;
                else if (hxH == "7") ret = 16 * 7;
                else if (hxH == "8") ret = 16 * 8;
                else if (hxH == "9") ret = 16 * 9;
                else if (hxH == "A" || hxH == "a") ret = 16 * 10;
                else if (hxH == "B" || hxH == "b") ret = 16 * 11;
                else if (hxH == "C" || hxH == "c") ret = 16 * 12;
                else if (hxH == "D" || hxH == "d") ret = 16 * 13;
                else if (hxH == "E" || hxH == "e") ret = 16 * 14;
                else if (hxH == "F" || hxH == "f") ret = 16 * 15;

                if (hxL == "0") ret += 0;
                else if (hxL == "1") ret += 1;
                else if (hxL == "2") ret += 2;
                else if (hxL == "3") ret += 3;
                else if (hxL == "4") ret += 4;
                else if (hxL == "5") ret += 5;
                else if (hxL == "6") ret += 6;
                else if (hxL == "7") ret += 7;
                else if (hxL == "8") ret += 8;
                else if (hxL == "9") ret += 9;
                else if (hxL == "A" || hxL == "a") ret += 10;
                else if (hxL == "B" || hxL == "b") ret += 11;
                else if (hxL == "C" || hxL == "c") ret += 12;
                else if (hxL == "D" || hxL == "d") ret += 13;
                else if (hxL == "E" || hxL == "e") ret += 14;
                else if (hxL == "F" || hxL == "f") ret += 15;
                else
                {
                    //bNoError = false;
                    return 0;
                }

                return ret;
            }
            catch (Exception ex)
            {
                //bNoError = false;
                return 0;
            }
        }

        private string ByteToStrHex(byte b)
        {
            try
            {
                int iTmpH = b / (byte)16;
                int iTmpL = b % (byte)16;
                string ret = "";

                if (iTmpH < 10)
                    ret = iTmpH.ToString();
                else
                {
                    if (iTmpH == 10) ret = "A";
                    if (iTmpH == 11) ret = "B";
                    if (iTmpH == 12) ret = "C";
                    if (iTmpH == 13) ret = "D";
                    if (iTmpH == 14) ret = "E";
                    if (iTmpH == 15) ret = "F";
                }

                if (iTmpL < 10)
                    ret += iTmpL.ToString();
                else
                {
                    if (iTmpL == 10) ret += "A";
                    if (iTmpL == 11) ret += "B";
                    if (iTmpL == 12) ret += "C";
                    if (iTmpL == 13) ret += "D";
                    if (iTmpL == 14) ret += "E";
                    if (iTmpL == 15) ret += "F";
                }

                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        // END FOR CONTROLLER
    }
}
