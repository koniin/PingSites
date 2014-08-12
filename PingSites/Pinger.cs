using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace PingSites
{
    public class Pinger {
        public void PingHost(string host) {
            PingHost(host, 1, 0);
        }

        public void PingHost(string host, int count) {
            PingHost(host, count, 1000);
        }

        public void PingHost(string host, int count, int timeOutMs) {
            for (var i = 0; i < count; i++) {
                try {
                    var ping = new Ping();
                    var reply = ping.Send(host);
                    WriteStatus(reply, host);
                } catch (PingException ex) {
                    WriteError(ex, host);
                }
                Thread.Sleep(timeOutMs);
            }
        }

        public void PingHosts(string[] hosts, int count, int timeOutMs) {
            for (var i = 0; i < count; i++) {
                foreach (var host in hosts) {
                    var p = new Ping();
                    p.PingCompleted += PingCompleted;
                    Console.WriteLine("{0} - Pinging: {1}", i, host);
                    p.SendAsync(host, 5000, host);
                }
                Thread.Sleep(timeOutMs);
                Console.WriteLine("\n");
            }
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e) {
            if (e.Error != null) {
                WriteError(e.Error, (string) e.UserState);
            } else if (e.Cancelled) {
                Console.WriteLine("{0}, Cancelled", e.UserState);
            } else {
                WriteStatus(e.Reply, (string) e.UserState);
            }
        }

        private void WriteStatus(PingReply reply, string host) {
            Console.WriteLine("{0} [{1}] : {2} : {3} ms", host, reply.Address, reply.Status, reply.RoundtripTime);
        }

        private void WriteError(Exception ex, string host) {
            if (ex.InnerException != null)
                Console.WriteLine("{0}, error: {1}", host, ex.InnerException.Message);
            else
                Console.WriteLine("{0}, error: {1}", host, ex.Message);
        }
    }
}
