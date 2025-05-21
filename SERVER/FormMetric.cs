using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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

         
            chart5.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart5.Series[0].XValueType = ChartValueType.DateTime;
            chart5.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart5.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.2).ToOADate();
            chart5.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart5.ChartAreas[0].AxisX.Interval = 5;

        

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
        private static int GetProcessorIdleTime(string selectedServer)
        {
            try
            {
                var searcher = new
                   ManagementObjectSearcher
                     (@"\\" + selectedServer + @"\root\CIMV2",
                      "SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name=\"_Total\"");

                ManagementObjectCollection collection = searcher.Get();
                ManagementObject queryObj = collection.Cast<ManagementObject>().First();

                return Convert.ToInt32(queryObj["PercentIdleTime"]);
            }
            catch (ManagementException e)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }
            return -1;
        }
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

           // Ping pingSender = new Ping();
           // PingOptions options = new PingOptions(64, true);
          //  PingReply reply;
            //string pubIp = new System.Net.WebClient().DownloadString("https://api.incolumitas.com/");
            // int ind = pubIp.Length - 1176;
            //pubIp = pubIp.Remove(ind);
           // Guid myuuid = Guid.NewGuid();
           // string myuuidAsString = myuuid.ToString();
            //reply = pingSender.Send("8.8.8.8", 3000, Encoding.ASCII.GetBytes(myuuidAsString), options);
           // string result1 = String.Format("Пинг - {0} мс\r\nВремя прохождения пакета - {1} мс\r\nВремя жизни пакета - {2}\r\nРазмер буфера - {3}\r\n", reply.RoundtripTime.ToString(), reply.RoundtripTime, reply.Options.Ttl, reply.Buffer.Length);
            // this.Invoke((MethodInvoker)(() => textBox1.Text = result1));

           


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

           

            chart5.Series[0].Points.AddXY(timeNow, properties.GetIPv4GlobalStatistics().ReceivedPackets);
            chart5.Series[1].Points.AddXY(timeNow, udpStat11.UdpListeners);



            fdfs.Add(new P((int)tanTemp, timeNow));
            _countSeconds++;

            if (_countSeconds == 12)
            {
                _countSeconds = 0;
                chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart1.ChartAreas[0].AxisX.Interval = 6;

              

                chart5.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart5.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(0.1).ToOADate();
                chart5.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart5.ChartAreas[0].AxisX.Interval = 6;

                















            }
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Host = System.Net.Dns.GetHostName();
            string IP = System.Net.Dns.GetHostByName(Host).AddressList[0].ToString();
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c chcp 65001 & ping "+ IP,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });
            //process.BeginOutputReadLine();
            //process.OutputDataReceived += (s, e) => { label5.Text = e.Data; };
            textBox2.Text = process.StandardOutput.ReadToEnd();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = ipProps.GetActiveTcpConnections();
            var ipStats = ipProps.GetIPv4GlobalStatistics();

            StringBuilder sb = new StringBuilder();




            sb.AppendLine(string.Format($"Всего {tcpConnections.Length} активных TCP-подключений"));
            Console.WriteLine();
            foreach (var connection in tcpConnections)
            {
                sb.AppendLine(string.Format("============================================="));
                sb.AppendLine(string.Format($"Локальный адрес: {connection.LocalEndPoint.Address}:{connection.LocalEndPoint.Port}"));
                sb.AppendLine(string.Format($"Адрес удаленного хоста: {connection.RemoteEndPoint.Address}:{connection.RemoteEndPoint.Port}"));
                sb.AppendLine(string.Format($"Состояние подключения: {connection.State}"));

                textBox2.Text = sb.ToString();



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0]; // 1st NIC !
            PerformanceCounter performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
            PerformanceCounter performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var ipStats = ipProps.GetIPv4GlobalStatistics();
            for (int i = 0; i < 11; i++)
            {
                sb.AppendLine(string.Format("Отправленые байты: {0}k\tПолученные байты: {1}k", performanceCounterSent.NextValue() / 1024, performanceCounterReceived.NextValue() / 1024));
                Thread.Sleep(100);

                textBox2.Text = sb.ToString();
            }
            sb.AppendLine(string.Format($"Входящие пакеты: {ipStats.ReceivedPackets}"));
            sb.AppendLine(string.Format($"Исходящие пакеты: {ipStats.OutputPacketRequests}"));
            sb.AppendLine(string.Format($"Отброшено входящих пакетов: {ipStats.ReceivedPacketsDiscarded}"));
            sb.AppendLine(string.Format($"Отброшено исходящих пакетов: {ipStats.OutputPacketsDiscarded}"));
            sb.AppendLine(string.Format($"Ошибки фрагментации: {ipStats.PacketFragmentFailures}"));
            sb.AppendLine(string.Format($"Ошибки восстановления пакетов: {ipStats.PacketReassemblyFailures}"));
            textBox2.Text = sb.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher searcher8 =
