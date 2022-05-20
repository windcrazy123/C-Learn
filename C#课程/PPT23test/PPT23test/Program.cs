using System;

namespace lasthomework
{
    interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
        void Speek();
        void Work();
    }
    class Student : IPerson
    {
        public string Name { get; set; }
        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (age > 0 && age < 120)
                {
                    age = value;
                }
            }
        }
        public void Speek()
        {
            Console.WriteLine(Name + ":老师好");
        }
        public void Work()
        {
            Console.WriteLine(Name + "同学开始记笔记");
        }
    }
    class Teacher : IPerson
    {
        public string Name { get; set; }
        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (age > 0 && age < 120)
                {
                    age = value;
                }
            }
        }
        public void Speek()
        {
            Console.WriteLine(Name + "：同学们好");
        }
        public void Work()
        {
            Console.WriteLine(Name + "老师开始上课");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IPerson[] person = new IPerson[] { new Student(), new Teacher() };
            person[0].Name = "peter";
            person[0].Age = 20;
            person[1].Name = "mike";
            person[1].Age = 40;
            person[0].Speek();
            person[1].Speek();
            Console.WriteLine();
            person[1].Work();
            person[0].Work();
            Console.ReadLine();


        }
    }
}
