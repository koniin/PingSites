using System;

namespace PingSites
{
    public class Program
    {
        static void Main(string[] args) {
            var pinger = new Pinger();
            pinger.PingHosts(new[] { "www.google.com", "www.halens.se", "www.reddit.com" }, 10, 2000);
            Console.WriteLine("Pinging complete.");
            Console.Read();
        }
    }
}
