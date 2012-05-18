
/*
 * 
 * Copyright (c) 2006-2010 Mladen Jankovic. All rights reserved.
 * 
 * contact: kataklinger@gmail[dot]com
 * 
 *	- Redistributions of sources code must retain the above copyright notice,
 *	  this list of conditions and the following disclaimer. 
 * 
 *	- Redistributions in binary form must reproduce the above copyright notice,
 *	  this list of conditions and the following disclaimer in the documentation
 *	  and/or other materials provided with the distribution.
 * 
 *  - The names of contributors may not be used to endorse or promote products
 *    derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace IExtendFramework.Net
{

	#region IPRouteHop

	public class IPRouteHop
	{

		#region Address

		private IPAddress _address;

		public IPAddress Address { get { return _address; } }

		#endregion

		#region Hop

		private int _hop;

		public int Hop { get { return _hop; } }

		#endregion

		#region HostName

		private string _hostName = string.Empty;

		public string HostName { get { return _hostName; } }

		public void StoreHostName(string name)
		{
			_hostName = name;

			if (OnHostNameAvailable != null)
				OnHostNameAvailable(this);
		}

		public delegate void HostNameAvailableDelegate(IPRouteHop hop);

		public event HostNameAvailableDelegate OnHostNameAvailable;

		#endregion

		#region ResponseTimes

		private long[] _responseTimes = null;

		public long[] ResponseTimes { get { return _responseTimes; } }

		#endregion

		#region Constructor

		public IPRouteHop(IPAddress address, int hop, int pingCount)
		{
			_address = address;
			_hop = hop;
			_responseTimes = new long[pingCount];
		}

		#endregion

		#region PingsCompleted

		private int _pingsCompleted = 0;

		public int PingsCompleted { get { return _pingsCompleted; } }

		public bool Complete { get { return _pingsCompleted == _responseTimes.Length; } }

		#endregion

		#region StorePingResult

		public void StorePingResult(long responseTime) { _responseTimes[_pingsCompleted++] = responseTime; }

		#endregion

	}

	#endregion

	#region IPRouteTracer

	public class IPRouteTracer
	{

		#region Locks

		private object _runControlLock = new object();

		#endregion

		#region Target

		private IPAddress _target;

		public IPAddress Target
		{
			get { return _target; }
			set
			{
				lock (_runControlLock)
				{
					CheckRunning();
					_target = value;
				}
			}
		}

		#endregion

		#region Trace Settings

		#region PingsPerHop

		private int _pingsPerHop = 1;

		public int PingsPerHop
		{
			get { return _pingsPerHop; }
			set
			{
				lock (_runControlLock)
				{
					CheckRunning();
					_pingsPerHop = value;
				}
			}
		}

		#endregion

		#region HopRetires

		private int _hopRetries = 1;

		public int HopRetries
		{
			get { return _hopRetries; }
			set
			{
				lock (_runControlLock)
				{
					CheckRunning();
					_hopRetries = value;
				}
			}
		}

		#endregion

		#endregion

		#region Ping Settings

		#region Timeout

		private int _timeout = 2000;

		public int Timeout
		{
			get { return _timeout; }
			set
			{
				lock (_runControlLock)
				{
					CheckRunning();
					_timeout = value;
				}
			}
		}

		#endregion

		#region PingBufferSize

		private byte[] _pingBuffer = new byte[32];

		public int PingBufferSize
		{
			get { return _pingBuffer.Length; }
			set
			{
				lock (_runControlLock)
				{
					CheckRunning();
					_pingBuffer = new byte[value];
				}
			}
		}

		#endregion

		#endregion

		#region Tracer State

		#region Active

		private bool _active = false;

		public bool Active
		{
			get { return _active; }
			set
			{
				if (value)
					throw new InvalidOperationException("Scanner can only be stopped using this property");

				Stop(false);
			}
		}

		#endregion

		private ManualResetEvent _stopEvent = new ManualResetEvent(true);

		private int _currentHop = 1;

		private int _currentRetry = 1;

		#endregion

		#region Route

		private List<IPRouteHop> _route = new List<IPRouteHop>();

		public List<IPRouteHop> Route { get { return _route; } }

		#endregion

		#region Events

		public delegate void HopDelegate(IPRouteTracer tracer, IPRouteHop hop);

		#region OnHopSuccess

		public event HopDelegate OnHopSuccess;

		private void RaiseOnHopSuccess(IPRouteHop hop)
		{
			if (OnHopSuccess != null)
				OnHopSuccess(this, hop);
		}

		#endregion

		#region OnHopPing

		public event HopDelegate OnHopPing;

		private void RaiseOnHopPing(IPRouteHop hop)
		{
			if (OnHopPing != null)
				OnHopPing(this, hop);
		}

		#endregion

		#region OnHopFail

		public delegate void HopFailDelegate(IPRouteTracer tracer, int hop, int retry, IPAddress address, IPStatus status);

		public event HopFailDelegate OnHopFail;

		private void RaiseOnHopFail(int hop, int retry, IPAddress address, IPStatus status)
		{
			if (OnHopFail != null)
				OnHopFail(this, hop, retry, address, status);
		}

		#endregion

		#region OnTraceStarted

		public delegate void StartTraceDelegate(IPRouteTracer tracer);

		public event StartTraceDelegate OnTraceStarted;

		private void RaiseOnTraceStarted()
		{
			if (OnTraceStarted != null)
				OnTraceStarted(this);
		}

		#endregion

		#region OnTraceCompleted

		public enum TraceStatus
		{
			Successed,
			Failed,
			Canceled
		}

		public static string[] TraceStatusNames = { "successed", "failed", "canceled" };

		public delegate void TraceCompletedDelegate(IPRouteTracer tracer, TraceStatus status);

		public event TraceCompletedDelegate OnTraceCompleted;

		private void RaiseOnTraceCompleted(TraceStatus status)
		{
			if (OnTraceCompleted != null)
				OnTraceCompleted(this, status);
		}

		#endregion

		#endregion

		AsyncHostNameResolver _nameResolver = new AsyncHostNameResolver();

		#region Constructors

		public IPRouteTracer() { _ping.PingCompleted += new PingCompletedEventHandler(_ping_PingCompleted); }

		public IPRouteTracer(IPAddress target) : this() { _target = target; }

		public IPRouteTracer(IPAddress target, int timeout) : this(target) { _timeout = timeout; }

		public IPRouteTracer(IPAddress target, int timeout, int hopRetries) : this(target, timeout) { _hopRetries = hopRetries; }

		public IPRouteTracer(IPAddress target, int timeout, int hopRetries, int pingsPerHop) : this(target, timeout, hopRetries) { _pingsPerHop = pingsPerHop; }

		public IPRouteTracer(IPAddress target, int timeout, int hopRetries, int pingsPerHop, int bufferSize) : this(target, timeout, hopRetries, pingsPerHop) { PingBufferSize = bufferSize; }

		#endregion

		#region Control Methods

		public void Start()
		{
			lock (_runControlLock)
			{
				CheckRunning();

				_active = true;
				_stopEvent.Reset();

				_currentHop = _currentRetry = 1;

				_route.Clear();

				RaiseOnTraceStarted();

				Thread t = new Thread(new ThreadStart(NextHop));
				t.Start();
			}
		}

		public void Stop(bool wait)
		{
			lock (_runControlLock)
			{
				if (_active)
				{
					_active = false;
					if (wait)
						_stopEvent.WaitOne();
				}
			}
		}

		public void Wait() { _stopEvent.WaitOne(); }

		#endregion

		#region Control Helper Methods

		private void CheckRunning() { if (_active)throw new InvalidOperationException("Cannot performe this operation while scanner is running!"); }

		private bool CheckStop()
		{
			if (!_active)
			{
				_stopEvent.Set();
				RaiseOnTraceCompleted(TraceStatus.Canceled);
			}

			return !_active;
		}

		#endregion

		#region Pinging

		private Ping _ping = new Ping();

		private void PingHop(IPRouteHop hop) { if (!CheckStop()) _ping.SendAsync(hop.Address, _timeout, hop); }

		private void NextHop() { if (!CheckStop()) _ping.SendAsync(_target, _timeout, _pingBuffer, new PingOptions(_currentHop, true), null); }

		void _ping_PingCompleted(object sender, PingCompletedEventArgs e)
		{
			if (e.UserState == null)
			{
				if (e.Reply.Status == IPStatus.TtlExpired || e.Reply.Status == IPStatus.Success)
				{
					IPRouteHop hop = new IPRouteHop(e.Reply.Address, _currentHop, _pingsPerHop);
					_route.Add(hop);

					_nameResolver.ResolveHostName(e.Reply.Address, new AsyncHostNameResolver.StoreHostNameDelegate(hop.StoreHostName));

					RaiseOnHopSuccess(hop);

					_currentHop++;
					PingHop(hop);
				}
				else
				{
					RaiseOnHopFail(_currentHop, _currentRetry, e.Reply.Address, e.Reply.Status);

					if (_currentRetry < _hopRetries)
					{
						_currentRetry++;
						NextHop();
					}
					else
					{
						lock (_runControlLock)
						{
							_active = false;
							_stopEvent.Set();
						}

						RaiseOnTraceCompleted(TraceStatus.Failed);
					}
				}
			}
			else
			{
				IPRouteHop hop = (IPRouteHop)e.UserState;

				hop.StorePingResult(e.Reply.Status == IPStatus.Success ? e.Reply.RoundtripTime : -1);

				RaiseOnHopPing(hop);

				if (!hop.Complete)
					PingHop(hop);
				else if (!hop.Address.Equals(_target))
					NextHop();
				else
				{
					lock (_runControlLock)
					{
						_active = false;
						_stopEvent.Set();
					}

					RaiseOnTraceCompleted(TraceStatus.Successed);
				}
			}

		}

		#endregion

	}

	#endregion

}
