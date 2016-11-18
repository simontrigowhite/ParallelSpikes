using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParallelSpikes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UsingAsyncAwait()
        {
            Console.WriteLine(DateStamp() + "start");

            this.DoSomethingAsync();

            Console.WriteLine(DateStamp() + "carrying on");

            Thread.Sleep(6000);
            Console.WriteLine(DateStamp() + "waited 6 secs");

            Thread.Sleep(3000);
            Console.WriteLine(DateStamp() + "waited 3 secs");
        }

        private async Task DoSomethingAsync()
        {
            Console.WriteLine(DateStamp() + "doing the wait");

            await Task.Delay(5000);

            Console.WriteLine(DateStamp() + "doing the rest");

            Thread.Sleep(2000);
            Console.WriteLine(DateStamp() + "done the rest");

        }


        [TestMethod]
        public void UsingTask()
        {

            Task t = Task.Factory.StartNew(() => Console.WriteLine(DateStamp() + "I am the first task"));

            var t2 = t.ContinueWith(delegate
            {
                Console.WriteLine(DateStamp() + "Start t2");
                Thread.Sleep(5000);
                Console.WriteLine(DateStamp() + "Finish t2");
                return "waited 5 seconds";
            });

            Console.WriteLine(DateStamp() + "Start main");
            Thread.Sleep(6000);
            Console.WriteLine(DateStamp() + "Finish main, waited 6 seconds");

            //block1
            string result = t2.Result;
            Console.WriteLine(DateStamp() + "result of t2 is: " + result);
            //end block1

            //block2
            t2.ContinueWith(delegate
                {
                    Console.WriteLine(DateStamp() + "coming");
                    Thread.Sleep(3000);
                    Console.WriteLine(DateStamp() + "Here i am");
                });
            Console.WriteLine(DateStamp() + "Waiting my task");
            //end block2

            Thread.Sleep(4000);
            Console.WriteLine(DateStamp() + "4 secs passed");

            //Console.ReadLine();
        }



        private static string DateStamp()
        {
            return String.Format("{0:hh:mm:ss.ff}", DateTime.Now) + " - ";
        }
    }
}
