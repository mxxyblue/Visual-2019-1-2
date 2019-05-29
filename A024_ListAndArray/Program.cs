using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A024_ListAndArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            /*int a = r.Next(); // 정수의 범위까지 랜덤하게 만든다(32비트)
            int b = r.Next(100); //0~99
            int c = r.Next(10, 20); // 10~20 // 3가지 방법 중에서 하나 사용한다
            double d = r.NextDouble(); //

            Console.WriteLine("{0}, {1}, {2}, {3}", a, b, c, d);

            string t = string.Format("{0, 10}, {1}, {2}, {3:F3}", a, b, c, d); // 이 방법이 가장 일반적
            //{,10} 은 10자리만 출력해주겠다는 의미, {:F3} 는 소수점 3번째자리까지 출력, {3,10:F3} //10자리할당, 소수점 3자리출력'
            //{:N0} 3자리마다 끊어서 출력해준다
            Console.WriteLine(t);*/

            /*for(int i = 0; i<10; i++)
            {
                a = r.Next(); 
                b = r.Next(100); 
                c = r.Next(10, 20); 
                d = r.NextDouble(); 

                t = string.Format("{0,10}, {1,10}, {2,10}, {3,10}", a, b, c, d);
                Console.WriteLine(t);
            }*/

            // 두 개의 주사위를 100번 던져서 숫자의 합이 얼마인지 출력하라

            /*for(int i = 0; i<100; i++)
            {

                //Console.WriteLine("{0}", r.Next(1,7) + r.Next(1,7));

                int x = r.Next(1, 7);
                int y = r.Next(1, 7);

                int sum = x+y;
                string t = string.Format("{0}", sum);
                Console.WriteLine(t);
            }*/

            // 두 개의 주사위를 1000000번(백만번) 던져서 각각의 합을 몇 번씩 나왔는지 출력하시오.
            // hint ! 배열을 사용하라
            //[ 출력 ]
            // 2 : 12345
            // 3 : 135424 (...)

            //배열이 필요하다 [13]개짜리
            //배열의 좋은 점 : 인덱스를 쓸 수 있다(for문과 맞아 돌아간다)


            int [] a = new int[13]; //[13]개짜리 배열 생성

            for(int i=0; i<1000000; i++)
            {
                a[r.Next(1, 7) + r.Next(1, 7)]++;
            }

           
            for (int i = 2; i<13; i++)
            {
                Console.WriteLine("{0,2}  :  {1}", i, a[i]);
            }

            Console.WriteLine("foreach array");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

            //리스트의 좋은 점 : 크기가 정해져 있지않다
            //리스트 :  Generic <T> List<T> (어떤 타입이던지 가능)

            List<int> lst = new List<int>();
            for(int i = 0; i<10; i++)
            {
                lst.Add(r.Next(100));
            }

            //리스트를 출력할 땐 foreach를 쓰는게 더 낫다
            foreach(var item in lst) //var은 int와 같은 역할을 한다, lst에 있는 item 각각에 대해서, 개수 알 필요가 없다
                Console.WriteLine(item);

            Console.WriteLine("for list");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(lst[i]); //마치 배열처럼 출력
            }

            
            //10개짜리 문자열 배열 s1과 문자열 리스트 s2를 만드시오

            string[] s1 = new string[10];
            List<string> s2 = new List<string>();

            //문자열 10개를 콘솔에서 입력받아서 리스트에 집어넣기

            Console.WriteLine("문자열 10개를 입력하세요");
            for (int i = 0; i < 10; i++)
            {
                string s = Console.ReadLine();

                //s2.Add(Console.ReadLine());
                s2.Add(s);
                s1[i] = s;
            }
            
            //문자열 10개를 출력
            for(int i = 0; i<10; i++)
            {
                Console.WriteLine("{0, 20} {1, 20}", s1[i], s2[i]);
            }

            //정렬하여 출력하기
            Array.Sort(s1); //배열
            s2.Sort(); //리스트

            Console.WriteLine("배열과 리스트 정렬 후 출력");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0, 20} {1, 20}", s1[i], s2[i]);
            }

        }
    }
}
