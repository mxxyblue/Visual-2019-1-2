using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A018_ClassBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            //클래스를 사용하자
            //클래스의 객체(개체, object, instance)를 만들어서 사용한다
            Car x = new Car();
            x.SetInTime();
            //...
            x.SetOutTime();
            //x.SetCarColor(1);
            x.CarColor = 1;

            //Math는 static 클래스 -> 객체를 만들지 않고 클래스 이름으로 사용(정적메소드)
            Console.WriteLine(Math.Sin(Math.PI));
        }
    }

    class Car
    {
        private int carNumber; //변수는 소문자로 시작, 낙타체사용
        DateTime inTime;
        DateTime outTime;
        public int CarColor { set; get; } //속성 - 대문자로 시작
        //private int carColor; 

        public void SetInTime()
        {
            this.inTime = DateTime.Now;
        }
        public void SetOutTime()
        {
            this.outTime = DateTime.Now;
        }
        /*public void SetCarColor(int color)
        {
            carColor = color;
        }*/
    }

}
