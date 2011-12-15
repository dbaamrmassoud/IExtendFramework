/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 3:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;

namespace IExtendFramework.Threading
{
    /// <summary>
    /// A Threading class
    /// </summary>
    public class Thread
    {
        private EventHandler Event;
        object sender;
        EventArgs e;
        public Thread(EventHandler e)
        {
            this.Event = e;
        }
        
        public void Start(object sender = null, EventArgs e = null)
        {
            ThreadStart job = new ThreadStart(_Start);
            System.Threading.Thread thread = new System.Threading.Thread(job);
            thread.SetApartmentState(ApartmentState.MTA);
            this.e = e;
            this.sender = sender;
            thread.Start();
        }
        
        private void _Start()
        {
            Event.Invoke(sender, e);
        }
    }
}
