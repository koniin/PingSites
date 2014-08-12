using System;

namespace PingSites
{
    public class Program
    {
        static void Main(string[] args) {
            var pinger = new Pinger();

            Console.WriteLine("Press Esc to exit");
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
                pinger.PingHosts(new[] { "www.google.com", "www.halens.se", "www.reddit.com", "www.github.com" }, 1, 3000);
            }
        }
    }
}
