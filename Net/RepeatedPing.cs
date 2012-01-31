/*
 * User: elijah
 * Date: 1/30/2012
 * Time: 2:46 PM
 */
using System;
using System.Net;
using System.Net.NetworkInformation;

namespace IExtendFramework.Net
{
    /// <summary>
    /// Repeatedly pings a host
    /// </summary>
    public class RepeatedPing
    {
        Threading.Thread t = null;
        private bool stopped = false;
        private string host = null;
        Ping ping = null;
        
        public int TotalPings
        { get; set; }
        public int SuccessfulPings
        { get; set; }
        public int FailedPings
        { get; set; }
        public bool Running
        {
            get
            {
                if (t == null)
                    return false;
                if (t.IsRunning == true)
                    return true;
                
                return false;
            }
        }
        
        public RepeatedPing(int waitTime)
        {
            ping = new Ping();
            t = new Threading.Thread(delegate(object sender, EventArgs e)
                                                                   {
                                                                       while (true)
                                                                       {
                                                                           System.Threading.Thread.Sleep(waitTime);
                                                                           if (stopped)
                                                                               break;
                                                                           // not async, so shouldnt cause much lag
                                                                           PingReply pr = ping.Send(host);
                                                                           if (pr.Status == IPStatus.Success)
                                                                               SuccessfulPings++;
                                                                           else
                                                                               FailedPings++;
                                                                           TotalPings++;
                                                                       }
                                                                   });
            TotalPings = 0;
            SuccessfulPings = 0;
            FailedPings = 0;
            ping.PingCompleted += new PingCompletedEventHandler(PingCompleted);
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            Console.WriteLine("Ping done!");
            TotalPings++;
            if (e.Cancelled == true || e.Error != null)
                FailedPings++;
            else
                SuccessfulPings++;
            
            
        }
        
        public void Start(string host)
        {
            this.host = host;
            if (!t.IsRunning)
                t.Start();
        }
        
        public void Stop()
        {
            stopped = true;
        }
    }
}
