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
//            Console.WriteLine("Box��Ķ���");
//            Box box = new Box(1, 2, 3);
//            Console.WriteLine(box.Height);
//            //�������ֱȽ�
//            Console.WriteLine(1.CompareTo(2));//-1
//            Console.WriteLine(3.CompareTo(1));//1
//            Console.WriteLine(1.CompareTo(1));//0
//            Console.WriteLine("����Box������бȽ�");
//            Box box2 = new Box(1, 2, 0);
//            Console.WriteLine(box.CompareTo(box2));

//            Console.WriteLine("ʹ����BoxComp");
//            BoxComp bc = new BoxComp();
//            Console.WriteLine(bc.Compare(box, box2));

//            Console.WriteLine("ʹ��List");
//            List<Box> boxes = new List<Box>();
//            boxes.Add(new Box(1, 3, 30));
//            boxes.Add(new Box(2, 2, 300));
//            boxes.Add(new Box(3, 1, 0));
//            Console.WriteLine("û��������б�");
//            Console.WriteLine("Height\tLength\tWidth");
//            foreach (Box item in boxes)
//            {  //����дvar item,����дBox item

//                Console.WriteLine("{0}\t{1}\t{2}", item.Height.ToString(), item.Length, item.Width);
//                //����дToString,Ҳ���Բ�д
//            }
//            boxes.Sort(new BoxLengthFirst());//���û�мӲ�����������ʱ�����쳣��
//                                             //���ԣ�BoxLengthFirst���þ��������

//            Console.WriteLine("ʹ����BoxLengthFirst����");
//            foreach (Box item in boxes)
//            {  //����дvar item,����дBox item

//                Console.WriteLine("{0}\t{1}\t{2}", item.Height.ToString(), item.Length, item.Width);
//                //����дToString,Ҳ���Բ�д
//            }
//            Console.WriteLine("ʹ��Comparer<Box>�����࣬�����˳��󷽷�Compare");
//            BoxLengthFirst bf = new BoxLengthFirst();
//            Console.WriteLine(bf.Compare(box, box2));

//            Console.WriteLine("ʹ�ø߶�����");
//            Comparer<Box> defComp = Comparer<Box>.Default;
//            boxes.Sort();   //���Boxû��д�̳У��ᷢ���쳣
//            foreach (Box item in boxes)
//            {  //����дvar item,����дBox item

//                Console.WriteLine("{0}\t{1}\t{2}", item.Height.ToString(), item.Length, item.Width);
//                //����дToString,Ҳ���Բ�д
//            }
//        }
//    }
//    class Box : IComparable<Box>
//    {//Ҫд�̳У���������ʹ��Sort������//�����Ĭ�ϵ�����Ч����

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
//            { //����߶Ȳ��ȣ��ͷ��ظ߶ȵĲ���
//                return this.Height.CompareTo(other.Height);
//            }
//            else if (this.Length.CompareTo(other.Length) != 0)
//            { //����߶�����ˣ������ȣ�������Ȳ���
//                return this.Length.CompareTo(other.Length);
//            }
//            else if (this.Width.CompareTo(other.Width) != 0)
//            {
//                return this.Width.CompareTo(other.Width);
//            }
//            else
//            {
//                return 0;//�������߶�һ����
//            }


//        }//����һ��Box�����бȽ�
//         //��ΪBox��3�����ԣ��ȱȽ�Height,�ٱȽ�Length,�ٱȽ�Width

//    }//end class

//    class BoxComp
//    {
//        public int Compare(Box x, Box y)
//        {
//            return x.CompareTo(y);
//        }//end Compare
//    }//end class
//    //�����Ķ��󣬿�����ΪSort�Ĳ���
//    class BoxLengthFirst : Comparer<Box>
//    {  //��Comparer<Box>���涨���˳����Compare����
//        public override int Compare(Box x, Box y)  //�ȱȽ�Length
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
/// ѧ����
/// </summary>
public class Student
{
    private string name;
    // ����
    public string Name { get { return name; } set { name = value; } }
    private int age;
    // ����
    public int Age { get { return age; } set { age = value; } }
    private string grade;
    // �꼶
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
    //���캯��
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
//���Ŷ���һ�����ڱȽϵ��࣬ʵ��IComparer<T>���ͽӿڣ�
public class StudentComparer : IComparer<Student>
{
    public enum CompareType
    {
        Name, Age, Grade
    }
    private CompareType type;
    // ���캯��������type��ֵ���жϰ��ĸ��ֶ�����
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
        Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-cn");//���ռ���ƴ������
        List<Student> arr = new List<Student>
        {
            new Student("����", 7, "һ�꼶"),
            new Student("����", 11, "���꼶"),
            new Student("����", 21, "һ�꼶"),
            new Student("����", 8, "���꼶"),
            new Student("����", 15, "���꼶")
        };
        // ����Sort������ʵ�ְ��꼶����
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Grade));
        // ѭ����ʾ�������Ԫ��
        foreach (Student item in arr)
            Console.WriteLine(item.ToString());
        // ����Sort������ʵ�ְ���������
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Name));
        // ѭ����ʾ�������Ԫ��
        foreach (Student item in arr)
            Console.WriteLine(item.ToString());
    }
}