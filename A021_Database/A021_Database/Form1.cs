using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //데이터베이스 쓰려면 추가해줘야 함

namespace A021_Database
{
    public partial class Form1 : Form
    {
        OleDbConnection conn = null; //객체생성 아직 하지 않음
        OleDbCommand comm = null;
        OleDbDataReader reader = null;

        string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;
Data Source=E:\School\2학년\C#\WindowsFormsApp1\A021_Database\A021_Database\Students.mdb;
Persist Security Info=False;";

        // ../../은 디버그 두칸 위에 있는 폴더에 파일이 있다는 것을 의미한다
        //@ = 특수문자가 아니라는 의미(그대로 쓴다)
        
        public Form1()
        {
            InitializeComponent();
            DisplayStudents();

        }

        private void DisplayStudents()
        {
            ConnectionOpen();

            //명령어 : 모든 레코드를 가져와라
            //명령어는 SQL로 만든다
            string sql = "SELECT * FROM StudentTable";
            comm = new OleDbCommand(sql, conn);

            ReadAndAddToListBox();

            reader.Close(); //리더를 반드시 닫아줘야한다
            ConnectionClose();
        }

        //DB의 Connection을 열어주는 메소드
        private void ConnectionOpen()
        {
            if (conn == null) //연결되어있지 않다면
            {
                conn = new OleDbConnection(connString); //연결해주고
                conn.Open(); //열기
            }
        }

        private void ConnectionClose()
        {
            conn.Close();
            conn = null;
        }

        //Reader 에서 DB의 값을 읽어와 ListBox애 표시
        private void ReadAndAddToListBox()
        {
            //명령어 실행
            reader = comm.ExecuteReader(); //executereader : db에서 값 읽어오는 메소드

            //DB에서 읽어오는 여러개의 레코드
            while (reader.Read()) //read하는 동안
            {
                string x = "";
                x += reader["ID"] + "\t"; //필드에서 가져온다음에 탭으로 띄워준다
                x += reader["SID"] + "\t";
                x += reader["SName"] + "\t";
                x += reader["Phone"];

                listBox1.Items.Add(x); //리스트박스에 추가한다

            }
        }

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;

            if(lb.SelectedItem == null)
                return;

            string[] s = lb.SelectedItem.ToString().Split('\t'); //탭문자로 쪼개준다
            txtID.Text = s[0];
            txtSld.Text = s[1];
            txtSName.Text = s[2];
            txtPhone.Text = s[3];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtSld.Text == "" || txtSName.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show(
                    "학번, 이름, 전화번호는 반드시 입력해야합니다.",
                    "추가에러");
                return;
            }
            ConnectionOpen();

            string sql = string.Format(
                "INSERT INTO StudentTable(SId, SName, Phone) Values({0},'{1}','{2}')",
                txtSld.Text, txtSName.Text, txtPhone.Text );

            MessageBox.Show(sql); //test

            comm = new OleDbCommand(sql, conn);
            if (comm.ExecuteNonQuery() == 1) //삽입하라고 하는 명령 후 성공 여부를 확인한다. 숫자는 변경된 레코드의 수
                MessageBox.Show("추가성공");
            ConnectionClose();

            listBox1.Items.Clear(); //리스트박스에 있는 데이터를 지워준다
            DisplayStudents(); 

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSld.Text == null && txtSName.Text == null && txtPhone.Text == null)
            {
                MessageBox.Show("학번, 이름, 전화번호 중 하나를 입력해야합니다.", "검색에러");
                return;
            }

            ConnectionOpen();

            string sql = "";
            if (txtSld.Text != "")

                sql = string.Format("SELECT * FROM StudentTable WHERE SId = {0}", txtSld.Text);

            else if (txtSName.Text != "")
                sql = string.Format("SELECT * FROM StudentTable WHERE SName = '{0}'", txtSName.Text);

            else if (txtPhone.Text != "")
                sql = string.Format("SELECT * FROM StudentTable WHERE SName = '{0}'", txtPhone.Text);

            comm = new OleDbCommand(sql, conn);

            listBox1.Items.Clear();
            ReadAndAddToListBox();

            ConnectionClose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (txtSld.Text == "" || txtSName.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show(
                    "삭제할 레코드를 선택해야 합니다.",
                    "삭제에러");
                return;
            }

            ConnectionOpen();

            string sql = string.Format(
                "DELETE FROM StudentTable WHERE ID ={0}",
                txtID.Text);

            MessageBox.Show(sql); //test

            comm = new OleDbCommand(sql, conn);
            if (comm.ExecuteNonQuery() == 1) //명령 후 성공 여부를 확인한다. 숫자는 변경된 레코드의 수
                MessageBox.Show("삭제 성공!");
            ConnectionClose();

            listBox1.Items.Clear(); //리스트박스에 있는 데이터를 지워준다
            DisplayStudents();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtID == null)
            {
                MessageBox.Show(
                    "수정할 레코드를 먼저 선택해야 합니다",
                    "수정에러");
                return;
            }
            ConnectionOpen();

            string sql = string.Format(
                "UPDATE StudentTable SET SId = {0}, SName='{1}', Phone = '{2}' WHERE ID = {3}",
                txtSld.Text, txtSName.Text, txtPhone.Text, txtID.Text);

            MessageBox.Show(sql); //test

            comm = new OleDbCommand(sql, conn);
            if (comm.ExecuteNonQuery() == 1) //명령 후 성공 여부를 확인한다. 숫자는 변경된 레코드의 수
                MessageBox.Show("수정성공");
            ConnectionClose();

            listBox1.Items.Clear(); //리스트박스에 있는 데이터를 지워준다
            DisplayStudents();

        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DisplayStudents();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtPhone.Text = "";
            txtSld.Text = "";
            txtSName.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
