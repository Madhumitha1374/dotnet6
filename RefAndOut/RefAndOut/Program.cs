using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefAndOut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            RefExample example = new RefExample();
            Console.WriteLine("Previous value of integer i:" + i.ToString());
            string test = example.GetNextName(ref i);
            Console.WriteLine("Current value of integer i:" + i.ToString());
            Console.ReadLine();


            //int j = 0;
            //OutExample example2 = new OutExample();
            //Console.WriteLine("Previous value of integer j:" + j.ToString());
            //string test1 = example2.GetNextNameByOut(out j);
            //Console.WriteLine("Current value of integer j:" + j.ToString());
        }
    }
}
