using System;
using System.Diagnostics;
using System.Timers;

namespace timer
{
	class Program
	{
		static void Main(string[] args)
		{
			var t2000 = new Timer(2000) { AutoReset = false };
			var waitForTimeout = new System.Threading.AutoResetEvent(false);
			t2000.Elapsed += (s, e) => waitForTimeout.Set();

			// stop 't2000' after one second, 
			// and start it again, 
			// we would expect our precision timer to run about 3 seconds now (1+2seconds)
			var t1000 = new Timer(1000) { AutoReset = false };
			t1000.Elapsed += (s, e) => { t2000.Stop(); t2000.Start(); };

			var precisionTimer = Stopwatch.StartNew();
			precisionTimer.Start();
			t2000.Start();
			t1000.Start();

			waitForTimeout.WaitOne();
			precisionTimer.Stop();

			Console.WriteLine($"Timeout after {precisionTimer.ElapsedMilliseconds}ms");
		}
	}

}
