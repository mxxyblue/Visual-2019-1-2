using A023_ArduinoSenserMonitoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace A023_PhotocellValue
{
    public partial class Form1 : Form
    {
        private double xCount = 200;
        SerialPort sPort;
        List<SensorData> myData = new List<SensorData>();
        Random r = new Random();
        Timer t = new Timer();

        SqlConnection conn;
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=E:\School\2학년\C#\A023_ArduinoSenserMonitoring\SensorData.mdf;
Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            // ComboBox
            foreach (var ports in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(ports);
            }
            comboBox1.Text = "Select Port";

            // 아두이노의 A0에서 받는 값의 범위
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 1023;

            // 차트 모양 세팅
            ChartSetting();

            //chart -> area, 시리즈로 구성
            // 숫자 버튼
            btnPortValue.BackColor = Color.Blue;
            btnPortValue.ForeColor = Color.White;
            btnPortValue.Text = "";
            btnPortValue.Font = new Font("맑은 고딕", 16, FontStyle.Bold);

            label1.Text = "Connection Time : ";
            textBox1.TextAlign = HorizontalAlignment.Center;
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = false;
        }

        private void ChartSetting()
        {
            chart1.ChartAreas.Clear(); //defalut로 하나씩 들어가있기때문에 초기화해줘야함
            chart1.ChartAreas.Add("draw");
            chart1.ChartAreas["draw"].AxisX.Minimum = 0; //x축
            chart1.ChartAreas["draw"].AxisX.Maximum = xCount;   // 최초에 차트 폭은 200으로 함
            chart1.ChartAreas["draw"].AxisX.Interval = xCount / 4;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].AxisY.Minimum = 0; //y축
            chart1.ChartAreas["draw"].AxisY.Maximum = 1024;
            chart1.ChartAreas["draw"].AxisY.Interval = 200;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].BackColor = Color.Blue;

            chart1.ChartAreas["draw"].CursorX.AutoScroll = true;

            chart1.ChartAreas["draw"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue;

            chart1.Series.Clear();
            chart1.Series.Add("PhotoCell");
            chart1.Series["PhotoCell"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell"].Color = Color.LightGreen;
            chart1.Series["PhotoCell"].BorderWidth = 3;
            if (chart1.Legends.Count > 0)
                chart1.Legends.RemoveAt(0); //legends라는 범례 지우기
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            ComboBox cb = sender as ComboBox;
            sPort = new SerialPort(cb.SelectedItem.ToString());
            //sPort.Open(); //포트를 연다
            //sPort.DataReceived += SPort_DataReceived; //데이터를 받을 때 마다 이 함수를 실행한다

            Timer t = new Timer(); //타이머를 하나 만들고
            t.Interval = 1000; //1초마다 틱이라는 함수가 실행된다
            t.Start();
            t.Tick += T_Tick;

            label1.Text = "Connection Time : " + DateTime.Now.ToString();
            btnDisconnect.Enabled = true;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            string s = r.Next(1024).ToString();
            this.BeginInvoke((new Action(delegate { ShowValue(s); }))); //showvalue 호출
            //여기서는 showvalue만 써줘도 실행가능
        }
        private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e) //메인프로세서에서 가지 친 프로세서
        {
            string s = sPort.ReadLine(); //s포트가 값을 받으면 그 값을 읽어라
            this.BeginInvoke((new Action(delegate { ShowValue(s); }))); //어려운 부분

            //그냥 함수면 showvalue쓰면 되지만 여기선 오류난다. 이 함수는 비동기식 동작이기 때문이다(예측할 수 없음)
            //메인이 아닌 곳에서는 저렇게 작성해야한다.
            //크로스 스레드 = 가지 친 프로세서 (이벤트 함수에서 처리하는 값)
        }

        private void ShowValue(string s) //아두이노에서 넘겨주는 값: string s (숫자 하나) 
        {
            int v = Int32.Parse(s); // 정수로 변환
            if (v < 0 || v > 1023)  // 처음 시작할 때 작은 값이나 큰 값이 들어오는 경우 배제 (아날로그 포트에 들어오는 값은 저 0~1023 사이의 값이니까)
                return;

            SensorData data = new SensorData( //SensorData :날짜 시간 값
              DateTime.Now.ToShortDateString(), //몇년 몇월 몇일
              DateTime.Now.ToString("HH:mm:ss"), v); //시간 분 초
            myData.Add(data); //SensorData의 list : myData
            DBInsert(data); //db 저장

            textBox1.Text = myData.Count.ToString();    // myData의 갯수를 표시
            progressBar1.Value = v; //원래는 v 대신 Int32.Parse(s)였음.

            // ListBox에 시간과 값을 표시
            string item = DateTime.Now.ToString() + "\t" + s; //Now.ToString()이렇게 하면 오늘 날짜 시간이 다 나옴
            listBox1.Items.Add(item);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;  // 계속 스크롤이 되도록 처리

            // Chart 표시
            chart1.Series["PhotoCell"].Points.Add(v); //v라는 값 집어넣음

            //zoom은 나중에 설명

            // zoom을 위해 200개까지는 기본, 데이터 갯수가 많아지면 200개만 보이지만, 스크롤 나타남
            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            //만약 maximum = mydata.count라고 해버리면 1000개의 데이터가 들어갔을 때 스크롤이 나타나는 게 아니라 1000개의
            //데이터가 하나의 그래프에서 보인다
            chart1.ChartAreas["draw"].AxisX.Maximum = (myData.Count >= xCount) ? myData.Count : xCount;

            // change chart range : Zoom 사용   //어디를 보여주는지 설정
            if (myData.Count > xCount) //데이터가 200보다 크면
            {
                chart1.ChartAreas["draw"].AxisX.ScaleView.Zoom(myData.Count - xCount, myData.Count);
                //만약 데이터가 300이라면 100부터 300까지 보여준다
            }
            else
            {
                chart1.ChartAreas["draw"].AxisX.ScaleView.Zoom(0, xCount);
            }

            // 숫자버튼 표시
            // btnPortValue.Text = sPort.PortName + "\n" + s;
            btnPortValue.Text = "\n" + s;

        }

        private void DBInsert(SensorData data)
        {
            string sql = string.Format("Insert into SensorTable" + "(Date, Time, Value) Values('{0}','{1}',{2})"
                , data.Date, data.Time, data.Value);
            //문자면 '{}' 숫자면 그냥 {}

            //connection을 하나 만들고 command 하나 만들고 connection 오픈, command 실행
            //실행이 끝나면 닫아준다.
            /*
             conn = new SqlConnection(connString)
             SqlCommand comm = new SqlCommand(sql, conn)
              conn.Open();
              comm.ExecuteNonQuery();
              conn.Close(); 
             */

            try
            {
                using (conn = new SqlConnection(connString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
            }
            
            finally { conn.Close(); }
        }

        private void btnPortValue_Click(object sender, EventArgs e) //포트 receive가 안되기때문에 버튼을 클릭하면 동작하게함
        {
            //class 밖에서 타이머를 하나 만들고
            t.Interval = 1000; //1초마다 틱이라는 함수가 실행된다
            t.Start();
            t.Tick += T_Tick;

            label1.Text = "Connection Time : " + DateTime.Now.ToString();
            btnDisconnect.Enabled = true;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            t.Stop();
        }
    }
}
