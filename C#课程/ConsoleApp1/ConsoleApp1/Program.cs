using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncBreakfast
{
    //class Program
    //{
    //    public class Coffee { }
    //    public class Egg { }
    //    public class Juice { }
    //    public class Toast { }
    //    public class Bacon { }
    //    static async Task Main(string[] args)
    //    {
    //        Coffee cup = PourCoffee();
    //        Console.WriteLine("coffee is ready");

    //        var eggsTask = FryEggsAsync(2);
    //        var baconTask = FryBaconAsync(3);
    //        var toastTask = MakeToastWithButterAndJamAsync(2);

    //        var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
    //        while (breakfastTasks.Count > 0)
    //        {
    //            Task finishedTask = await Task.WhenAny(breakfastTasks);
    //            if (finishedTask == eggsTask)
    //            {
    //                Console.WriteLine("eggs are ready");
    //            }
    //            else if (finishedTask == baconTask)
    //            {
    //                Console.WriteLine("bacon is ready");
    //            }
    //            else if (finishedTask == toastTask)
    //            {
    //                Console.WriteLine("toast is ready");
    //            }
    //            breakfastTasks.Remove(finishedTask);
    //        }

    //        Juice oj = PourOJ();
    //        Console.WriteLine("oj is ready");
    //        Console.WriteLine("Breakfast is ready!");
    //    }
    //    static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
    //    {
    //        var toast = await ToastBreadAsync(number);
    //        ApplyButter(toast);
    //        ApplyJam(toast);

    //        return toast;
    //    }
    //    private static Juice PourOJ()
    //    {
    //        Console.WriteLine("Pouring orange juice");
    //        return new Juice();
    //    }
    //    private static void ApplyJam(Toast toast) =>
    //        Console.WriteLine("Putting jam on the toast");
    //    private static void ApplyButter(Toast toast) =>
    //        Console.WriteLine("Putting butter on the toast");
    //    private static async Task<Toast> ToastBreadAsync(int slices)
    //    {
    //        for (int slice = 0; slice < slices; slice++)
    //        {
    //            Console.WriteLine("Putting a slice of bread in the toaster");
    //        }
    //        Console.WriteLine("Start toasting...");
    //        await Task.Delay(3000);
    //        Console.WriteLine("Remove toast from toaster");

    //        return new Toast();
    //    }
    //    private static async Task<Bacon> FryBaconAsync(int slices)
    //    {
    //        Console.WriteLine($"putting {slices} slices of bacon in the pan");
    //        Console.WriteLine("cooking first side of bacon...");
    //        await Task.Delay(3000);
    //        for (int slice = 0; slice < slices; slice++)
    //        {
    //            Console.WriteLine("flipping a slice of bacon");
    //        }
    //        Console.WriteLine("cooking the second side of bacon...");
    //        await Task.Delay(3000);
    //        Console.WriteLine("Put bacon on plate");

    //        return new Bacon();
    //    }
    //    private static async Task<Egg> FryEggsAsync(int howMany)
    //    {
    //        Console.WriteLine("Warming the egg pan...");
    //        await Task.Delay(3000);
    //        Console.WriteLine($"cracking {howMany} eggs");
    //        Console.WriteLine("cooking the eggs ...");
    //        await Task.Delay(3000);
    //        Console.WriteLine("Put eggs on plate");

    //        return new Egg();
    //    }
    //    private static Coffee PourCoffee()
    //    {
    //        Console.WriteLine("Pouring coffee");
    //        return new Coffee();
    //    }
    //}


    public class Account
    {

        private readonly object Lock = new object();
        private decimal balance;

        public Account(decimal balance)
        {
            this.balance = balance;
        }
        public Account() => balance = 0;
        static Account() { }

        public decimal Debit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
            }
            decimal appliedAmount = 0;
            lock (Lock)
            {
                if (balance >= amount)
                {
                    balance -= amount;
                    appliedAmount = amount;
                }
            }
            return appliedAmount;
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
            }
            lock (Lock)
            {
                balance += amount;
            }
        }

        public decimal GetBalance()
        {
            lock (Lock)
            {
                return balance;
            }
        }
    }

    public class AccountTest
    {
        
        //static async Task Main()
        //{
            
        //    //System.Threading.ThreadState
        //    Account account = new Account(1000);
        //    Thread thread = new Thread(() => Console.WriteLine());

        //    var tasks = new Task[100];
        //    for (int i = 0; i < tasks.Length; i++)
        //    {
        //        tasks[i] = Task.Run(() => Update(account));
        //    }
        //    await Task.WhenAll(tasks);
        //    Console.WriteLine($"Account's balance is {account.GetBalance()}");
        //}

        static void Update(Account account)
        {
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }





    class SafeQueue<T>
    {
        // A queue that is protected by Monitor.
        private Queue<T> m_inputQueue = new Queue<T>();

        // Lock the queue and add an element.
        public void Enqueue(T qValue)
        {
            // Request the lock, and block until it is obtained.
            Monitor.Enter(m_inputQueue);
            try
            {
                // When the lock is obtained, add an element.
                m_inputQueue.Enqueue(qValue);
            }
            finally
            {
                // Ensure that the lock is released.
                Monitor.Exit(m_inputQueue);
            }
        }

        // Try to add an element to the queue: Add the element to the queue
        // only if the lock is immediately available.
        public bool TryEnqueue(T qValue)
        {
            // Request the lock.
            if (Monitor.TryEnter(m_inputQueue))
            {
                try
                {
                    m_inputQueue.Enqueue(qValue);
                }
                finally
                {
                    // Ensure that the lock is released.
                    Monitor.Exit(m_inputQueue);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // Try to add an element to the queue: Add the element to the queue
        // only if the lock becomes available during the specified time
        // interval.
        public bool TryEnqueue(T qValue, int waitTime)
        {
            // Request the lock.
            if (Monitor.TryEnter(m_inputQueue, waitTime))
            {
                try
                {
                    m_inputQueue.Enqueue(qValue);
                }
                finally
                {
                    // Ensure that the lock is released.
                    Monitor.Exit(m_inputQueue);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // Lock the queue and dequeue an element.
        public T Dequeue()
        {
            T retval;

            // Request the lock, and block until it is obtained.
            Monitor.Enter(m_inputQueue);
            try
            {
                // When the lock is obtained, dequeue an element.
                retval = m_inputQueue.Dequeue();
            }
            finally
            {
                // Ensure that the lock is released.
                Monitor.Exit(m_inputQueue);
            }

            return retval;
        }

        // Delete all elements that equal the given object.
        public int Remove(T qValue)
        {
            int removedCt = 0;

            // Wait until the lock is available and lock the queue.
            Monitor.Enter(m_inputQueue);
            try
            {
                int counter = m_inputQueue.Count;
                while (counter > 0)
                // Check each element.
                {
                    T elem = m_inputQueue.Dequeue();
                    if (!elem.Equals(qValue))
                    {
                        m_inputQueue.Enqueue(elem);
                    }
                    else
                    {
                        // Keep a count of items removed.
                        removedCt += 1;
                    }
                    counter = counter - 1;
                }
            }
            finally
            {
                // Ensure that the lock is released.
                Monitor.Exit(m_inputQueue);
            }

            return removedCt;
        }

        // Print all queue elements.
        public string PrintAllElements()
        {
            StringBuilder output = new StringBuilder();

            // Lock the queue.
            Monitor.Enter(m_inputQueue);
            try
            {
                foreach (T elem in m_inputQueue)
                {
                    // Print the next element.
                    output.AppendLine(elem.ToString());
                }
            }
            finally
            {
                // Ensure that the lock is released.
                Monitor.Exit(m_inputQueue);
            }

            return output.ToString();
        }
    }

    public class Example
    {
        private static SafeQueue<int> q = new SafeQueue<int>();
        private static int threadsRunning = 0;
        private static int[][] results = new int[3][];

        

        //static void Main()
        //{
        //    Console.WriteLine("Working...");

        //    for (int i = 0; i < 3; i++)
        //    {
        //        Thread t = new Thread(ThreadProc);
        //        t.Start(i);
        //        Interlocked.Increment(ref threadsRunning);
        //    }
        //}

        private static void ThreadProc(object state)
        {
            DateTime finish = DateTime.Now.AddSeconds(10);
            Random rand = new Random();
            int[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int threadNum = (int)state;

            while (DateTime.Now < finish)

            {
                int what = rand.Next(250);
                int how = rand.Next(100);

                if (how < 16)
                {
                    q.Enqueue(what);
                    result[(int)ThreadResultIndex.EnqueueCt] += 1;
                }
                else if (how < 32)
                {
                    if (q.TryEnqueue(what))
                    {
                        result[(int)ThreadResultIndex.TryEnqueueSucceedCt] += 1;
                    }
                    else
                    {
                        result[(int)ThreadResultIndex.TryEnqueueFailCt] += 1;
                    }
                }
                else if (how < 48)
                {
                    // Even a very small wait significantly increases the success
                    // rate of the conditional enqueue operation.
                    if (q.TryEnqueue(what, 10))
                    {
                        result[(int)ThreadResultIndex.TryEnqueueWaitSucceedCt] += 1;
                    }
                    else
                    {
                        result[(int)ThreadResultIndex.TryEnqueueWaitFailCt] += 1;
                    }
                }
                else if (how < 96)
                {
                    result[(int)ThreadResultIndex.DequeueCt] += 1;
                    try
                    {
                        q.Dequeue();
                    }
                    catch
                    {
                        result[(int)ThreadResultIndex.DequeueExCt] += 1;
                    }
                }
                else
                {
                    result[(int)ThreadResultIndex.RemoveCt] += 1;
                    result[(int)ThreadResultIndex.RemovedCt] += q.Remove(what);
                }
            }

            results[threadNum] = result;

            if (0 == Interlocked.Decrement(ref threadsRunning))
            {
                StringBuilder sb = new StringBuilder(
                   "                               Thread 1 Thread 2 Thread 3    Total\n");

                for (int row = 0; row < 9; row++)
                {
                    int total = 0;
                    sb.Append(titles[row]);

                    for (int col = 0; col < 3; col++)
                    {
                        sb.Append(String.Format("{0,9}", results[col][row]));
                        total += results[col][row];
                    }

                    sb.AppendLine(String.Format("{0,9}", total));
                }

                Console.WriteLine(sb.ToString());
            }
        }

        private static string[] titles = {
      "Enqueue                       ",
      "TryEnqueue succeeded          ",
      "TryEnqueue failed             ",
      "TryEnqueue(T, wait) succeeded ",
      "TryEnqueue(T, wait) failed    ",
      "Dequeue attempts              ",
      "Dequeue exceptions            ",
      "Remove operations             ",
      "Queue elements removed        "};

        private enum ThreadResultIndex
        {
            EnqueueCt,
            TryEnqueueSucceedCt,
            TryEnqueueFailCt,
            TryEnqueueWaitSucceedCt,
            TryEnqueueWaitFailCt,
            DequeueCt,
            DequeueExCt,
            RemoveCt,
            RemovedCt
        };
    }

    /* This example produces output similar to the following:

    Working...
                                   Thread 1 Thread 2 Thread 3    Total
    Enqueue                          277382   515209   308464  1101055
    TryEnqueue succeeded             276873   514621   308099  1099593
    TryEnqueue failed                   109      181      134      424
    TryEnqueue(T, wait) succeeded    276913   514434   307607  1098954
    TryEnqueue(T, wait) failed            2        0        0        2
    Dequeue attempts                 830980  1544081   924164  3299225
    Dequeue exceptions                12102    21589    13539    47230
    Remove operations                 69550   129479    77351   276380
    Queue elements removed            11957    22572    13043    47572
     */
}