using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Jil;
using System.Collections.Generic;
using System.Threading;
using Talamoana.PublichStashFetcher;
using System.Reactive.Linq;

namespace Talamoana.Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            Console.WriteLine("Monitoring stashes...\r\n");
            var task = RunAsync(cts.Token);

            Console.Beep(500, 100);
            Console.Beep(1000, 200);

            while (!Console.KeyAvailable) Task.Delay(100).Wait();

            Console.WriteLine("Aborting... Waiting up to 5 seconds for clean shutdown");
            cts.Cancel();
            task.Wait(5000);
        }

        static async Task RunAsync(CancellationToken ct)
        {
            
            var stashMonitor = new PublicStashMonitor();
            stashMonitor.Subscribe(new StashObserver());

            try
            {
                await stashMonitor.Monitor(true, ct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                Debugger.Break();
            }
        }
    }
}
