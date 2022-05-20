using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DesignPatterns
{
	//单例
	namespace Singleton
	{//确保类只有一个实例，并提供全局访问点
		public class Mgr01
		{/*饿汉式
         * 类加载到内存后就实例化一个单例,保证线程安全，class加载一次静态方法只加载一次拿多少次都是这一个
         * 唯一缺点：不论用到与否，类装载时就完成实例化
         * （但是不用还装载干啥）
         */
			private static readonly Mgr01 Instance = new Mgr01();
			private Mgr01() { }//私有构造
			public static Mgr01 GetInstance() { return Instance; }
		}
		public class Mgr02
		{//比第一好一些，加载类不会加载内部类
			private Mgr02() { }
			private static class Mgr02Holder
			{
				internal static readonly Mgr02 Instance = new Mgr02();
			}
			public static Mgr02 GetInstance() {
				return Mgr02Holder.Instance;
			}
		}
		public class Mgr03
		{
			private static Mgr03 Instance;
			private Mgr03() { }
			public static Mgr03 GetInstance() {//双重检查
				if (Instance == null) {
					lock (Mgr03.Instance)//保证线程安全
					{
						if (Instance == null) {
							Instance = new Mgr03();
						}
					}
				}
				return Instance;
			}
		}
	}
	//策略
	namespace Strategy
	{
		//引子:比较器弱化版
		#region
		public interface Comparable<T>
		{
			public int CompareTo(T c);
		}
		//例：排序
		public class Sorter01<T>
		{
			public static void Sort(Comparable<T>[] arr) {
				for (int i = 0; i < arr.Length - 1; i++) {
					int min = i;
					for (int j = 0; j < arr.Length; j++) {
						min = arr[j].CompareTo((T)arr[min]) == -1 ? j : min;
					}
					swap(arr, i, min);
				}
			}
			static void swap(Comparable<T>[] arr, int i, int j) {
				Comparable<T> temp = arr[i];
				arr[i] = arr[j];
				arr[j] = temp;
			}
		}
		//有一个dog类
		public class Dog : Comparable<Dog>
		{
			int food;
			int weight;
			public Dog(int food, int weight) {
				this.food = food;
				this.weight = weight;
			}
			public int CompareTo(Dog d) {
				if (this.food < d.food) return -1;
				else if (this.food > d.food) return 1;
				else return 0;
			}
		}
		#endregion

		//Fire(FireStrategy f)每次调用，都需要new，因此把strategy做成单例（singleton）;或者成员变量如FireStrategy f = new DefualtStrategy()这个无法动态改变策略;(看需求)
		//进入比较器:加比较策略，使sorter复用（开闭原则对修改关闭，对扩展开放）
		#region
		public interface Compartor<T>
		{
			int compare(T o1, T o2);
		}
		public class DogFoodCompartor : Compartor<DogB>
		{
			public int compare(DogB o1, DogB o2) {
				if (o1.food > o2.food) return -1;
				else if (o1.food < o2.food) return 1;
				else return 0;
			}
		}
		public class DogWeightCompartor : Compartor<DogB>
		{
			public int compare(DogB o1, DogB o2) {
				if (o1.weight > o2.weight) return -1;
				else if (o1.weight < o2.weight) return 1;
				else return 0;
			}
		}

		public class Sorter02<T>
		{
			public static void Sort(T[] arr, Compartor<T> compartor) {
				for (int i = 0; i < arr.Length - 1; i++) {
					int min = i;
					for (int j = 0; j < arr.Length; j++) {
						min = compartor.compare(arr[j], arr[min]) == -1 ? j : min;
					}
					swap(arr, i, min);
				}
			}

			static void swap(T[] arr, int i, int j) {
				T temp = arr[i];
				arr[i] = arr[j];
				arr[j] = temp;
			}
		}
		public class DogB
		{
			public int food;
			public int weight;
			public DogB(int food, int weight) {
				this.food = food;
				this.weight = weight;
			}
		}

		#endregion

		//策略模式--定义算法族，分别封装起来，让他们之间可以互相替换，此模式让算法的变化独立于使用算法的用户
		#region
		public interface Flyable
		{
			public void Fly();
		}
		public class FlyWithWings : Flyable
		{
			public void Fly() {
				Console.WriteLine("I have wings, I can fly");
			}
		}
		public class FlyNoway : Flyable
		{
			public void Fly() {
				Console.WriteLine("nothing");
			}
		}
		public class FlyRocketPowered : Flyable
		{
			public void Fly() {
				Console.WriteLine("I'm flying with a rocket");
			}
		}

		public interface Quackable
		{
			public void Quack();
		}
		public class Quack : Quackable
		{
			void Quackable.Quack() {
				Console.WriteLine("I'm duck guaguagua");
			}
		}
		public class Squeak : Quackable
		{
			public void Quack() {
				Console.WriteLine("I'm rubber duck zhizhizhi");
			}
		}
		public class MuteQuack : Quackable
		{
			public void Quack() {
				Console.WriteLine("nothing");
			}
		}

		public abstract class Duck
		{
			public Flyable f;
			public Quackable q;
			public abstract void Display();
			public void Swim() {
				Console.WriteLine("I can swim");
			}
			public void PerformQuack() {
				q.Quack();
			}
			public void PerformFly() {
				f.Fly();
			}
			public void SetFlyBehavior(Flyable fb) {
				f = fb;
			}
			public void SetQuackBehavior(Quackable qb) {
				q = qb;
			}
		}
		public class MallardDuck : Duck
		{
			public MallardDuck() {
				f = new FlyWithWings();
				q = new Quack();
			}
			public override void Display() {
				Console.WriteLine("I'm really mallard duck");
			}
		}
		public class ModelDuck : Duck
		{
			public ModelDuck() {
				f = new FlyNoway();
				q = new Quack();
			}
			public override void Display() {
				Console.WriteLine("I'm model duck");
			}
		}
		/*   main
        public class DuckSimulator
            {
                public static void Main()
                {
                    Duck model = new ModelDuck();
                    model.PerformFly();
                    model.SetFlyBehavior(new FlyRocketPowered());//可以动态的改变行为
                    model.PerformFly();
                }
            }*/
		#endregion

		//作业：TankFire (strategy & singleton)
		#region
		public interface FireStrategy
		{
			public void Strategy(Tank t);
		}
		public class FireStrategy01 : FireStrategy
		{
			private FireStrategy01() { }
			static readonly FireStrategy01 fireStrategy01 = new FireStrategy01();
			public static FireStrategy01 GetFireStrategy01() { return fireStrategy01; }
			public void Strategy(Tank t) {
				Console.WriteLine("defulte");
			}
		}
		public class FireStrategy02 : FireStrategy
		{
			private FireStrategy02() { }
			static readonly FireStrategy02 fireStrategy02 = new FireStrategy02();
			public static FireStrategy02 GetFireStrategy02() { return fireStrategy02; }
			public void Strategy(Tank t) {
				Console.WriteLine("different direction");
			}
		}
		public class FireStrategy03 : FireStrategy
		{
			private FireStrategy03() { }
			static readonly FireStrategy03 fireStrategy03 = new FireStrategy03();
			public static FireStrategy03 GetFireStrategy03() { return fireStrategy03; }
			public void Strategy(Tank t) {
				Console.WriteLine("nuclear");
			}
		}

		public class Tank
		{
			public void Fire(FireStrategy f) {
				f.Strategy(this);
			}
		}
		#endregion
	}
	//工厂（编程习惯)
	namespace AbstractFactory
	{
		#region
		//抽象工厂
		public abstract class AbstractFactory
		{
			public abstract Food CreateFood();
			public abstract Vehicle CreateVehicle();
			public abstract Weapon CreateWeapon();
		}
		//具体工厂:两个产品族（产品族的维度容易扩展，产品种类数量不易扩展）
		public class ModernFactory : AbstractFactory
		{
			public override Food CreateFood() {
				return new Bread();
			}

			public override Vehicle CreateVehicle() {
				return new Car();
			}

			public override Weapon CreateWeapon() {
				return new AK47();
			}
		}
		public class MagicFactory : AbstractFactory
		{
			public override Food CreateFood() {
				return new MushRoom();
			}

			public override Vehicle CreateVehicle() {
				return new Broom();
			}

			public override Weapon CreateWeapon() {
				return new MagicStick();
			}
		}
		//抽象产品
		public abstract class Food
		{
			public abstract void PrintName();
		}
		public abstract class Weapon
		{
			public abstract void Shoot();
		}
		public abstract class Vehicle
		{
			public abstract void Go();
		}
		//具体产品
		public class AK47 : Weapon
		{
			public override void Shoot() {
				Console.WriteLine("shoot");
			}
		}
		public class MagicStick : Weapon
		{
			public override void Shoot() {
				Console.WriteLine("stick");
			}
		}
		public class Bread : Food
		{
			public override void PrintName() {
				Console.WriteLine("bread");
			}
		}
		public class MushRoom : Food
		{
			public override void PrintName() {
				Console.WriteLine("mushroom");
			}
		}
		public class Car : Vehicle
		{
			public override void Go() {
				Console.WriteLine("car");
			}
		}
		public class Broom : Vehicle
		{
			public override void Go() {
				Console.WriteLine("broom");
			}
		}
		class Program
		{
			static void M(string[] args)//main
			{
				AbstractFactory factory = new ModernFactory();
				//AbstractFactory f = new MagicFactory();

				Weapon a = factory.CreateWeapon();
				Vehicle b = factory.CreateVehicle();
				Food f = factory.CreateFood();
			}
		}
		#endregion
		//工厂方法模式
		#region
		//简单工厂
		public class SimplePizzaFactory//工厂
		{
			public Pizza CreatePizza(String type) {
				Pizza pizza = null;
				//根据type给pizza赋值
				return pizza;
			}
		}
		public class PizzaStore
		{
			SimplePizzaFactory factory;
			public PizzaStore(SimplePizzaFactory factory) {
				this.factory = factory;
			}
			public Pizza OrderPizza(String type) {
				Pizza pizza;
				pizza = factory.CreatePizza(type);
				//调用pizza方法对pizza处理
				return pizza;
			}
		}
		//public class Test1
		//{
		//    static void Main()
		//    {
		//        SimplePizzaFactory factory = new SimplePizzaFactory();
		//        PizzaStore simplestore = new PizzaStore(factory);
		//        simplestore.OrderPizza("type");
		//    }
		//}
		#endregion
		//工厂方法模式定义了一个创建对象的接口，但由子类决定要实例化的类是哪一个。
		//工厂方法让类把实例化推迟到子类
		//下方Pizzastore2提供了一个创建对象的方法的接口，也称为“工厂方法”
		//工厂方法和创建者不一定总是抽象的，可以定义一个默认的工厂方法来产具体产品
		public abstract class PizzaStore2
		{
			public Pizza OrderPizza(string type) {
				Pizza pizza;
				pizza = CreatePizza(type);
				//处理
				return pizza;
			}//传参可以enum这样就限定了
			protected abstract Pizza CreatePizza(string type);//工厂方法//工厂经常只产生一种对象不需要参数
		}
		//public class NewPizzaStore : PizzaStore2
		//{
		//    protected override Pizza CreatePizza(string type)
		//    {
		//        //根据type值返回不同的Pizza子类
		//        return null;
		//    }
		//}//改为下方抽象工厂模式后的store。
		public class NewPizzaStore2 : PizzaStore2
		{
			protected override Pizza CreatePizza(string type) {
				Pizza pizza = null;
				PizzaIngredientFactory ingredientFactory = new NewPizzaIngredientFactory();//
				if (type.Equals("newStyle")) {
					pizza = new NewStylePizza(ingredientFactory);
					pizza.SetName("New Style Pizza");
				}//else if...
				return pizza;
			}
		}
		public abstract class Pizza
		{
			protected string name;
			//protected string dough;//生面团
			//protected string sauce;//酱
			protected Dough dough;
			protected Sauce sauce;
			//由于抽象原料工厂一套佐料分开为单个类protected ArrayList toppings = new ArrayList();//一套佐料
			//protected void Prepare() { //...
			//}由于抽象工厂改为下方方法
			public abstract void Prepare();
			protected void Bake() { //...
			}
			//...
			public string SetName(string name) {
				this.name = name;
				return this.name;
			}
			public string GetName() {
				return name;
			}
		}
		public class NewStylePizza : Pizza
		{
			//public NewStylePizza()
			//{
			//    //name = "...";
			//    //dough = "...";
			//    //sauce = "...";
			//    //toppings.Add("a b c");
			//}
			//可以覆盖父类方法达到不同做披萨的方法
			//尽管最好不要覆盖基类中已实现的方法但也只是最好，可以直接在基类中设计为抽象函数如果有这个需求
			//由于抽象工厂改为下方
			PizzaIngredientFactory ingredientFactory;
			public NewStylePizza(PizzaIngredientFactory ingredientFactory) {
				this.ingredientFactory = ingredientFactory;
			}
			public override void Prepare() {
				Console.WriteLine("Preparing " + name);
				dough = ingredientFactory.CreateDough();
				sauce = ingredientFactory.CreateSauce();
			}
		}
		//public class Test2
		//{
		//    static void Main()
		//    {
		//        PizzaStore2 store2 = new NewPizzaStore();

		//        Pizza pizza = store2.OrderPizza("type");
		//        Console.WriteLine(pizza.GetName());
		//    }
		//}

		/*抽象工厂模式
         提供一个接口，用于创建相关或依赖对象的家族，而不需要明确指定具体类
        抽象工厂的方法经常以工厂方法的方式实现
        工厂方法用的是继承而抽象工厂通过对象的组合*/
		#region
		public abstract class Dough { }
		public abstract class Sauce { }
		public class NewDough : Dough { }
		public class NewSauce : Sauce { }
		public interface PizzaIngredientFactory
		{
			public Dough CreateDough();
			public Sauce CreateSauce();
			//...
			//每个原料都是一个新的类
		}
		public class NewPizzaIngredientFactory : PizzaIngredientFactory
		{
			public Dough CreateDough() {
				return new NewDough();
			}
			public Sauce CreateSauce() {
				return new NewSauce();
			}
		}
		#endregion
	}
	//责任链//Chain of Responsibility
	namespace COR
	{
		public class App
		{
			//public static void Main(String[] args)
			//{
			//    Request request = new Request();
			//    request.SetData("test");
			//    Process(request);
			//}
			//模拟请求处理全过程
			private static Response Process(Request request) {
				Response response = new Response();
				FilterChain chain = new FilterChain();
				chain.AddFilter(new ParameterValidateFilter());
				chain.AddFilter(new LogFilter());
				chain.DoFilter(request, response, chain);
				return response;
			}
		}

		public class Request
		{
			//请求数据
			private String data;
			public String GetData() { return data; }
			public void SetData(String data) { this.data = data; }
		}

		public class Response
		{
			//响应数据
			private String data;
			public String GetData() { return data; }
			public void SetData(String data) { this.data = data; }
		}

		public interface Filter
		{
			//过滤器
			public void DoFilter(Request request, Response response, FilterChain chain);
		}

		//参数校验过滤器
		public class ParameterValidateFilter : Filter
		{
			public void DoFilter(Request request, Response response, FilterChain chain) {
				if (request != null && request.GetData() != null && request.GetData().Contains("test")) {
					chain.DoFilter(request, response, chain);
				} else {
					response.SetData("处理失败");
				}
			}
		}

		//日志过滤器
		public class LogFilter : Filter
		{
			public void DoFilter(Request request, Response response, FilterChain chain) {
				Console.WriteLine("————————开始处理——————————");//模拟日志
				chain.DoFilter(request, response, chain);
				Console.WriteLine("————————处理结束——————————");//模拟日志
			}
		}

		//过滤器链【执行责任链模式的主要成员】
		public class FilterChain : Filter
		{
			//过滤器列表
			private List<Filter> filters = new List<Filter>();
			private int index = 0;
			//添加过滤器
			public void AddFilter(Filter filter) {
				filters.Add(filter);
			}
			public void DoFilter(Request request, Response response, FilterChain chain) {
				if (index == filters.Count) {
					// 真正处理请求
					response.SetData("处理成功");
					return;
				}
				Filter filter = filters[index];
				index++;
				filter.DoFilter(request, response, chain);
			}
		}
	}
	//观察者（Observer）
	namespace Observer
	{
		//观察者
		//observer listener hook callback都是一个东西
		#region
		//事件源对象
		public class Child
		{
			private bool cry;
			private List<Observer1> observers = new List<Observer1>();
			public Child() {
				observers.Add(new Dad());
			}
			public bool IsCry() { return cry; }
			public void WakeUp() {
				cry = true;
				WakeUpEvent aEvent = new WakeUpEvent(1, "bed", this);
				foreach (Observer1 observer in observers) {
					observer.ActionOnWakeUp(aEvent);
				}
			}
		}
		//事件
		public abstract class Event<T>
		{
			public abstract T GetSource();
		}
		public class WakeUpEvent : Event<Child>
		{
			long timestamp;
			string loc;
			Child source;
			public WakeUpEvent(long timestamp, string loc, Child source) {
				this.timestamp = timestamp;
				this.loc = loc;
				this.source = source;
			}
			public override Child GetSource() { return source; }
		}
		//observer
		public interface Observer1
		{
			public void ActionOnWakeUp(WakeUpEvent event1);
		}
		public class Dad : Observer1
		{
			public void Feed() { Console.WriteLine(".."); }
			public void ActionOnWakeUp(WakeUpEvent event1) {//可以获得该事件包含的信息
				Feed();
			}
		}
		#endregion
		//观察者模式：定义了对象之间的一对多依赖，这样一来，当一个对象改变状态时，他的所有依赖者都会收到通知并自动更新
		//设计原则：为了交互对象之间的松耦合设计而努力
		public interface Subject
		{
			public void RegisterObserver(Observer o);
			public void RemoveObserver(Observer o);
			public void NotifyObservers();
		}
		public interface Observer
		{
			public void Update(float temp, float humidity, float pressure);
		}
		public interface DisplayElement
		{
			public void Display();
		}
		public class WeatherData : Subject
		{
			private ArrayList observers;
			private float temperature;
			private float humidity;
			private float pressure;
			public WeatherData() {
				observers = new ArrayList();
			}

			public void RegisterObserver(Observer o) {
				observers.Add(o);
			}

			public void RemoveObserver(Observer o) {
				int i = observers.IndexOf(o);
				if (i >= 0) {
					observers.Remove(i);
				}
			}

			public void NotifyObservers() {
				for (int i = 0; i < observers.Count; i++) {
					Observer observer = (Observer)observers[i];
					observer.Update(temperature, humidity, pressure);
				}
			}

			public void GetTemperature() {
				// TODO implement here
			}

			public void GetHumidity() {
				// TODO implement here
			}

			public void GetPressure() {
				// TODO implement here
			}

			public void MeasurementsChanged() {
				NotifyObservers();
			}

			public void SetMeasurements(float temperature, float humidity, float pressure) {
				this.temperature = temperature;
				this.humidity = humidity;
				this.pressure = pressure;
				MeasurementsChanged();
			}
		}
		public class CurrentConditionsDisplay : DisplayElement, Observer
		{
			private float temperature;
			private float humidity;
			private Subject weatherData;

			public CurrentConditionsDisplay(Subject weatherData) {
				this.weatherData = weatherData;
				weatherData.RegisterObserver(this);
			}

			public void Update(float temperature, float humidity, float pressure) {
				this.temperature = temperature;
				this.humidity = humidity;
				Display();
			}

			public void Display() {
				Console.WriteLine("Current conditions: " + temperature
				+ "F degrees and " + humidity + "% humidity");
			}

		}
		public class StatisticsDisplay : DisplayElement, Observer
		{
			public StatisticsDisplay(Subject weatherData) { }
			public void Update(float temperature, float humidity, float pressure) {
				// TODO implement here
			}
			public void Display() {
				// TODO implement here
			}
		}
		public class ForecastDisplay : DisplayElement, Observer
		{
			public ForecastDisplay(Subject weatherData) { }
			public void Update(float temperature, float humidity, float pressure) {
				// TODO implement here
			}
			public void Display() {
				// TODO implement here
			}
		}
		public class WeatherStation
		{
			//public static void Main()
			//{
			//    WeatherData weatherData = new WeatherData();

			//    CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
			//    StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
			//    ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
			//    //模拟新的气象测量
			//    weatherData.SetMeasurements(80, 65, 30.4f);
			//    weatherData.SetMeasurements(82, 70, 29.2f);
			//    weatherData.SetMeasurements(78, 90, 29.4f);
			//}
		}
	}
	//装饰者
	namespace Decorator
	{
		//装饰者模式：动态地将责任附加到对象上。
		//若要扩展功能，装饰者提供了比继承更有弹性的替代方案
		public abstract class Beverage//饮料//也可以将此component设计成接口,看需求
		{
			public static int TALL { get { return 1; } }//小杯
			public static int GRANDE { get { return 2; } }//中杯
			public static int VENTI { get { return 3; } }//大杯

			public String description = "Unknown Beverage";
			public String GetDescription() { return description; }
			public abstract double Cost();
		}
		//condiment n.调料
		//必须让CondimentDecorator能取代Beverage，所以扩展自Beverage类
		//利用继承达到“类型匹配”，目的不是利用继承获得“行为”
		public abstract class CondimentDecorator : Beverage
		{
			//必须重新实现
			new public abstract String GetDescription();
		}

		public class Espresso : Beverage//浓缩咖啡
		{
			public Espresso() { description = "Espresso"; }
			public override double Cost() { return 1.99; }
		}
		public class HouseBlend : Beverage//blend v.混合;n.混合物 houseblend综合咖啡
		{
			public HouseBlend() { description = "House Blend Coffee"; }
			public override double Cost() { return 0.89; }
		}
		public class DarkRoast : Beverage//深焙
		{
			public DarkRoast() { description = "DarkRoast"; }
			public override double Cost() { return 0.99; }
		}

		public class Mocha : CondimentDecorator
		{
			Beverage beverage;
			public Mocha(Beverage beverage) { this.beverage = beverage; }
			public override double Cost() {
				return 0.2 + beverage.Cost();
			}
			public override string GetDescription() {
				return beverage.GetDescription() + ", Mocha";
			}
		}
		public class Soy : CondimentDecorator
		{
			Beverage beverage;
			public Soy(Beverage beverage) { this.beverage = beverage; }
			public override double Cost() {
				return 0.15 + beverage.Cost();
			}
			public override string GetDescription() {
				return beverage.GetDescription() + ", Soy";
			}
		}
		public class Whip : CondimentDecorator
		{
			Beverage beverage;
			public Whip(Beverage beverage) { this.beverage = beverage; }
			public override double Cost() {
				return 0.1 + beverage.Cost();
			}
			public override string GetDescription() {
				return beverage.GetDescription() + ", Whip";
			}
		}
		//如果有小中大杯，且调料根据咖啡容量收费
		/*public class Milk : CondimentDecorator
        {
            Beverage beverage;
            public Milk(Beverage beverage) { this.beverage = beverage; }
            //要把getsize()传播到被包装的饮料因为所有的调料装饰者都会用到这个方法所以应该把他移到抽象类中。
            //饮料还要写一个setsize()方法
            public int GetSize() { return beverage.GetSize(); }
            public override double Cost()
            {
                double cost = beverage.Cost();
                if (GetSize() == Beverage.TALL){
                    cost += 0.1;
                }
                else if (GetSize() == Beverage.GRANDE){
                    cost += 0.15;
                }
                else if (GetSize() == Beverage.VENTI){
                    cost += 0.2;
                }
                return cost;
            }
            public override string GetDescription()
            {
                return beverage.GetDescription() + ", Milk";
            }
        }*/

		//Main
		public class StarbuzzCoffee
		{
			//public static void Main()
			//{
			//    Beverage beverage0 = new Espresso();
			//    Console.WriteLine(beverage0.GetDescription() 
			//        + " $" + beverage0.Cost());

			//    Beverage beverage1 = new DarkRoast();
			//    beverage1 = new Mocha(beverage1);
			//    beverage1 = new Mocha(beverage1);
			//    beverage1 = new Whip(beverage1);
			//    Console.WriteLine(beverage1.GetDescription()
			//        + " $" + beverage1.Cost());

			//    Beverage beverage2 = new HouseBlend();
			//    beverage2 = 
			//        new Soy(
			//            new Mocha(
			//                new Whip(beverage2)));
			//    Console.WriteLine(beverage2.GetDescription()
			//        + " $" + beverage2.Cost());
			//}
		}
	}
	//命令模式
	namespace Command
	{
		/*命令模式：将“请求”封装成对象，以便使用不同的请求、队列或者日志来参数化其他对象。
         命令模式也支持可撤销的操作*/
		//命令接口
		public interface ICommand
		{
			public void Execute();
			public void Undo();
		}
		public class NoCommand : ICommand
		{//作为替代品
		 //当不想返回或者没有一个有意义的对象时，空对象就很有用（null object）
		 //甚至有时候空对象本身也是一种设计模式
			public void Execute() { }

			public void Undo() {
				throw new NotImplementedException();
			}
		}
		//开灯命令
		public class LightOnCommand : ICommand
		{
			Light light;
			public LightOnCommand(Light light) {
				this.light = light;
			}
			public void Execute() {
				light.On();
			}
			public void Undo() {
				light.Off();
			}
		}
		public class LightOffCommand : ICommand
		{
			Light light;
			public LightOffCommand(Light light) {
				this.light = light;
			}
			public void Execute() {
				light.Off();
			}
			public void Undo() {
				light.On();
			}
		}
		public class GarageDoorUpCommand : ICommand
		{
			GarageDoor garageDoor;
			public GarageDoorUpCommand(GarageDoor garageDoor) {
				this.garageDoor = garageDoor;
			}
			public void Execute() {
				garageDoor.Up();
			}

			public void Undo() {
				throw new NotImplementedException();
			}
		}
		public class StereoOnWithCDCommand : ICommand
		{
			Stereo stereo;
			public StereoOnWithCDCommand(Stereo stereo) {
				this.stereo = stereo;
			}
			public void Execute() {
				stereo.On();
				stereo.SetCD();
				stereo.SetVolume(6);//音量
			}

			public void Undo() {
				throw new NotImplementedException();
			}
		}
		public class StereoOffCommand : ICommand
		{
			Stereo stereo;
			public StereoOffCommand(Stereo stereo) {
				this.stereo = stereo;
			}
			public void Execute() {
				stereo.Off();
			}

			public void Undo() {
				throw new NotImplementedException();
			}
		}
		public class CeilingFanHighCommand : ICommand
		{
			CeilingFan ceilingFan;
			int prevSpeed;

			public CeilingFanHighCommand(CeilingFan ceilingFan) {
				this.ceilingFan = ceilingFan;
			}
			public void Execute() {
				prevSpeed = ceilingFan.GetSpeed();
				ceilingFan.High();
			}
			public void Undo() {
				if (prevSpeed == CeilingFan.HIGH) {
					ceilingFan.High();
				} else if (prevSpeed == CeilingFan.MEDIUM) {
					ceilingFan.Medium();
				} else if (prevSpeed == CeilingFan.LOW) {
					ceilingFan.Low();
				} else if (prevSpeed == CeilingFan.OFF) {
					ceilingFan.Off();
				}
			}
		}
		public class CeilingFanMediumCommand : ICommand
		{
			CeilingFan ceilingFan;
			int prevSpeed;

			public CeilingFanMediumCommand(CeilingFan ceilingFan) {
				this.ceilingFan = ceilingFan;
			}
			public void Execute() {
				prevSpeed = ceilingFan.GetSpeed();
				ceilingFan.Medium();
			}
			public void Undo() {
				if (prevSpeed == CeilingFan.HIGH) {
					ceilingFan.High();
				} else if (prevSpeed == CeilingFan.MEDIUM) {
					ceilingFan.Medium();
				} else if (prevSpeed == CeilingFan.LOW) {
					ceilingFan.Low();
				} else if (prevSpeed == CeilingFan.OFF) {
					ceilingFan.Off();
				}
			}
		}
		public class CeilingFanOffCommand : ICommand
		{
			CeilingFan ceilingFan;
			int prevSpeed;

			public CeilingFanOffCommand(CeilingFan ceilingFan) {
				this.ceilingFan = ceilingFan;
			}
			public void Execute() {
				prevSpeed = ceilingFan.GetSpeed();
				ceilingFan.Off();
			}
			public void Undo() {
				if (prevSpeed == CeilingFan.HIGH) {
					ceilingFan.High();
				} else if (prevSpeed == CeilingFan.MEDIUM) {
					ceilingFan.Medium();
				} else if (prevSpeed == CeilingFan.LOW) {
					ceilingFan.Low();
				} else if (prevSpeed == CeilingFan.OFF) {
					ceilingFan.Off();
				}
			}
		}
		//宏命令
		public class MacroCommand : ICommand
		{
			ICommand[] commands;

			public MacroCommand(ICommand[] commands) {
				this.commands = commands;
			}
			public void Execute() {
				for (int i = 0; i < commands.Length; i++) {
					commands[i].Execute();
				}
			}
			public void Undo() {
				for (int i = 0; i < commands.Length; i++) {
					commands[i].Undo();
				}
			}
		}
		//使用命令对象
		public class SimpleRemoteControl
		{
			ICommand slot;
			public SimpleRemoteControl() { }
			public void SetCommand(ICommand command) {
				slot = command;
			}
			public void ButtonWasPressed() {
				slot.Execute();
			}
		}
		//实现遥控器
		public class RemoteControl
		{
			ICommand[] onCommands;
			ICommand[] offCommands;//处理多个开与关的命令
			public RemoteControl() {
				onCommands = new ICommand[7];
				offCommands = new ICommand[7];
				ICommand noCommand = new NoCommand();
				for (int i = 0; i < 7; i++) {
					onCommands[i] = noCommand;
					offCommands[i] = noCommand;
				}
			}
			public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) {
				onCommands[slot] = onCommand;
				offCommands[slot] = offCommand;
			}
			public void OnButtonWasPushed(int slot) {
				onCommands[slot].Execute();
			}
			public void OffButtonWasPushed(int slot) {
				offCommands[slot].Execute();
			}
			public override string ToString() {
				string str = "";
				for (int i = 0; i < onCommands.Length; i++) {
					str += "[slot " + i + "] " + onCommands[i].GetType()
						+ "   " + offCommands[i].GetType() + "\n";
				}
				return str;
			}
		}
		public class RemoteControlWithUndo
		{
			ICommand[] onCommands;
			ICommand[] offCommands;//处理多个开与关的命令
			ICommand undoCommand;//记录前一个命令
			public RemoteControlWithUndo() {
				onCommands = new ICommand[7];
				offCommands = new ICommand[7];
				ICommand noCommand = new NoCommand();
				for (int i = 0; i < 7; i++) {
					onCommands[i] = noCommand;
					offCommands[i] = noCommand;
				}
				undoCommand = noCommand;
			}
			public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) {
				onCommands[slot] = onCommand;
				offCommands[slot] = offCommand;
			}
			public void OnButtonWasPushed(int slot) {
				onCommands[slot].Execute();
				undoCommand = onCommands[slot];
			}
			public void OffButtonWasPushed(int slot) {
				offCommands[slot].Execute();
				undoCommand = offCommands[slot];
			}
			public void UndoButtonWasPushed() {
				undoCommand.Undo();
			}
			public override string ToString() {
				string str = "";
				for (int i = 0; i < onCommands.Length; i++) {
					str += "[slot " + i + "] " + onCommands[i].GetType()
						+ "   " + offCommands[i].GetType() + "\n";
				}
				return str;
			}
		}
		public class Light
		{
			string name;
			public Light(string name) {
				this.name = name;
			}
			public void On() { Console.WriteLine(name + " Light is On"); }
			public void Off() { Console.WriteLine(name + " Light is Off"); }
		}
		public class GarageDoor
		{
			string name;
			public GarageDoor(string name) {
				this.name = name;
			}
			public void Up() { Console.WriteLine(name + " The door is oping"); }
			public void Down() { Console.WriteLine(name + " The door is closing"); }
		}
		public class Stereo//音响
		{
			string name;
			public Stereo(string name) {
				this.name = name;
			}
			public void On() { Console.WriteLine(name + " stereo is open"); }
			public void Off() { Console.WriteLine(name + " stereo is close"); }
			public void SetCD() { Console.WriteLine(name + " set for CD input"); }
			public void SetDVD() { Console.WriteLine(name + " set for DVD input"); }
			public void SetVolume(int volume) { Console.WriteLine("volume set to " + volume); }
		}
		public class CeilingFan//吊扇
		{
			public static readonly int HIGH = 3;
			public static readonly int MEDIUM = 2;
			public static readonly int LOW = 1;
			public static readonly int OFF = 0;
			string location;
			int speed;

			public CeilingFan(string location) {
				this.location = location;
				speed = OFF;
			}
			public void High() { speed = HIGH; Console.WriteLine(location + " ceiling fan is on high"); }
			public void Medium() { speed = MEDIUM; Console.WriteLine(location + " ceiling fan is on medium"); }
			public void Low() { speed = LOW; Console.WriteLine(location + " ceiling fan is on low"); }
			public void Off() { speed = OFF; Console.WriteLine(location + " ceiling fan is off"); }
			public int GetSpeed() { return speed; }
		}
		/*public class Test1
        {
            static void Main()
            {
                SimpleRemoteControl remote = new SimpleRemoteControl();//调用者会传入命令对象发出请求
                Light light = new Light();//请求接收者
                GarageDoor garageDoor = new GarageDoor();
                LightOnCommand lightOn = new LightOnCommand(light);//创建命令将接收者传给他
                GarageDoorCommand garageUp = new GarageDoorCommand(garageDoor);

                remote.SetCommand(lightOn);//把命令传给调用者
                remote.ButtonWasPressed();
                remote.SetCommand(garageUp);
                remote.ButtonWasPressed();
            }
        }*/
		//public class Test2
		//{
		//    static void Main()
		//    {
		//        RemoteControl remoteControl = new RemoteControl();

		//        Light livingRoomLight = new Light("Living Room");
		//        Light kitchenLight = new Light("Kitchen");
		//        GarageDoor garageDoor = new GarageDoor("GarageDoor");
		//        Stereo stereo = new Stereo("LivingRoomStereo");

		//        LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
		//        LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
		//        LightOnCommand kitchenLightOn = new LightOnCommand(kitchenLight);
		//        LightOffCommand kitchenLightOff = new LightOffCommand(kitchenLight);

		//        GarageDoorUpCommand garageDoorUp = new GarageDoorUpCommand(garageDoor);

		//        StereoOnWithCDCommand stereoOnWithCD = new StereoOnWithCDCommand(stereo);
		//        StereoOffCommand stereoOff = new StereoOffCommand(stereo);

		//        remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
		//        remoteControl.SetCommand(1, kitchenLightOn, kitchenLightOff);
		//        remoteControl.SetCommand(2, stereoOnWithCD, stereoOff);
		//        Console.WriteLine(remoteControl);
		//        remoteControl.OnButtonWasPushed(0);
		//        remoteControl.OffButtonWasPushed(0);
		//        remoteControl.OnButtonWasPushed(1);
		//        remoteControl.OffButtonWasPushed(1);
		//        remoteControl.OnButtonWasPushed(2);
		//        remoteControl.OffButtonWasPushed(2);
		//    }
		//}
		/*public class Test3
        {
            static void Main()
            {
                RemoteControlWithUndo remoteControl = new RemoteControlWithUndo();

                CeilingFan ceilingFan = new CeilingFan("Living Room");
                CeilingFanHighCommand ceilingFanHigh = new CeilingFanHighCommand(ceilingFan);
                CeilingFanMediumCommand ceilingFanMedium = new CeilingFanMediumCommand(ceilingFan);
                CeilingFanOffCommand ceilingFanOff = new CeilingFanOffCommand(ceilingFan);

                remoteControl.SetCommand(0, ceilingFanMedium, ceilingFanOff);
                remoteControl.SetCommand(1, ceilingFanHigh, ceilingFanOff);

                remoteControl.OnButtonWasPushed(0);
                remoteControl.OffButtonWasPushed(0);
                Console.WriteLine(remoteControl);
                remoteControl.UndoButtonWasPushed();

                remoteControl.OnButtonWasPushed(1);
                Console.WriteLine(remoteControl);
                remoteControl.UndoButtonWasPushed();
            }
        }*/
		//public class MacroTest
		//{
		//    static void Main()
		//    {
		//        RemoteControlWithUndo remoteControl = new RemoteControlWithUndo();

		//        Light livingRoomLight = new Light("Living Room");
		//        Light kitchenLight = new Light("Kitchen");

		//        LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
		//        LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
		//        LightOnCommand kitchenLightOn = new LightOnCommand(kitchenLight);
		//        LightOffCommand kitchenLightOff = new LightOffCommand(kitchenLight);

		//        ICommand[] lightsOn = { livingRoomLightOn, kitchenLightOn };
		//        ICommand[] lightsOff = { livingRoomLightOff, kitchenLightOff };

		//        MacroCommand lightsOnMacro = new MacroCommand(lightsOn);
		//        MacroCommand lightsOffMacro = new MacroCommand(lightsOff);

		//        remoteControl.SetCommand(0, lightsOnMacro, lightsOffMacro);

		//        Console.WriteLine(remoteControl);
		//        Console.WriteLine("on");
		//        remoteControl.OnButtonWasPushed(0);
		//        Console.WriteLine("off");
		//        remoteControl.OffButtonWasPushed(0);
		//        Console.WriteLine("undo");
		//        remoteControl.UndoButtonWasPushed();
		//    }
		//}
	}
	//适配器模式
	namespace Adapter
	{
		/*适配器模式将一个类的接口， 转换成客户期望的另一个接口。
         * 适配器让接口不兼容的累可以合作无间。*/
		public interface Duck
		{
			public void Quack();
			public void Fly();
		}
		public class MallardDuck : Duck
		{
			public void Fly() {
				Console.WriteLine("Quack");
			}
			public void Quack() {
				Console.WriteLine("I'm flying");
			}
		}
		public interface Turkey
		{
			public void Gobble();
			public void Fly();
		}
		public class WildTurkey : Turkey
		{
			public void Fly() {
				Console.WriteLine("I'm flying a short distance");
			}
			public void Gobble() {
				Console.WriteLine("Gobble");
			}
		}
		public class TurkeyAdapter : Duck
		{
			Turkey turkey;
			public TurkeyAdapter(Turkey turkey) {
				this.turkey = turkey;
			}

			public void Fly() {
				for (int i = 0; i < 5; i++) {
					turkey.Fly();
				}
			}
			public void Quack() {
				turkey.Gobble();
			}
		}
		public class DuckTestDrive
		{
			//static void Main()
			//{
			//    MallardDuck duck = new MallardDuck();
			//    WildTurkey turkey = new WildTurkey();
			//    Duck turkeyAdapter = new TurkeyAdapter(turkey);
			//    Console.WriteLine("The Turkey");
			//    turkey.Gobble();
			//    turkey.Fly();
			//    Console.WriteLine("\nThe Duck");
			//    TestDuck(duck);
			//    Console.WriteLine("\nThe turkeyAdapter");
			//    TestDuck(turkeyAdapter);
			//}
			static void TestDuck(Duck duck) {
				duck.Quack();
				duck.Fly();
			}
		}
	}
	//外观模式
	namespace Facade
	{
		/*外观模式提供了一个统一的接口，用来访问子系统中的一群接口。
         * 外观定义了一个高层接口，让子系统更容易使用。*/
		public class HomeTheaterFacade
		{
			Amplifier amp;
			DvdPlayer dvd;
			CdPlayer cd;
			Projector projector;
			TheaterLights lights;
			Screen screen;
			PopcornPopper popper;

			public HomeTheaterFacade(Amplifier amp,
				DvdPlayer dvd,
				CdPlayer cd,
				Projector projector,
				TheaterLights lights,
				Screen screen,
				PopcornPopper popper) {
				this.amp = amp;
				this.dvd = dvd;
				this.cd = cd;
				this.projector = projector;
				this.lights = lights;
				this.screen = screen;
				this.popper = popper;
			}
			public void WatchMovie(string movie) {
				Console.WriteLine("get ready to watch a movie");
				popper.On();
				popper.Pop();
				lights.Dim(10);
				screen.Down();
				projector.On();
				projector.WideScreenMode();
				amp.On();
				amp.SetDvd(dvd);
				amp.SetSurroundSound();
				amp.SetVolume(5);
				dvd.On();
				dvd.Play(movie);
			}
			public void EndMovie() {
				Console.WriteLine("shutting movie theater down");
				popper.Off();
				lights.On();
				screen.Up();
				projector.Off();
				amp.Off();
				dvd.Stop();
				dvd.Eject();
				dvd.Off();
			}
		}
		public class Amplifier
		{
			public void On() { }
			public void SetDvd(DvdPlayer dvd) { }
			public void SetSurroundSound() { }
			public void SetVolume(int n) { }
			public void Off() { }
		}
		public class DvdPlayer
		{
			public void On() { }
			public void Play(string movie) { }
			public void Stop() { }
			public void Eject() { }
			public void Off() { }
		}
		public class CdPlayer { }
		public class Projector
		{
			public void On() { }
			public void WideScreenMode() { }
			public void Off() { }
		}
		public class TheaterLights
		{
			public void Dim(int n) { }
			public void On() { }
		}
		public class Screen
		{
			public void Down() { }
			public void Up() { }
		}
		public class PopcornPopper
		{
			public void On() { }
			public void Pop() { }
			public void Off() { }
		}
		//public class HomeTheaterTestDrive
		//{
		//    static void Main()
		//    {
		//        //先实例化组件
		//        HomeTheaterFacade homeTheater =
		//            new HomeTheaterFacade(amp, dvd, cd,
		//            projector, screen, lights, popper);
		//        homeTheater.WatchMovie("ARK");
		//        homeTheater.EndMovie();
		//    }
		//}
	}
	//模板方法模式
	namespace TemplateMethod
	{
		/*模板方法模式在一个方法中定义一个算法的骨架，而将一些步骤延迟到子类中。
         * 模板方法使得子类可以在不改变算法结构的情况下，重新定义算法中的某些步骤。*/
		public abstract class CaffeineBeverageWithHook
		{
			public void PrepareRecipe() {
				BoilWater();
				Brew();
				PourInCup();
				if (CustomerWantsCondiments())//加上一个if如果client要调料才调用add
				{
					AddCondiments();
				}
			}
			public abstract void Brew();//泡茶与煮咖啡具体不一样，到子类具体实现
			public abstract void AddCondiments();//同上
			public void BoilWater() { }//同一个步骤子类继承
			public void PourInCup() { }//同上
			public virtual bool CustomerWantsCondiments() { return true; }//钩子方法
		}
		/*钩子子类实现算法中可选的部分，或者在钩子对于子类的实现并不重要的时候，子类可以对此钩子置之不理。
         * 另一个用法是让子类能够有机会对模板方法中某些即将发生的（或刚刚发生的）步骤作出反应。
         * 正如此例，钩子也可以让子类有能力为其抽象类做一些决定。
         请记住，默写步骤是可选的所以你可以实现为钩子而不是实现成抽象方法，这样就可以让抽象类的子类的负荷减轻。*/
		public class CoffeeWithHook : CaffeineBeverageWithHook
		{
			public override void AddCondiments() {
				Console.WriteLine("Adding sugar and milk");
			}
			public override void Brew() {
				Console.WriteLine("dripping coffee through filter");
			}

			public override bool CustomerWantsCondiments()//覆盖钩子方法
			{
				string answer = GetUserInput();//根据用户输入对调料的决定
				if (answer.ToLower().StartsWith("y")) {
					return true;
				} else { return false; }
			}
			private string GetUserInput() {
				string answer = null;
				Console.WriteLine("would you like milk and sugar with your coffee(y/n)?");
				try {
					answer = Console.ReadLine();
				} catch (Exception) {
					Console.WriteLine("IO error tyring to read your answer");
				}
				if (answer == null) {
					return "no";
				}
				return answer;
			}
		}
		public class TeaWithHook : CaffeineBeverageWithHook
		{
			public override void AddCondiments() {
				Console.WriteLine("Adding lemon");
			}
			public override void Brew() {
				Console.WriteLine("brew tea");
			}

			public override bool CustomerWantsCondiments()//覆盖钩子方法
			{
				string answer = GetUserInput();//根据用户输入对调料的决定
				if (answer.ToLower().StartsWith("y")) {
					return true;
				} else { return false; }
			}
			private string GetUserInput() {
				string answer = null;
				Console.WriteLine("would you like lemon with your tea(y/n)?");
				try {
					answer = Console.ReadLine();
				} catch (Exception) {
					Console.WriteLine("IO error tyring to read your answer");
				}
				if (answer == null) {
					return "no";
				}
				return answer;
			}
		}
		public class BeverageTestDrive
		{
			//static void Main()
			//{
			//    TeaWithHook teaHook = new TeaWithHook();
			//    CoffeeWithHook coffeeHook = new CoffeeWithHook();
			//    Console.WriteLine("\nMaking tea");
			//    teaHook.PrepareRecipe();
			//    Console.WriteLine("\nMaking coffee");
			//    coffeeHook.PrepareRecipe();
			//}
		}
	}
	//迭代器模式
	namespace MyIteraor
	{/*迭代器模式提供一种方法顺序访问一个聚合对象中的各个元素，而又不暴露其内部的表示*/
		public class MenuItem
		{
			string name;
			string description;
			bool vegetarian;
			double price;

			public MenuItem(string name, string description, bool vegetarian, double price) {
				this.name = name;
				this.description = description;
				this.vegetarian = vegetarian;
				this.price = price;
			}
			public string GetName() { return name; }
			public string GetDescription() { return description; }
			public double GetPrice() { return price; }
			public bool IsVegetarian() { return vegetarian; }
		}
		public class PancakeHouseMenu : Menu
		{
			ArrayList menuItems;
			public PancakeHouseMenu() {
				menuItems = new ArrayList();
			}
			public void AddItem(string name, string description, bool vegetarian, double price) {
				MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
				menuItems.Add(menuItem);
			}
			//public ArrayList GetMenuItems() { return menuItems; }
			public Iterator CreateIterator() {
				return new PancakeHouseIterator(menuItems);
			}
			//还有菜单的其他方法
		}
		public class DinerMenu : Menu
		{
			static int MAX_ITEMS = 6;
			int numberOfItems = 0;
			MenuItem[] menuItems;//使用数组可以控制菜单的长度，并且取出菜单项时不需要转型
			public DinerMenu() {
				menuItems = new MenuItem[MAX_ITEMS];
			}
			public void AddItem(string name, string description,
				bool vegetarian, double price) {
				MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
				if (numberOfItems >= MAX_ITEMS) {
					Console.WriteLine("sorry, menu is full");
				} else {
					menuItems[numberOfItems] = menuItem;
					numberOfItems = numberOfItems + 1;
				}
			}
			//加入迭代器后注销此方法1不需要2暴露内部实现public MenuItem[] GetMenuItems() { return menuItems; }
			//加入下方方法；
			public Iterator CreateIterator() {
				return new DinerMenuIterator(menuItems);
			}
			//这里还有菜单的其他方法
		}
		public interface Iterator
		{
			bool hasNext();
			object next();
		}

		//封装遍历
		public class DinerMenuIterator : Iterator
		{
			MenuItem[] items;
			int position;
			public DinerMenuIterator(MenuItem[] items) {
				this.items = items;
			}
			public bool hasNext() {
				if (position >= items.Length || items[position] == null) {
					return false;
				} else {
					return true;
				}
			}
			public object next() {
				MenuItem menuItem = items[position];
				position = position + 1;
				return menuItem;
			}
			//可省略public void Remove()
			//{
			//    if (position <= 0)
			//    {
			//        throw new Exception("You can't remove an item until you've done at least one Next()")
			//    }
			//    if (items[position-1] != null)
			//    {
			//        for (int i = position-1; i < items.Length-1; i++)
			//        {
			//            items[i] = items[i + 1];
			//        }
			//        items[items.Length-1] = null;
			//    }
			//}
		}
		public class PancakeHouseIterator : Iterator
		{
			ArrayList items;
			int position;
			public PancakeHouseIterator(ArrayList items) {
				this.items = items;
			}
			public bool hasNext() {
				if (position >= items.Count) {
					return false;
				} else {
					return true;
				}
			}

			public object next() {
				MenuItem menuItem = (MenuItem)items[position];
				position = position + 1;
				return menuItem;
			}
		}
		public class Waitress
		{//由于菜单的实现已经被封装起来了。女招待不知道菜单如何存储菜单项集合
		 //PancakeHouseMenu pancakeHouseMenu;
		 //DinerMenu dinerMenu;
		 //脱离具体菜单
			Menu pancakeHouseMenu;
			Menu dinerMenu;
			//加入咖啡店
			Menu cafeMenu;
			//public Waitress(PancakeHouseMenu pancakeHouseMenu, DinerMenu dinerMenu)
			public Waitress(Menu pancakeHouseMenu, Menu dinerMenu, Menu cafeMenu) {
				this.pancakeHouseMenu = pancakeHouseMenu;
				this.dinerMenu = dinerMenu;
				this.cafeMenu = cafeMenu;
			}
			public void PrintMenu() {
				Iterator pancakeIterator = pancakeHouseMenu.CreateIterator();
				Iterator dinerIterator = dinerMenu.CreateIterator();
				Iterator cafeIterator = cafeMenu.CreateIterator();
				Console.WriteLine("Menu\n---\nBREAKFAST");
				PrintMenu(pancakeIterator);
				Console.WriteLine("\nLUNCH");
				PrintMenu(dinerIterator);
				Console.WriteLine("\nDINNER");
				PrintMenu(cafeIterator);
			}
			private void PrintMenu(Iterator iterator) {//这样就不用每一个菜单方法写一个遍历了
													   //只需要一个循环就可以多态的处理任何项的集合
													   //而且不会捆绑具体类（MenuItem[]&ArrayList）只使用一个接口（迭代器）
													   //但还是捆绑于两个具体的菜单类，所以让两个菜单类实现同一个接口Menu（下方）
				while (iterator.hasNext()) {
					MenuItem menuItem = (MenuItem)iterator.next();
					Console.Write(menuItem.GetName() + ", ");
					Console.Write(menuItem.GetPrice() + " -- ");
					Console.Write(menuItem.GetDescription());
				}
			}
			//其他的方法
		}
		public interface Menu { public Iterator CreateIterator(); }
		//Test1 public class MenuTestDrive
		//{
		//    static void Main()
		//    {
		//        PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
		//        DinerMenu dinerMenu = new DinerMenu();
		//        Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu);
		//        waitress.PrintMenu();
		//    }
		//}
		/*现在又出现咖啡厅（直接改版）*/
		public class CafeMenu : Menu
		{
			Hashtable menuItems = new Hashtable();
			public CafeMenu() {
				AddItem("Fries", "blbl", true, 3.99);
				AddItem("hhh", "ououou", false, 3.69);
				AddItem("A", "whole", true, 4.29);
			}
			public void AddItem(string name, string description, bool vegetarian, double price) {
				MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
				menuItems.Add(menuItem.GetName(), menuItem);
			}
			//public Hashtable GetItems()
			//{
			//    return menuItems;
			//}
			public Iterator CreateIterator() {
				return new CafeMenuIterator(menuItems);
			}
		}
		public class CafeMenuIterator : Iterator
		{
			IDictionaryEnumerator enumerator;

			public CafeMenuIterator(Hashtable collection) {
				enumerator = collection.GetEnumerator();
			}
			public bool hasNext() {
				return enumerator.MoveNext();
			}
			public object next() {
				MenuItem menuItem = (MenuItem)enumerator.Value;
				return menuItem;
			}
		}
		//Test2 public class MenuTestDrive
		//{
		//    static void Main()
		//    {
		//        PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
		//        DinerMenu dinerMenu = new DinerMenu();
		//        CafeMenu cafeMenu = new CafeMenu();
		//        Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu, cafeMenu);
		//        waitress.PrintMenu();
		//    }
		//}

		/*你以为结束了吗，
         * 要想遍历可以foreach而且每出现一个Menu，waitress就要改，你不觉得违反了开闭原则了吗
         现在餐厅又想加入甜点菜单作为他的子菜单，因为类型不同，不能将整个甜品菜单加入菜单项数组成为一个menuItem
        所以很自然的想到了树状结构
        *接下来看下一个模式，之后再往下看*/
		/*
		public abstract class MenuComponent
		{
			public abstract Iterator CreateIterator();
		}
		public class Menu : MenuComponent
		{
			//其他部分代码不变
			public override Iterator CreateIterator() {
				return new CompositeIterator(menuComponents.Iterator());
			}
		}
		public class MenuItem : MenuComponent
		{
			//其他不变
			public override Iterator CreateIterator() {
				return new NullIterator();
			}
		}
		public class CompositeIterator : Iterator
		{
			Stack stack = new Stack();
			public CompositeIterator(Iterator iterator) {
				stack.Push(iterator);
			}
			/*C#迭代器int position = -1;
            int startingPoint;
            public CompositeIterator(Iterator iterator, int startingPoint)
            {
                stack.Push(iterator);
                this.startingPoint = startingPoint;
            }
            public bool MoveNext()
            {
                if (position != stack.Count)
                {
                    position++;
                }
                return position < stack.Count;
                //position++;
                //if (position >= stack.Count)
                //{
                //    return false;
                //}
                //return true;
            }
            public object Current {
                get {
                    object current = null;
                    if (position == -1 || position >= stack.Count)
                    {
                        throw new InvalidOperationException();
                    }
                    int index = position + startingPoint;
                    index = index % stack.Count;
                    for (int i = stack.Count; i <= index; i--)
                    {
                        current = stack.Peek();
                    }
                    return current;
                } 
            }*\/

			public object next() {
				if (hasNext()) {
					Iterator iterator = (Iterator)stack.Peek();
					MenuComponent component = (MenuComponent)iterator.next();
					if (component is Menu) {
						stack.Push(component.CreateIterator());
					}
					return component;
				} else { return null; }
			}
			public bool hasNext() {
				if (stack.Count == 0) {
					return false;
				} else {
					Iterator iterator = (Iterator)stack.Peek();
					if (!iterator.hasNext()) {
						stack.Pop();
						return hasNext();
					} else { return true; }
				}
			}
			public void Remove() {
				throw new Exception("Unsuppoted Operation");
			}
		}
		/*“外部”的迭代器，所以有许多需要追踪的事情。
         * 外部迭代器必须维护它在遍历中的位置，
         * 以便外部客户可以通过调用hasNext(）和next()来驱动遍历。
         * 在这个例子中，我们的代码也必须维护组合递归结构的位置。
         * 这也就是为什么当我们在组合层次结构中上上下下时，使用堆栈来维护我们的位置。*\/
		public class NullIterator : Iterator
		{
			public object next() {
				return null;
			}
			public bool hasNext() {
				return false;
			}
			public void Remove() {
				throw new Exception("Unsuppoted Operation");
			}
		}
		public class Waitress
		{
			MenuComponent allMenus;
			public Waitress(MenuComponent allMenus) {
				this.allMenus = allMenus;
			}
			public void PrintMenu() {
				allMenus.Print();
			}
			public void PrintVegetarianMenu() {
				Iterator iterator = allMenus.CreateIterator();
				Console.WriteLine("\nVEGETARIAN MENU\n----");
				while (iterator.hasNext()) {
					MenuComponent menuComponent = 
						(MenuComponent)iterator.next();
					try {
						if (menuComponent.isVegetarian()) {
							menuComponent.Print();
						}
					} catch (Exception) {
						throw new Exception("Unsupported Operation");
					}
				}
			}
		}
		*/



	}
	//组合模式
	namespace Composite
	{/*组合模式允许你将对象组合成树形结构来表现“整体/部分”层次结构。
      * 组合能让客户以一致的方式处理个别对象以及对象组合。*/
		//就上一模式中菜单和菜单项，菜单项中又有菜单或菜单项。
		/*       ^叶
        //组合<
        *              ^叶
        //       v组合<
        *              v叶*/
		//那么应用组合模式就需要创建一个组件接口来作为菜单和菜单项的共同接口，
		//让我们能够用统一做法处理菜单和菜单项。

		//所有组件必须实现MenuComponent接口
		//（尽管菜单组件扮演了两个角色）
		//但叶节点和组合节点不同有些方法可能不适合，对此有时候，你最好抛出运行时异常
		public abstract class MenuComponent
		{
			public virtual void Add(MenuComponent menuComponent) {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual void Remove(MenuComponent menuComponent) {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual MenuComponent GetChild(int i) {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual string GetName() {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual string GetDescription() {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual double GetPrice() {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual bool IsVegetarian() {
				throw new Exception("Unsuppoted Operation");
			}
			public virtual void Print() {
				throw new Exception("Unsuppoted Operation");
			}
		}
		//叶
		public class MenuItem : MenuComponent
		{
			string name;
			string description;
			bool vegetarian;
			double price;
			public MenuItem(string name, string description, bool vegetarian, double price) {
				this.name = name;
				this.description = description;
				this.vegetarian = vegetarian;
				this.price = price;
			}
			public override string GetName() { return name; }
			public override string GetDescription() { return description; }
			public override double GetPrice() { return price; }
			public override bool IsVegetarian() { return vegetarian; }
			public override void Print() {
				Console.Write(" " + GetName());
				if (IsVegetarian()) {
					Console.Write("(v)");
				}
				Console.Write(", " + GetPrice());
				Console.WriteLine("     -- " + GetDescription());
			}
		}
		//组合
		public class Menu : MenuComponent
		{
			IEnumerator enumerator;
			ArrayList menuComponents = new ArrayList();
			string name;
			string description;
			public Menu(string name, string description) {
				this.name = name;
				this.description = description;
			}
			public override void Add(MenuComponent menuComponent) {
				menuComponents.Add(menuComponent);
			}
			public override void Remove(MenuComponent menuComponent) {
				menuComponents.Remove(menuComponent);
			}
			public override MenuComponent GetChild(int i) {
				return (MenuComponent)menuComponents[i];
			}
			public override string GetName() { return name; }
			public override string GetDescription() { return description; }
			public override void Print() {
				Console.Write("\n" + GetName());
				Console.WriteLine(", " + GetDescription());
				Console.WriteLine("---------------------");

				//Iterator
				enumerator = menuComponents.GetEnumerator();
				while (enumerator.MoveNext()) {
					((MenuComponent)enumerator.Current).Print();
				}

				/*foreach (MenuComponent menuComponent in menuComponents)
                {//遍历期间如果遇到另一个菜单对象他的print方法开始另一个遍历（递归）
                    menuComponent.Print();
                }*/
			}
		}
		/*在我们写 MenuComponent 类的print(）方法的时候，
         * 我们利用了一个迭代器来遍历组件内的每个项。
         * 如果遇到的是菜单（而不是菜单项），我们就会递归地调用print()方法处理它。
         * 换句话说， MenuComponent 是在“内部”自行处理遍历。*/



		public class Waitress
		{
			MenuComponent allMenus;
			public Waitress(MenuComponent allMenus) {
				this.allMenus = allMenus;
			}
			public void PrintMenu() {
				allMenus.Print();
			}
		}
		//public class MenuTestDrive
		//{
		//	static void Main() {
		//		MenuComponent pancakeHouseMenu = new Menu("PANCAKE HOUSE MENU", "Breakfast");
		//		MenuComponent dinerMenu = new Menu("DINER MENU", "Lunch");
		//		MenuComponent cafeMenu = new Menu("CAFE MENU", "Dinner");
		//		MenuComponent dessertMenu = new Menu("DESSERT MENU", "Dessert of course!");
		//		MenuComponent allMenus = new Menu("ALL MENUS", "All menus combined");
		//		allMenus.Add(pancakeHouseMenu);
		//		allMenus.Add(dinerMenu);
		//		allMenus.Add(cafeMenu);

		//		dinerMenu.Add(new MenuItem("Pasta", "sauce and bread", true, 3.89));
		//		dinerMenu.Add(dessertMenu);//菜单中加入菜单
		//		dessertMenu.Add(new MenuItem("Apple Pie", "apple pie with a crust", true, 1.59));
		//		//加入更多菜单项
		//		Waitress waitress = new Waitress(allMenus);
		//		waitress.PrintMenu();

		//	}
		//}

		/*但是现在MenuComponent类有两个责任。组合模式不但要管理层次结构，而且要执行菜单的操作。
         * 你的观察有几分真实性。我们可以这么说，组合模式以单一责任设计原则换取透明性( transparency )。
         * 什么是透明性?通过让组件的接口同时包含一些管理子节点和叶节点的操作。客户就可以将组合和叶节点一视同仁。
         * 也就是说，一个元素究竟是组合还是叶节点，对客户是透明的。
         * 现在，我们在 MenuComponent 类中同时具有两种类型的操作。
         * 因为客户有机会对一个元素做一些不恰当或是没有意义的操作（例如试图把菜单添加到菜单项），所以我们失去了一些“安全性”。
         * 这是设计上的抉择：我们当然也可以采用另一种方向的设计，将责任区分开来放在不同的接口中。
         * 这么一来，设计上就比较安全，但我们也因此失去了透明性，
         * 客户的代码将必须用条件语句和 instanceof 操作符处理不同类型的节点。
         * 所以，回到你的问题，这是一个很典型的折衷案例。
         * 尽管我们受到设计原则的指导，但是，我们总是需要观察某原则对我们的设计所造成的影响。
         * 有时候，我们会故意做一些看似违反原则的事情。
         * 然而，在某些例子中，这是观点的问题。
         * 比方说：让管理孩子的操作(例如add0、remove()、getChild()）出现在叶节点中，似乎很不恰当，
         * 但是换个视角来看，你可以把叶节点视为没有孩子的节点。*/
		//现在回到迭代器

	}
	//状态模式
	namespace State
	{
		//状态模式：允许对象在内部状态改变时改变它的行为，对象看起来好像修改了它的类
		public interface IState
		{
			public void InsertQuarter();
			public void EjectQuarter();
			public void TurnCrank();
			public void Dispense();
		}
		public class SoldState : IState
		{
			GumballMachine gumballMachine;
			public SoldState(GumballMachine gumballMachine) {
				this.gumballMachine = gumballMachine;
			}
			public void Dispense() {
				gumballMachine.ReleaseBall();
				if (gumballMachine.GetCount() > 0) {
					gumballMachine.SetState(gumballMachine.GetNoQuarterState());
				} else {
					Console.WriteLine("Oops, out of gumballs!");
					gumballMachine.SetState(gumballMachine.GetSoldOutState());
				}
			}
			public void EjectQuarter() {
				Console.WriteLine("Sorry, you already turned the crank");
			}
			public void InsertQuarter() {
				Console.WriteLine("Please wait, we're already giving you a gumball");
			}
			public void TurnCrank() {
				Console.WriteLine("Turning twice doesn't get you another gumball!");
			}
		}
		public class NoQuarterState : IState
		{
			GumballMachine gumballMachine;
			public NoQuarterState(GumballMachine gumballMachine) {
				this.gumballMachine = gumballMachine;
			}
			public void Dispense() {
				Console.WriteLine("You need to pay first");
			}
			public void EjectQuarter() {
				Console.WriteLine("You haven't inserted a quarter");
			}
			public void InsertQuarter() {
				Console.WriteLine("You inserted a quarter");
				gumballMachine.SetState(gumballMachine.GetHasQuarterState());
			}
			public void TurnCrank() {
				Console.WriteLine("You turned, but there's no quarter");
			}
		}
		//winner在这里
		public class HasQuarterState : IState
		{
			Random randomWinner = new Random();
			GumballMachine gumballMachine;
			public HasQuarterState(GumballMachine gumballMachine) {
				this.gumballMachine = gumballMachine;
			}
			public void Dispense() {
				Console.WriteLine("No gumball dispensed");
			}
			public void EjectQuarter() {
				Console.WriteLine("Quarter returned");
				gumballMachine.SetState(gumballMachine.GetNoQuarterState());
			}
			public void InsertQuarter() {
				Console.WriteLine("You can't insert another quarter");
			}
			public void TurnCrank() {
				Console.WriteLine("You turned...");
				int winner = randomWinner.Next(10);
				if ((winner == 0) && (gumballMachine.GetCount() > 1)) {
					gumballMachine.SetState(gumballMachine.GetWinnerState());
				} else {
					gumballMachine.SetState(gumballMachine.GetSoldState());
				}
			}
		}
		public class SoldOutState : IState
		{
			GumballMachine gumballMachine;
			public SoldOutState(GumballMachine gumballMachine) {
				this.gumballMachine = gumballMachine;
			}
			public void Dispense() {
				Console.WriteLine("No ball dispensed");
			}
			public void EjectQuarter() {
				Console.WriteLine("You haven't inserted a quarter");
			}
			public void InsertQuarter() {
				Console.WriteLine("There haven't ball");
			}
			public void TurnCrank() {
				Console.WriteLine("You turned, but there haven't ball");
			}
		}
		public class WinnerState : IState
		{
			GumballMachine gumballMachine;
			public WinnerState(GumballMachine gumballMachine) {
				this.gumballMachine = gumballMachine;
			}
			public void Dispense() {
				Console.WriteLine("YOU'RE A WINER! You get two gumballs for your quarter");
				gumballMachine.ReleaseBall();
				if (gumballMachine.GetCount() == 0) {
					gumballMachine.SetState(gumballMachine.GetSoldOutState());
				} else {
					gumballMachine.ReleaseBall();
					if (gumballMachine.GetCount() > 0) {
						gumballMachine.SetState(gumballMachine.GetNoQuarterState());
					} else {
						Console.WriteLine("Oops, out of gumballs!");
						gumballMachine.SetState(gumballMachine.GetSoldOutState());
					}
				}
			}
			public void EjectQuarter() {
				Console.WriteLine("Sorry, you already turned the crank");
			}
			public void InsertQuarter() {
				Console.WriteLine("Please wait, we're already giving you a gumball");
			}
			public void TurnCrank() {
				Console.WriteLine("Turning twice doesn't get you another gumball!");
			}
		}

		public class GumballMachine
		{
			IState soldOutState;
			IState noQuarterState;
			IState hasQuarterState;
			IState soldState;
			IState winnerState;

			IState state;
			int count = 0;

			public GumballMachine(int numberGumballs) {
				soldOutState = new SoldOutState(this);
				noQuarterState = new NoQuarterState(this);
				hasQuarterState = new HasQuarterState(this);
				soldState = new SoldState(this);
				winnerState = new WinnerState(this);
				this.count = numberGumballs;
				state = soldOutState;
				if (numberGumballs > 0) {
					state = noQuarterState;
				}
			}

			public void InsertQuarter() {
				state.InsertQuarter();
			}
			public void EjectQuarter() {
				state.EjectQuarter();
			}
			public	void TurnCrank() {
				state.TurnCrank();
				state.Dispense();
			}
			public void SetState(IState state) {
				this.state = state;
			}
			public void ReleaseBall() {
				Console.WriteLine("A gumball comes rolling out the slot...");
				if (count != 0) {
					count -= 1;
				}
			}
			public IState GetHasQuarterState() {
				return hasQuarterState;
			}
			public IState GetNoQuarterState() {
				return noQuarterState;
			}
			public IState GetSoldState() {
				return soldState;
			}
			public IState GetSoldOutState() {
				return soldOutState;
			}
			public IState GetWinnerState() {
				return winnerState;
			}
			public int GetCount() {//获取糖果数目
				return count;
			}
		}
		/*public class GumballMachineTestDrive
		{
			static void Main() {
				GumballMachine gumballMachine = new GumballMachine(5);

				Console.WriteLine(gumballMachine);

				gumballMachine.InsertQuarter();
				gumballMachine.TurnCrank();

				Console.WriteLine(gumballMachine);

				gumballMachine.InsertQuarter();
				gumballMachine.TurnCrank();
				gumballMachine.InsertQuarter();
				gumballMachine.TurnCrank();

				Console.WriteLine(gumballMachine);
			}
		}*/
	}
	//代理模式
	/*为另一个对象提供一个替身或占位符以访问此对象*/
	//
}
