#define A
#define B
#if !A
#elif A != B
#else

#endif
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Tools;

namespace ConsoleApp1
{
    class Program
    {
        public delegate int Calc(int a, int b);
        static void Main(string[] args)
        {
            {//double test1 = Calculator1.Div(2.3, 4.6);

                //Calculator calculator = new Calculator();
                //Func<int, int, int> func = new Func<int, int, int>(calculator.Add);
                //Calc calc = new Calc(calculator.Add);

                //int x = 100;
                //int y = 200;
                //int z = 0;

                //z = calc(x, y);
                //Console.WriteLine(z);
                //z = func(x, y);
                //Console.WriteLine(z);

                //ProductFactory productFactory = new ProductFactory();
                //WrapFactory wrapFactory = new WrapFactory();
                //Func<Product> func1 = new Func<Product>(productFactory.MakePizza);
                //Logger logger = new Logger();
                //Action<Product> action = new Action<Product>(logger.Log);

                //Box box = wrapFactory.WrapProduct(func1, action);

                //Console.WriteLine(box.Product.Name);

                //Thread thread = new Thread(new ThreadStart(calculator.Report));
                //thread.Start();

                //Task task = new Task(new Action(calculator.Report));
                //task.Start();
            }
            

            //yield  JumpStaement
            {
                HelloCollection helloCollection = new HelloCollection();
                foreach (string s in helloCollection)
                {
                    Console.WriteLine(s);
                }
            }
            //try catch
            {/*
                try
                {
                    //执行的代码，其中可能有异常。一旦发现异常，则立即跳到catch执行。否则不会执行catch里面的内容
                }
                catch
                {
                    //除非try里面执行代码发生了异常，否则这里的代码不会执行
                }
                finally
                {
                    //不管什么情况都会执行，包括try catch 里面用了return ,可以理解为只要执行了try或者catch，就一定会执行 finally
                }
            */
            }

            //using语句
            try
            {
                // 使用using
                using (MyDispose md = new MyDispose())
                {
                    md.DoWork();
                    // 抛出一个异常来测试using
                    throw new Exception("抛出一个异常");
                }
            }
            catch
            {
            }
            finally
            {
                Console.WriteLine("Good Bye!");
            }

            //静态构造函数和（实例）构造函数区别
            {//A p = new A();
             //A q = new A();
             //静态构造函数主要用于初始化一些静态的变量。
             //静态构造函数只会执行一次，而且是在创建此类的第一个实例 或 引用任何静态成员（包括静态方法）之前，由.NET自动调用。
             //不冲突可同时出现
                B t = new B();
            }

            //《abstract和override算式表达式建模》使用 Expression 类，对于不同的 x 和 y 值，计算表达式 x * (y + 2) 的值
            {
                Expression e = new Operation(
              new VariableReference("x"),
              '*',
              new Operation(
                  new VariableReference("y"),
                  '+',
                  new Constant(2)
                  )
              );
                Hashtable vars = new Hashtable();
                vars["x"] = 3;
                vars["y"] = 5;
                Console.WriteLine(e.Evaluate(vars));        // Outputs "21"
                vars["x"] = 1.5;
                vars["y"] = 9;
                Console.WriteLine(e.Evaluate(vars));		// Outputs "16.5"

            }

            //索引器（Indexer）
            {
                var stringCollection = new SampleCollection<string>();
                stringCollection[0] = "Hello, World";
                Console.WriteLine(stringCollection[0]);
            }

            //运算符 (operator)
            {
                List<int> a = new List<int>();
                a.Add(1);
                a.Add(2);
                List<int> b = new List<int>();
                b.Add(1);
                b.Add(2);
                Console.WriteLine(a == b);      // Outputs "True" 
                b.Add(3);
                Console.WriteLine(a == b);      // Outputs "False"
                /*第一个 Console.WriteLine 输出 True，原因是两个列表包含的对象数目、对象顺序和对象值都相同。
                如果 List<T> 未定义 operator ==，则第一个 Console.WriteLine 将输出 False，
                原因是 a 和 b 引用的是不同的 List<int> 实例*/
            }

            //protected修饰符
            {/*P p = new P();
            P p1 = new O();
            O o = new O();*/
            }



            
        }
    }
    class Calculator
    {
        public void Report()
        {
            Console.WriteLine("I have 3 methods.");
        }
        public int Add(int a, int b)
        {
            int result = a + b;
            return result;
        }
        public int Sub(int a, int b)
        {
            int result = a - b;
            return result;
        }
    }
    class Logger
    {
        public void Log(Product product)
        {
            Console.WriteLine("{0},{1}", product.Name, product.Price);
        }
    }
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
    class Box
    {
        public Product Product { get; set; }
    }
    class WrapFactory
    {
        public Box WrapProduct(Func<Product> getProduct, Action<Product> logback)
        {
            Box box = new Box();
            Product product = getProduct();
            box.Product = product;
            if (product.Price >= 50)
            {
                logback(product);
            }
            return box;
        }
    }
    class ProductFactory
    {
        public Product MakePizza()
        {
            Product product = new Product();
            product.Name = "Pizza";
            product.Price = 100;
            return product;
        }
        public Product MakeToy()
        {
            Product product = new Product();
            product.Name = "Toy";
            product.Price = 30;
            return product;
        }
    }

