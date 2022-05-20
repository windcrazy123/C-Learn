using System;

namespace 异或
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vs = { 1, 1, 2, 2,3, 4, 4, 5 };
            PrintOddTimeNum printOddTimeNum = new PrintOddTimeNum();
            printOddTimeNum.printOddTimeNum2(vs);
        }
    }
    class PrintOddTimeNum
    {
        //数组中只有一个数出现基数次
        public void printOddTimeNum1(int[] arr)
        {
            int eor = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                eor ^= arr[i];
            }
            //eor = 出现基数次的数
            Console.WriteLine(eor);
            //eor!=0
            //eor必然有一个位置上是1
            //int rightOne = eor & (~eor + 1);//提取出最右的1
        }

        //有两个数出现基数次
        public void printOddTimeNum2(int[] arr)
        {
            int eor = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                eor ^= arr[i];
            }
            //eor = a^b
            //eor!=0
            //eor必然有一个位置上是1
            int rightOne = eor & (~eor + 1);//提取出最右的1
            int onlyOne = 0;//eor'
            for (int i = 0; i < arr.Length; i++)
            {
                if ((arr[i] & rightOne) == 0)
                {
                    onlyOne ^= arr[i];
                }
            }
            Console.WriteLine(onlyOne + " " + (eor ^ onlyOne));
        }
    }
}
