using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace A025_SnakeBite
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        Random r = new Random(); //랜덤한 위치
        Ellipse[] snakes = new Ellipse[30]; //30개 원의 배열
        Ellipse egg = new Ellipse(); //1개짜리 원
        private int size = 12;         // Egg와 Body의사이즈
        private int visibleCount = 5; // 처음에 보이는 뱀의 길이 (처음에 5개만 보여준다)
        private string move = "";      // 뱀의 이동방향
        private int eaten = 0;         // 먹은 알의 개수
        DispatcherTimer timer = new DispatcherTimer(); //Forms의 Timer와 똑같은 역할
        Stopwatch sw = new Stopwatch();
        private bool startFlag = false;

        public Window1()
        {
            InitializeComponent();
            //TestSnake();

            InitSnake(); //뱀을 초기화
            InitEgg();

            timer.Interval = new TimeSpan(0,0,0,0,100); //  (일, 시, 분, 초, 100ms)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (move != "") //마우스가 클릭되면 시작한다
            {
                startFlag = true;

                for (int i = visibleCount; i >= 1; i--) // 꼬리 하나를 더 계산 (움직였는데 알을 만났을 때 하나를 더 보여야하기때문)
                {
                    Point p = (Point)snakes[i - 1].Tag; 
                    snakes[i].Tag = new Point(p.X, p.Y); //앞 snake의 위치를 tag에 저장
                }

                Point pnt = (Point)snakes[0].Tag; //머리의 위치
                if (move == "Right")
                    snakes[0].Tag = new Point(pnt.X + size, pnt.Y);
                else if (move == "Left")
                    snakes[0].Tag = new Point(pnt.X - size, pnt.Y); //x위치 변화
                else if (move == "Up")
                    snakes[0].Tag = new Point(pnt.X, pnt.Y - size);
                else if (move == "Down")
                    snakes[0].Tag = new Point(pnt.X, pnt.Y + size); //y위치 변화
                EatEgg();   // 알을 먹었는지 체크 (좌표가 같은지 확인)
            }

            if (startFlag == true)
            {
                TimeSpan ts = sw.Elapsed;
                Time.Text = String.Format("Time = {0:00}:{1:00}.{2:00}",
                   ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                DrawSnakes();
            }
        }

        private void EatEgg()
        {
            Point pS = (Point)snakes[0].Tag;
            Point pE = (Point)egg.Tag;

            if (pS.X == pE.X && pS.Y == pE.Y)
            {
                egg.Visibility = Visibility.Hidden;
                visibleCount++;
                // 꼬리를 하나 늘림
                snakes[visibleCount - 1].Visibility = Visibility.Visible;
                Score.Text = "Eggs = " + (++eaten).ToString();

                if (visibleCount == 30)
                {
                    timer.Stop();
                    sw.Stop();
                    DrawSnakes();
                    TimeSpan ts = sw.Elapsed;
                    string tElapsed = String.Format("Time = {1:00}:{2:00}:{3:00}",
                        ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    MessageBox.Show("Success!!!  " + tElapsed + " sec");
                    this.Close();
                }

                Point p = new Point(r.Next(1, 480 / size) * size,
                  r.Next(1, 380 / size) * size);
                egg.Tag = p;
                egg.Visibility = Visibility.Visible;
                Canvas.SetLeft(egg, p.X);
                Canvas.SetTop(egg, p.Y);
            }
        }

        private void DrawSnakes()
        {
            for (int i = 0; i < visibleCount; i++)
            {
                Point p = (Point)snakes[i].Tag;
                Canvas.SetLeft(snakes[i], p.X);
                Canvas.SetTop(snakes[i], p.Y);
            }
        }

        private void InitEgg()
        {
            egg = new Ellipse();
            egg.Tag = new Point(r.Next(1, 480 / size) * size,
               r.Next(1, 380 / size) * size); //랜덤하게 위치 생성
            egg.Width = size;
            egg.Height = size;
            egg.Stroke = Brushes.Black;
            egg.Fill = Brushes.Red;

            Point p = (Point)egg.Tag;
            Canvas1.Children.Add(egg);
            Canvas.SetLeft(egg, p.X);
            Canvas.SetTop(egg, p.Y); //위치를 정해준다(set)
        }

            private void InitSnake()
        {
            for (int i = 0; i < 30; i++)
            {
                snakes[i] = new Ellipse(); 
                snakes[i].Width = size; //크기 지정
                snakes[i].Height = size;
                if (i == 0)
                    snakes[i].Fill = Brushes.Chocolate; // 머리 색깔변경
                else if (i % 5 == 0)
                    snakes[i].Fill = Brushes.YellowGreen; // 5번째 마디 색깔변경
                else
                    snakes[i].Fill = Brushes.Gold;
                snakes[i].Stroke = Brushes.Black;
                Canvas1.Children.Add(snakes[i]); //children에 Add 해줬다
            }

            for (int i = visibleCount; i < 30; i++)
            {
                snakes[i].Visibility = Visibility.Hidden; //나머지 25개는 hidden 해놓기
            }

            int x = r.Next(1, 480 / size) * size;
            int y = r.Next(1, 380 / size) * size;
            CreateSnake(x, y); //몸통 만드는 함수
        }

        private void CreateSnake(int x, int y) //제일 중요, 뱀이 머리에서부터 아래쪽으로 5개 마디의 위치를 지정함
        {
            for (int i = 0; i < visibleCount; i++)
            {
                snakes[i].Tag = new Point(x, y + i * size); 
                //Tag (object type; 아무거나 올 수 있다) // tag에 x,y라는 위치(point)를 저장하겠다
                Canvas.SetLeft(snakes[i], x);
                Canvas.SetTop(snakes[i], y + i * size); //뱀의 위치가 랜덤하게 만들었던 x,y로 지정된다
                //두번째 루프에선 size 만큼 더 큰 size가 들어간다 -> 첫번째 원의 뒤에 연결되어 나타난다
            }
        }

        private void TestSnake()
        {
            Ellipse x = new Ellipse();
            x.Width = 20;
            x.Height = 20; //크기 설정
            x.Stroke = Brushes.Black; //색을 지정할 때는 brushes 사용
            x.Fill = Brushes.Red;
            Canvas.SetLeft(x, 100);
            Canvas.SetTop(x, 100);
            Canvas1.Children.Add(x); //이 문장을 추가해야 그려진다

            Ellipse[] snake = new Ellipse[30];

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (move == "")  // 맨 처음 키가 눌리면 sw 시작
                sw.Start();

            if (e.Key == Key.Right)
                move = "Right";
            else if (e.Key == Key.Left)
                move = "Left";
            else if (e.Key == Key.Up)
                move = "Up";
            else if (e.Key == Key.Down)
                move = "Down";
            else if (e.Key == Key.Escape)
                move = "";
        }
    }
}