    //yield JumpStatement
    public class HelloCollection : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return "Hello";
            yield return "World";
            /*如果你只想让用户访问ARRAY的前8个数据,这时将会用到yield break
            for (int i = 0; i < 10; i++)
            {
                if (i < 8)
                {
                    yield return array[i];
                }
                else
                {
                    yield break;
                }
            }*/
        }
    }
    /*被注释掉的代码相当于上面119~126
    public class HelloCollection : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Enumerator enumerator = new Enumerator(0);
            return enumerator;
        }
        public class Enumerator : IEnumerator, IDisposable
        {
            private int state;
            private object current;
            public Enumerator(int state)
            {
                this.state = state;
            }
            public bool MoveNext()
            {
                switch (state)
                {
                    case 0:
                        current = "Hello";
                        state = 1;
                        return true;
                    case 1:
                        current = "World";
                        state = 2;
                        return true;
                    case 2:
                        break;
                }
                return false;
            }
            public void Reset()
            {
                throw new NotSupportedException();
            }
            public object Current
            {
                get { return current; }
            }
            public void Dispose()
            {
            }
        }
    }*/

    //using语句不仅免除了程序员输入Dispose调用的代码，它还提供了机制保证Dispose方法被调用，无论using语句块顺利执行结束，还是抛出了一个异常
    //继承自IDisposable接口，仅仅用来做测试，不使用任何非托管资源
    public class MyDispose : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose方法被调用");
        }
        public void DoWork()
        {
            Console.WriteLine("做了很多工作");
        }
    }
    /*    //注意一点使用using时常犯的错误，那就是千万不要试图在using语句块外初始化对象 ，如下面代码所示：
        MyDispose md = new MyDispose();
        using (md)
        {
            md.DoWork();
        }
    */
    /*事实上，C#编译器为using语句自动添加了try/finally块，所以Dispose方法能够保证被调用到，所以如下两段代码经过编译后内容将完全一致：
        using (MyDispose md = new MyDispose())
        {
              md.DoWork();
        }
    和
        MyDispose md;
        try
        {
            md = new MyDispose();
            md.DoWork();
        }
        finally
        {
            md.Dispose();
        }*/

    //当约束是一个泛型类型参数时，它就叫无类型约束(Naked type constraints)。当一个有类型参数成员方法，要把它的参数约束为其所在类的类型参数时，无类型约束很有用
    public class Pair<T>
    {
        public T First;
        void Add<U>(Pair<U> items) where U : T
        {

        }
    }

    //静态构造函数和（实例）构造函数区别
    class A
    {
        public A()
        {
            Console.WriteLine("public A");
        }
        static A()
        {
            Console.WriteLine("static A");
        }
        public int high = 100;
        public string name = "Jarry";
    }
    class B : A
    {
        public B()
        {
            Console.WriteLine("public B");
        }
        static B()
        {
            Console.WriteLine("static B");
        }
    }

    //抽象 (abstract) 方法是没有实现的虚方法.和override抽象方法，《可用来算术表达式建模》
    public abstract class Expression
    {
        public abstract double Evaluate(Hashtable vars);
    }
    public class Constant : Expression//Constant 的 Evaluate 实现只是返回所存储的常量
    {
        double value;
        public Constant(double value)
        {
            this.value = value;
        }
        public override double Evaluate(Hashtable vars)
        {
            return value;
        }
    }
    public class VariableReference : Expression//VariableReference 的实现在哈希表中查找变量名称，并返回产生的值。
    {
        string name;
        public VariableReference(string name)
        {
            this.name = name;
        }
        public override double Evaluate(Hashtable vars)
        {
            object value = vars[name];
            if (value == null)
            {
                throw new Exception("Unknown variable: " + name);
            }
            return Convert.ToDouble(value);
        }
    }
    public class Operation : Expression//Operation 的实现先对左操作数和右操作数求值（通过递归调用它们的 Evaluate 方法），然后执行给定的算术运算
    {
        Expression left;
        char op;
        Expression right;
        public Operation(Expression left, char op, Expression right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }
        public override double Evaluate(Hashtable vars)
        {
            double x = left.Evaluate(vars);
            double y = right.Evaluate(vars);
            switch (op)
            {
                case '+': return x + y;
                case '-': return x - y;
                case '*': return x * y;
                case '/': return x / y;
            }
            throw new Exception("Unknown operator");
        }
    }

    //索引器（Indexer）
    class SampleCollection<T>
    {
        // Declare an array to store the data elements.
        private T[] arr = new T[100];

        // Define the indexer to allow client code to use [] notation.
        public T this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
    }

    //运算符 (operator) 是一种类成员，它定义了可应用于类实例的特定表达式运算符的含义。
    //可以定义三类运算符：一元运算符、二元运算符和转换运算符。
    //所有运算符都必须声明为 public 和 static。
    //List<T> 类声明了两个运算符 operator == 和 operator !=，从而为将那些运算符应用于 List 实例的表达式赋予了新的含义
    public class List<T>
    {
        T[] items;
        int count = 0;
        public List()
        {
            items = new T[4];
        }
        public void Add(T item)
        {
            items[count] = item;
            count++;
        }
        public override bool Equals(object other)
        {
            return Equals(this, other as List<T>);
        }
        static bool Equals(List<T> a, List<T> b)
        {
            if (a == null) return b == null;
            if (b == null || a.count != b.count) return false;
            for (int i = 0; i < a.count; i++)
            {
                if (!object.Equals(a.items[i], b.items[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool operator ==(List<T> a, List<T> b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(List<T> a, List<T> b)
        {
            return !Equals(a, b);
        }
        //具体而言，上述运算符将两个 List<T> 实例的相等关系定义为逐一比较其中所包含的对象(使用所包含对象的 Equals 方法)
    }

    //如果 M 为 protected，则当访问发生在声明了 M 的类中，或发生在从声明 M 的类派生的类中并通过派生类类型进行访问时，允许进行访问
    class P
    {
        protected int a;
        protected int x;
        private void U()
        {
            this.a = 20;
        }
        static void F(P a, O b)
        {
            a.x = 1;        // Ok
            b.x = 1;        // Ok
        }
    }
    class O : P
    {
        private void I()
        {
            P p = new P();
            P p1 = new O();
            O o = new O();
            o.a = 10;
        }
        static void F(P a, O b)
        {
            a.x = 1;        // Error, must access through instance of B
            b.x = 1;        // Ok
        }
        /*在 P 中可以通过 P 和 O 的实例访问 x，
         * 这是因为在两种情况下访问都通过 P 的实例或从 P 派生的类发生。
         * 但是在 O 中，由于 P 不从 O 派生，所以不可能通过 P 的实例访问 x*/
    }


    //
    class MyClass<T> where T : Int32
    {
        T t;
        void F()
        {
            t = new T;
        }
    }

}
