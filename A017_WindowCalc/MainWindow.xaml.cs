using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A017_WindowCalc
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool opFlag;
        private double saved;
        private string op;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string num = btn.Content.ToString();
            if (txtResult.Text == "0" || opFlag == true)
            {
                txtResult.Text = num;
                opFlag = false;
            }
            else
                txtResult.Text += num;
        }

        // 소수점
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (txtResult.Text.Contains("."))
                return; //아무것도 안한다
            else
                txtResult.Text += ".";
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            double v = double.Parse(txtResult.Text);
            txtResult.Text = (-v).ToString();
        }

        //사칙연산자 처리(+, -, *, /)
        private void Op_Click(object sender, RoutedEventArgs e)
        {
            //1번처리
            opFlag = true;
            //2번처리
            saved = double.Parse(txtResult.Text);

            //3번처리
            Button btn = sender as Button;
            string s = btn.Content.ToString();

            switch(s)
            {
                case "+":
                    op = "+";
                    break;
                case "-":
                    op = "-";
                    break;
                case "×":
                    op = "*";
                    break;
                case "÷":
                    op = "/";
                    break;
            }
            //4번처리 txtExp에 수식표시
            txtExp.Text = txtResult.Text + " " + s;
        }
        // = 클릭되면 saved를 불러와서 + 5 ==> txtResult에 표시
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            double v = double.Parse(txtResult.Text);
            switch(op)
            {
                case "+":
                    txtResult.Text = (saved + v).ToString();
                    break;
                case "-":
                    txtResult.Text = (saved - v).ToString();
                    break;
                case "*":
                    txtResult.Text = (saved * v).ToString();
                    break;
                case "/":
                    txtResult.Text = (saved / v).ToString();
                    break;
            }
            txtExp.Text = "";
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
        }

        //모든것을 초기화한다
        private void C_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
            txtExp.Text = "";
            saved = 0;
            op = "";
            opFlag = false;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            string s = txtResult.Text;
            s = s.Remove(s.Length - 1); //끝 인덱스부터 지워준다(F1눌러서 찾아보기)
            //리턴하는 값이기 떄문에 반환값을 받아야한다 따라서 s가 필요
            if (s == "")
                s = "0";
            txtResult.Text = s;
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            if(txtExp.Text == "")
                txtExp.Text = "√(" + txtResult.Text + ")";
            else
                txtExp.Text = "√(" + txtExp.Text + ")";
            double x = Math.Sqrt(double.Parse(txtResult.Text));
            txtResult.Text = x.ToString();
        }

        private void Sqr_Click(object sender, RoutedEventArgs e)
        {
            if (txtExp.Text == "")
                txtExp.Text = "sqr(" + txtResult.Text + ")";
            else
                txtExp.Text = "sqr(" + txtExp.Text + ")";
            double x = double.Parse(txtResult.Text);
            txtResult.Text = (x*x).ToString();
        }

        private void Reci_Click(object sender, RoutedEventArgs e)
        {
            if (txtExp.Text == "")
                txtExp.Text = "1/(" + txtResult.Text + ")";
            else
                txtExp.Text = "1/(" + txtExp.Text + ")";
            double x = double.Parse(txtResult.Text);
            txtResult.Text = (1/x).ToString();
        }
    }
}
