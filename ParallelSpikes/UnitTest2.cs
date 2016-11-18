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
            string yResult = y.Result;
            Console.WriteLine(DateStamp() + xResult + " " + yResult);
        }

        private async Task<string> SimulateAsync(string s, int secs)
        {
            Console.WriteLine(DateStamp() + s + " " + "doing the wait");

            await Task.Delay(secs * 1000);

            Console.WriteLine(DateStamp() + s + " " + "waited " + secs + " secs; doing the rest");

            Thread.Sleep(2000);
            Console.WriteLine(DateStamp() + s + " " + "done the rest");

            return s + " " + "result: done";
        }


        [TestMethod]
        public void RunnerUsingTask()
        {

            Console.WriteLine(DateStamp() + "start");

            Task x = Task.Factory.StartNew(() => Console.Write(""));
            Task y = Task.Factory.StartNew(() => Console.Write(""));

            var x2 = x.ContinueWith(delegate
            {
                return SimulateAsTask("x", 5);
            });

            var y2 = y.ContinueWith(delegate
            {
                return SimulateAsTask("y", 6);
            });

            string xResult = x2.Result;
            string yResult = y2.Result;
            Console.WriteLine(DateStamp() + xResult + " " + yResult);
        }

        private static string SimulateAsTask(string s, int secs)
        {
            Console.WriteLine(DateStamp() + s + " " + "doing the wait");

            Thread.Sleep(secs * 1000);

            Console.WriteLine(DateStamp() + s + " " + "waited " + secs + " secs; doing the rest");

            Thread.Sleep(2000);
            Console.WriteLine(DateStamp() + s + " " + "done the rest");

            return s + " " + "result: done";
        }

        private static string DateStamp()
        {
            return String.Format("{0:HH:mm:ss.ff}", DateTime.Now) + " - ";
        }
    }
}
