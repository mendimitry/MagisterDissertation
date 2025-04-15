using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Server
{
    public partial class FormMetric : Form
    {
        delegate void Progress(int value);
        public FormMetric()
        {

            InitializeComponent();
            timer1.Interval = 1200; // 500 миллисекунд

            Random rand = new Random();
            duration = rand.Next(3000, 10000);
            dt = DateTime.Now.AddMilliseconds(duration);

         





            // timer1.Interval = 10;

            timer1.Tick += timer1_Tick;

            timer1.Enabled = true;

            // chart1.ChartAreas[0].AxisY.Maximum = 45;
            //  chart1.ChartAreas[0].AxisY.Minimum = 0;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.Interval = 10;

            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart2.Series[0].XValueType = ChartValueType.DateTime;
            chart2.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart2.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart2.ChartAreas[0].AxisX.Interval = 5;

            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart3.Series[0].XValueType = ChartValueType.DateTime;
            chart3.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart3.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart3.ChartAreas[0].AxisX.Interval = 5;

            chart4.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart4.Series[0].XValueType = ChartValueType.DateTime;
            chart4.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart4.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart4.ChartAreas[0].AxisX.Interval = 5;

            chart5.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart5.Series[0].XValueType = ChartValueType.DateTime;
            chart5.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart5.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart5.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart5.ChartAreas[0].AxisX.Interval = 5;

            chart6.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart6.Series[0].XValueType = ChartValueType.DateTime;
            chart6.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart6.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart6.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart6.ChartAreas[0].AxisX.Interval = 5;

            Application.DoEvents();



        }
        DateTime dt;
        int duration;
        class P
        {
            int value;
            DateTime d;
            public P(int v, DateTime dd)
            {
                value = v;
                d = dd;
            }

            public int Value { get => value; set => this.value = value; }
            public DateTime D { get => d; set => d = value; }
        }
        private int _countSeconds = 0;
        List<P> fdfs = new List<P>();

        public DateTime Dt { get => dt; set => dt = value; }
        public int Duration { get => duration; set => duration = value; }
        public int CountSeconds { get => _countSeconds; set => _countSeconds = value; }
        private List<P> Fdfs { get => fdfs; set => fdfs = value; }

        private void timer1_Tick(object sender, EventArgs e)
        {

            double temp = duration - (dt - DateTime.Now).TotalMilliseconds;
            


            PerformanceCounter cpuCounter;

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");




            double tanTemp = cpuCounter.NextValue();



            Random random = new Random();
            DateTime timeNow = DateTime.Now;

            //int value = random.Next(0, 100);
            //chart1.Series[0].Points.AddXY(timeNow,value);

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions(64, true);
            PingReply reply;
            //string pubIp = new System.Net.WebClient().DownloadString("https://api.incolumitas.com/");
            // int ind = pubIp.Length - 1176;
            //pubIp = pubIp.Remove(ind);
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();
            reply = pingSender.Send("8.8.8.8", 3000, Encoding.ASCII.GetBytes(myuuidAsString), options);
            string result1 = String.Format("Пинг - {0} мс\r\nВремя прохождения пакета - {1} мс\r\nВремя жизни пакета - {2}\r\nРазмер буфера - {3}\r\n", reply.RoundtripTime.ToString(), reply.RoundtripTime, reply.Options.Ttl, reply.Buffer.Length);
            this.Invoke((MethodInvoker)(() => textBox1.Text = result1));


                    //listBox1.Invoke(new Action(() => listBox1.Items.Add("Статус " + reply.Status.ToString() + " Время отклика " + reply.RoundtripTime)));
                    
                
            



            


               
            



            
            


            





            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            // NetworkInterface[] networkCards
            //= System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            UdpStatistics udpStat11 = null;

            udpStat11 = properties.GetUdpIPv4Statistics();

            UdpStatistics tcpstat = properties.GetUdpIPv4Statistics();
            //reply.RoundtripTime.ToString(), reply.RoundtripTime, reply.Options.Ttl, reply.Buffer.Length
            chart1.Series[0].Points.AddXY(timeNow, tcpstat.DatagramsReceived);
            chart1.Series[1].Points.AddXY(timeNow, tcpstat.DatagramsSent);

            chart2.Series[0].Points.AddXY(timeNow, reply.RoundtripTime);
            chart2.Series[1].Points.AddXY(timeNow, udpStat11.UdpListeners);

            chart3.Series[0].Points.AddXY(timeNow, reply.Options.Ttl);
            chart3.Series[1].Points.AddXY(timeNow, udpStat11.UdpListeners);

           chart4.Series[0].Points.AddXY(timeNow, reply.Buffer.Length);
            chart4.Series[1].Points.AddXY(timeNow, udpStat11.UdpListeners);

            chart5.Series[0].Points.AddXY(timeNow, properties.GetIPv4GlobalStatistics().ReceivedPackets);
            chart5.Series[1].Points.AddXY(timeNow, udpStat11.UdpListeners);

            chart6.Series[0].Points.AddXY(timeNow, properties.GetIPv4GlobalStatistics().ReceivedPackets);
            chart6.Series[1].Points.AddXY(timeNow, udpStat11.UdpListeners);

            fdfs.Add(new P((int)tanTemp, timeNow));
            _countSeconds++;

            if (_countSeconds == 12)
            {
                _countSeconds = 0;
                chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart1.ChartAreas[0].AxisX.Interval = 6;

                chart2.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart2.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart2.ChartAreas[0].AxisX.Interval = 6;

                chart3.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart3.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart3.ChartAreas[0].AxisX.Interval = 6;

                chart4.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart4.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart4.ChartAreas[0].AxisX.Interval = 6;

                chart5.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart5.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart5.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart5.ChartAreas[0].AxisX.Interval = 6;

                chart6.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart6.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart6.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart6.ChartAreas[0].AxisX.Interval = 6;
















            }
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
