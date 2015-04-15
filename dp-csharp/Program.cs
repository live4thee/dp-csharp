using System;
using System.Diagnostics;

namespace DpCSharp
{
    class Program
    {
        class WillFailAtFirstTime
        {
            int count = 0;

            public T Do<T>(Func<T> func)
            {
                if (count++ == 0)
                {
                    throw new Exception("fault at first try");
                }

                return func();
            }
        }

        static void testRetry()
        {
            var v = new WillFailAtFirstTime();
            Func<int> f = () => v.Do<int>(() => { return 1; });

            int i = Retry.Do<int>(f, TimeSpan.FromSeconds(1), 2);
            Console.WriteLine("Retry result: " + i);
        }

        static void Main(string[] args)
        {
            testRetry();
        }
    }
}
