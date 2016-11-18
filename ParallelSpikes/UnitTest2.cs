using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParallelSpikes
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void RunnerUsingAsyncAwait()
        {
            Console.WriteLine(DateStamp() + "start");

            Task<string> x = this.SimulateAsync("x", 5);
            Task<string> y = this.SimulateAsync("y", 6);

            string xResult = x.Result;
            Console.WriteLine(DateStamp() + xResult);

            string yResult = y.Result;
            Console.WriteLine(DateStamp() + yResult);

        }

        private async Task<string> SimulateAsync(string s, int secs)
        {
            Console.WriteLine(DateStamp() + s + " " + "doing the wait");

            await Task.Delay(secs * 1000);

            Console.WriteLine(DateStamp() + s + " " + "waited 5 secs; doing the rest");

            Thread.Sleep(2000);
            Console.WriteLine(DateStamp() + s + " " + "done the rest");

            return s + " " + "result: done";
        }


        [TestMethod]
        public void RunnerUsingTask()
        {

            Console.WriteLine(DateStamp() + "start");

            Task x = Task.Factory.StartNew(() => Console.Write("")); // Console.WriteLine(DateStamp() + "I am the x task"));
            Task y = Task.Factory.StartNew(() => Console.Write("")); // Console.WriteLine(DateStamp() + "I am the y task"));

            var x2 = x.ContinueWith(delegate
            {
                int secs = 5;
                string s = "x";

                Console.WriteLine(DateStamp() + s + " " + "doing the wait");

                Thread.Sleep(secs * 1000);

                Console.WriteLine(DateStamp() + s + " " + "waited 5 secs; doing the rest");

                Thread.Sleep(2000);
                Console.WriteLine(DateStamp() + s + " " + "done the rest");

                return s + " " + "result: done";
            });

            var y2 = x.ContinueWith(delegate
            {
                int secs = 6;
                string s = "y";

                Console.WriteLine(DateStamp() + s + " " + "doing the wait");

                Thread.Sleep(secs * 1000);

                Console.WriteLine(DateStamp() + s + " " + "waited 5 secs; doing the rest");

                Thread.Sleep(2000);
                Console.WriteLine(DateStamp() + s + " " + "done the rest");

                return s + " " + "result: done";
            });

            string xResult = x2.Result;
            Console.WriteLine(DateStamp() + xResult);

            string yResult = y2.Result;
            Console.WriteLine(DateStamp() + yResult);

        }

        private static string DateStamp()
        {
            return String.Format("{0:hh:mm:ss.ff}", DateTime.Now) + " - ";
        }
    }
}
