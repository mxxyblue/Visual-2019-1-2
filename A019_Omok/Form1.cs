using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A019_Omok
{
    enum STONE { none, black, white}; //enum - 열거형.. int가 정수를 저장할 수 있는 것처럼 저 값을 가질 수 있음
    public partial class Form1 : Form //partial -> 조각난
    {
        private int mgn = 30;
        private int 눈 = 30;
        private int 화점 = 10;
        private int 돌 = 26;
        private Graphics g;
        private Pen pen = new Pen(Color.Black);
        private Brush bBrush = new SolidBrush(Color.Black);
        private Brush wBrush = new SolidBrush(Color.White);
        private bool flag;
        private STONE[,] 바둑판 = new STONE[19, 19];
        private bool imageFlag = true;

        int stoneCnt = 1;
        Font font = new Font("맑은 고딕", 10);

        List<Revive> lstRevive = new List<Revive>();  // 리스트 <>은 제너릭 리스트 이다 어떤 형태의 변수도 들어갈 수 있음
        private bool reviveFlag;
        private string dirName;
        private string filename;

        public Form1() //프로그램 시작 시 가장 먼저 실행되는 부분
        {
            InitializeComponent();

            this.Text = "오목 by 신수민";
            panel1.BackColor = Color.Orange;
            this.ClientSize = new Size(2 * mgn + 18 * 눈, 2 * mgn + 18 * 눈 +menuStrip1.Height);

            g = panel1.CreateGraphics();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); //건드리지 않는다
            DrawBoard(); //바둑판 그리기-페인트 이벤트가 생길때 사용하는 것이 좋기때문에 여기생성
            DrawStone();
        }

        private void DrawStone()
        {
            for(int x = 0; x<19; x++)
            {
                for(int y = 0; y<19; y++)
                {
                    if(바둑판[x,y] == STONE.black)
                    {   Rectangle r = new Rectangle(mgn + x * 눈 - 돌 / 2, mgn + y * 눈 - 돌 / 2, 돌, 돌);
                        if (imageFlag == false)
                            g.FillEllipse(bBrush, r);
                        else
                        {
                            Bitmap bmp = new Bitmap("../../images/black.png");
                            g.DrawImage(bmp, r);
                        }
                    }
                    else if (바둑판[x, y] == STONE.white)
                    {
                        Rectangle r = new Rectangle(mgn + x * 눈 - 돌 / 2, mgn + y * 눈 - 돌 / 2, 돌, 돌);
                        if (imageFlag == false)
                            g.FillEllipse(wBrush, r);
                        else
                        {
                            Bitmap bmp = new Bitmap("../../images/white.png");
                            g.DrawImage(bmp, r);
                        }
                    }
                }
            }
        }

        //바둑판그리기
        //Graphics 객체, pen 객체, brush 객체
        private void DrawBoard()
        {
            //선을 19개 그리면 된다
            //g.DrawLine(pen, Point, Point)
            //가로줄, 세로줄
            for (int i = 0; i < 19; i++) //for + tab 2번
            {
                g.DrawLine(pen,
                    mgn, mgn + i*눈, mgn + 18 * 눈, mgn + i * 눈); //가로
                g.DrawLine(pen,
                    mgn+i*눈, mgn, mgn + i * 눈, mgn + 18 * 눈); //세로
            }

            //화점(눈금의 3, 9, 15 위치에 원을 채워 그린다)
            for (int x = 3; x <= 15; x+=6)
            {
                for (int y = 3; y <= 15; y+=6)
                {
                    //원은 사각형으로 그리는데
                    //사각형은 시작점과 폭, 높이로 지정
                    g.FillEllipse(bBrush, mgn + x * 눈-화점/2, 
                        mgn + y * 눈-화점/2, 화점, 화점);
                }
            }
        }


        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (reviveFlag == true)
            {
                ReviveGame();
                return;
            }
            //마우스 좌표를 자료구조 좌표로 변환하자

            int x = (e.X - mgn + 눈 / 2) / 눈;
            int y = (e.Y - mgn + 눈 / 2) / 눈;

            if (바둑판[x, y] != STONE.none)
                return;

            Rectangle r = new Rectangle(mgn+x*눈-돌/2, mgn + y * 눈-돌/2, 돌, 돌);

            if(flag == false)
            {
                if(imageFlag == false)
                    g.FillEllipse(bBrush, r);
                else
                {
                    Bitmap bmp = new Bitmap("../../images/black.png");
                    g.DrawImage(bmp, r);
                }
                lstRevive.Add(new Revive(x,y, STONE.black, stoneCnt));
                DrawStoneSequence(stoneCnt++, Brushes.White, r);

                바둑판[x, y] = STONE.black;
                flag = true;
            }

            else
            {
                if (imageFlag == false)
                    g.FillEllipse(wBrush, r);
                else
                {
                    Bitmap bmp = new Bitmap("../../images/white.png");
                    g.DrawImage(bmp, r);
                }

                lstRevive.Add(new Revive(x, y, STONE.white, stoneCnt));
                DrawStoneSequence(stoneCnt++, Brushes.Black, r);
                바둑판[x, y] = STONE.white;
                flag = false;

                
            }
            CheckOmok(x, y);
        }

        private void ReviveGame()
        {
           // if (stoneCnt < lstRevive.Count)
           foreach(var item in lstRevive)
            {
                DrawAStone(item);
            }
               // DrawAStone(lstRevive[stoneCnt++]);
        }

        private void DrawAStone(Revive item)
        {
            int x = item.X;
            int y = item.Y;
            STONE s = item.Stone;
            int seq = item.Seq;

            Rectangle r = new Rectangle(mgn + 눈 * x - 돌 / 2,
              mgn + 눈 * y - 돌 / 2, 돌, 돌);

            if (s == STONE.black)
            {
                if (imageFlag == false)
                    g.FillEllipse(bBrush, r);
                else
                {
                    Bitmap bmp = new Bitmap("../../Images/black.png");
                    g.DrawImage(bmp, r);
                }
                DrawStoneSequence(seq, Brushes.White, r);
                바둑판[x, y] = STONE.black;
            }
            else
            {
                if (imageFlag == false)
                    g.FillEllipse(wBrush, r);
                else
                {
                    Bitmap bmp = new Bitmap("../../Images/white.png");
                    g.DrawImage(bmp, r);
                }
                DrawStoneSequence(seq, Brushes.Black, r);
                바둑판[x, y] = STONE.white;
            }
            CheckOmok(x, y);
        }

        //수순표시 메서드
        private void DrawStoneSequence(int v, Brush color, Rectangle r)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(v.ToString(), font, color, r, stringFormat);
        }

        private void CheckOmok(int x, int y)
        {
            int cnt = 1;
            //우방향
            for (int i = x + 1; i < 19; i++)
            {
                if (바둑판[x, y] == 바둑판[i, y])
                    cnt++;
                else
                    break;
            }
            //좌방향
            for (int i = x - 1; i >= 0; i--)
            {
                if (바둑판[x, y] == 바둑판[i, y])
                    cnt++;
                else
                    break;
            }
            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
            cnt = 1;
            //위방향
            for (int i = y - 1; i >= 0; i--)
            {
                if (바둑판[x, y] == 바둑판[x, i])
                    cnt++;
                else
                    break;
            }
            //아래방향
            for (int i = y + 1; i < 19; i++)
            {
                if (바둑판[x, y] == 바둑판[x, i])
                    cnt++;
                else
                    break;
            }
            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
            //대각선 방향 
            for (int i = x + 1, j = y - 1; i < 19 && j >= 0; i++, j--)
            {
                if (바둑판[x, y] == 바둑판[i, j])
                    cnt++;
                else
                    break;
            }
            for (int i = x - 1, j = y + 1; i >= 0 && j < 19; i--, j++)
            {
                if (바둑판[x, y] == 바둑판[i, j])
                    cnt++;
                else
                    break;
            }
            //역대각선 방향
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (바둑판[x, y] == 바둑판[i, j])
                    cnt++;
                else
                    break;
            }

            for (int i = x + 1, j = y + 1; i <= 18 && j <= 18; i++, j++)
                if (바둑판[x, y] == 바둑판[i, j])
                    cnt++;
                else
                    break;

            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
        }

        private void OmokComplete(int x, int y)
        {
            SaveGame();
            MessageBox.Show(바둑판[x, y].ToString().ToUpper() + " Wins!!");
        }

        private void SaveGame()
        {
            if(reviveFlag == true)  // 복기모드에서는 저장하지 않습니다.
                return;

            //윈도우의 "문서" 경로
            string documentPath =
              Path.Combine(Environment.ExpandEnvironmentVariables
              ("%userprofile%"), "Documents").ToString();
            dirName = documentPath + "/Omok/"; //경로에 오목을 추가

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            string fileName = dirName + DateTime.Now.ToShortDateString() //날짜
              + "-" + DateTime.Now.Hour + DateTime.Now.Minute + ".omk"; //시간과 분
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);


            foreach (Revive item in lstRevive)
            {
                sw.WriteLine("{0} {1} {2} {3}",
                   item.X, item.Y, item.Stone, item.Seq);
            }
            sw.Close();
            fs.Close();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void 이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageFlag = true;
        }

        private void 그리기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageFlag = false;
        }

        private void 복기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reviveFlag = true;
   
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = dirName;
            ofd.Filter = "Omok files(*.omk)|*.omk";
            ofd.ShowDialog();
            string fileName = ofd.FileName;
            //sequenceFlag = true;

            try
            {
                StreamReader r = File.OpenText(fileName);
                string line = "";

                // 파일내용을 한줄씩 읽어서 lstRevive 리스트에 넣는다
                while ((line = r.ReadLine()) != null)
                {
                    string[] items = line.Split(' ');
                    Revive rev = new Revive(
                        int.Parse(items[0]), 
                        int.Parse(items[1]),
                      items[2] == "black" ? STONE.black : STONE.white,
                         int.Parse(items[3]));
                    lstRevive.Add(rev);
                }
                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // 복기준비
            reviveFlag = true;
            stoneCnt = 1;
            NewGame();
            stoneCnt = 0;
        }

        //새로 게임을 시작
        private void NewGame()
        {
            flag = false;
            for (int x = 0; x < 19; x++)
            {
                for (int y = 0; y < 19; y++)
                {
                    바둑판[x, y] = STONE.none;
                }
            }
            panel1.Refresh(); //패널을 지우고
            DrawBoard(); //다시 그린다
        }
    }
}