new ManagementObjectSearcher("root\\CIMV2",
"SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher9 =
new ManagementObjectSearcher("root\\WMI",
"SELECT * FROM Win32_PerfFormattedData_Counters_ThermalZoneInformation");


            foreach (ManagementObject queryObj in searcher8.Get())
            {
                foreach (ManagementObject queryObj1 in searcher9.Get())
                {
                    sb.AppendLine(string.Format("Наименование: {0}", queryObj["Name"], queryObj["ProcessorId"]));
                    sb.AppendLine(string.Format("Число ядер: {0}", queryObj["NumberOfCores"]));
                    sb.AppendLine(string.Format("ProcessorId: {0}", queryObj["ProcessorId"]));

                    sb.AppendLine(string.Format("Температура: {0}", Convert.ToDouble(queryObj1["CurrentTemperature"]) - 273));

               
                    textBox2.Text = sb.ToString();
                }
        }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                sb.AppendLine(string.Format("Имя диска: " + drive.Name));
                sb.AppendLine(string.Format("Файловая система: " + drive.DriveFormat));
                sb.AppendLine(string.Format("Тип диска: " + drive.DriveType));
                sb.AppendLine(string.Format("Объем доступного свободного места (в байтах): " + drive.AvailableFreeSpace));
                sb.AppendLine(string.Format("Готов ли диск: " + drive.IsReady));
                sb.AppendLine(string.Format("Корневой каталог диска: " + drive.RootDirectory));
                sb.AppendLine(string.Format("Общий объем свободного места, доступного на диске (в байтах): " + drive.TotalFreeSpace));
                sb.AppendLine(string.Format("Размер диска (в байтах): " + drive.TotalSize));
                sb.AppendLine(string.Format("Метка тома диска: " + drive.VolumeLabel));
                textBox2.Text = sb.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();
            StringBuilder sb = new StringBuilder();
            foreach (ManagementObject result in results)
            {
                sb.AppendLine(string.Format("Общая память: {0} KB", result["TotalVisibleMemorySize"]));
                sb.AppendLine(string.Format("Свободная память: {0} KB", result["FreePhysicalMemory"]));
                sb.AppendLine(string.Format("Общая вир. память: {0} KB", result["TotalVirtualMemorySize"]));
                sb.AppendLine(string.Format("Свободная вир. память: {0} KB", result["FreeVirtualMemory"]));
                textBox2.Text = sb.ToString();
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Attribute
        {
            public byte AttributeID;
            public ushort Flags;
            public byte Value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] VendorData;
        }

       
        private void button7_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "$(Get-PhysicalDisk | Select *)[0]",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });
            //process.BeginOutputReadLine();
            //process.OutputDataReceived += (s, e) => { label5.Text = e.Data; };
            textBox2.Text = process.StandardOutput.ReadToEnd();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var process = new StringBuilder();

            ManagementObjectSearcher searcherProc = new ManagementObjectSearcher("root\\CIMV2", "Select Name, CommandLine From Win32_Process");


            
            foreach (System.Diagnostics.Process winProc in System.Diagnostics.Process.GetProcessesByName("ServerPing"))
            {

                process.AppendLine(string.Format("Процесс: {0}, Имя: {1}.exe", winProc.Id, winProc.ProcessName));

                textBox2.Text = "Копия программы под UID " + process.ToString() + "запущена. Работа стабильна";
               



            }





            
        }
    }
    }

