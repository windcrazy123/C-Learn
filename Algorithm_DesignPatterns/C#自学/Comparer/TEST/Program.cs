using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

//namespace list_example2
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Box类的定义");
//            Box box = new Box(1, 2, 3);
//            Console.WriteLine(box.Height);
//            //单个数字比较
//            Console.WriteLine(1.CompareTo(2));//-1
//            Console.WriteLine(3.CompareTo(1));//1
//            Console.WriteLine(1.CompareTo(1));//0
//            Console.WriteLine("两个Box对象进行比较");
//            Box box2 = new Box(1, 2, 0);
//            Console.WriteLine(box.CompareTo(box2));

//            Console.WriteLine("使用类BoxComp");
//            BoxComp bc = new BoxComp();
//            Console.WriteLine(bc.Compare(box, box2));

//            Console.WriteLine("使用List");
//            List<Box> boxes = new List<Box>();
//            boxes.Add(new Box(1, 3, 30));
//            boxes.Add(new Box(2, 2, 300));
//            boxes.Add(new Box(3, 1, 0));
//            Console.WriteLine("没有排序的列表");
//            Console.WriteLine("Height\tLength\tWidth");
//            foreach (Box item in boxes)
//            {  //可以写var item,可以写Box item

//                Console.WriteLine("{0}\t{1}\t{2}", item.Height.ToString(), item.Length, item.Width);
//                //可以写ToString,也可以不写
//            }
//            boxes.Sort(new BoxLengthFirst());//如果没有加参数，就运行时发生异常。
//                                             //所以，BoxLengthFirst作用就是在这里。

//            Console.WriteLine("使用了BoxLengthFirst排序");
//            foreach (Box item in boxes)
//            {  //可以写var item,可以写Box item

//                Console.WriteLine("{0}\t{1}\t{2}", item.Height.ToString(), item.Length, item.Width);
//                //可以写ToString,也可以不写
//            }
//            Console.WriteLine("使用Comparer<Box>的子类，定义了抽象方法Compare");
//            BoxLengthFirst bf = new BoxLengthFirst();
//            Console.WriteLine(bf.Compare(box, box2));

//            Console.WriteLine("使用高度优先");
//            Comparer<Box> defComp = Comparer<Box>.Default;
//            boxes.Sort();   //如果Box没有写继承，会发生异常
//            foreach (Box item in boxes)
//            {  //可以写var item,可以写Box item

//                Console.WriteLine("{0}\t{1}\t{2}", item.Height.ToString(), item.Length, item.Width);
//                //可以写ToString,也可以不写
//            }
//        }
//    }
//    class Box : IComparable<Box>
//    {//要写继承，才能正常使用Sort方法。//这个是默认的排序效果。

//        public Box(int h, int l, int w)
//        {
//            this.Height = h;
//            this.Length = l;
//            this.Width = w;
//        }
//        public int Height { get; private set; }
//        public int Length { get; private set; }
//        public int Width { get; private set; }

//        public int CompareTo(Box other)
//        {
//            if (this.Height.CompareTo(other.Height) != 0)
//            { //如果高度不等，就返回高度的差异
//                return this.Height.CompareTo(other.Height);
//            }
//            else if (this.Length.CompareTo(other.Length) != 0)
//            { //如果高度相等了，看长度，如果长度不等
//                return this.Length.CompareTo(other.Length);
//            }
//            else if (this.Width.CompareTo(other.Width) != 0)
//            {
//                return this.Width.CompareTo(other.Width);
//            }
//            else
//            {
//                return 0;//长，宽，高都一样。
//            }


//        }//传入一个Box，进行比较
//         //因为Box有3个属性，先比较Height,再比较Length,再比较Width

//    }//end class

//    class BoxComp
//    {
//        public int Compare(Box x, Box y)
//        {
//            return x.CompareTo(y);
//        }//end Compare
//    }//end class
//    //这个类的对象，可以作为Sort的参数
//    class BoxLengthFirst : Comparer<Box>
//    {  //在Comparer<Box>里面定义了抽象的Compare方法
//        public override int Compare(Box x, Box y)  //先比较Length
//        {
//            if (x.Length.CompareTo(y.Length) != 0)
//            {
//                return x.Length.CompareTo(y.Length);
//            }
//            else if (x.Height.CompareTo(y.Height) != 0)
//            {
//                return x.Height.CompareTo(y.Height);
//            }
//            else if (x.Width.CompareTo(y.Width) != 0)
//            {
//                return x.Width.CompareTo(y.Width);
//            }
//            else
//            {
//                return 0;
//            }
//        }
//    }//end class
//}

/// <summary>
/// 学生类
/// </summary>
public class Student
{
    private string name;
    // 姓名
    public string Name { get { return name; } set { name = value; } }
    private int age;
    // 年龄
    public int Age { get { return age; } set { age = value; } }
    private string grade;
    // 年级
    public string Grade
    {
        get
        {
            return grade;
        }
        set
        {
            grade = value;
        }
    }
    //构造函数
    public Student(string name, int age, string grade)
    {
        this.name = name;
        this.age = age;
        this.grade = grade;
    }
    public override string ToString()
    {
        return this.name + "," + this.age.ToString() + "," + this.grade;
    }
}
//接着定义一个用于比较的类，实现IComparer<T>泛型接口：
public class StudentComparer : IComparer<Student>
{
    public enum CompareType
    {
        Name, Age, Grade
    }
    private CompareType type;
    // 构造函数，根据type的值，判断按哪个字段排序
    public StudentComparer(CompareType type)
    {
        this.type = type;
    }
    public int Compare(Student x, Student y)
    {
        /*switch (type)
        {
            case CompareType.Name:
                return x.Name.CompareTo(y.Name);
            case CompareType.Age:
                return x.Age.CompareTo(y.Age);
            case CompareType.Grade:
                return x.Grade.CompareTo(y.Grade);
            default:
                return 0;
        }*/
        return type switch
        {
            CompareType.Name => x.Name.CompareTo(y.Name),
            CompareType.Age => x.Age.CompareTo(y.Age),
            CompareType.Grade => x.Grade.CompareTo(y.Grade),
            _ => 0,
        };
    }
}
public class Test
{
    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-cn");//按照简中拼音排序
        List<Student> arr = new List<Student>
        {
            new Student("张三", 7, "一年级"),
            new Student("李四", 11, "二年级"),
            new Student("王五", 21, "一年级"),
            new Student("陈六", 8, "三年级"),
            new Student("刘七", 15, "二年级")
        };
        // 调用Sort方法，实现按年级排序
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Grade));
        // 循环显示集合里的元素
        foreach (Student item in arr)
            Console.WriteLine(item.ToString());
        // 调用Sort方法，实现按姓名排序
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Name));
        // 循环显示集合里的元素
        foreach (Student item in arr)
            Console.WriteLine(item.ToString());
    }
}