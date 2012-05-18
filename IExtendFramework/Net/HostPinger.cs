
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
using System.Threading;
using System.Net.NetworkInformation;
using System.Xml;

namespace IExtendFramework.Net
{

	#region HostStatus

	/// <summary>
	/// Defines host status.
	/// </summary>
	public enum HostStatus
	{
		/// <summary>
		/// Host is dead (it is not responsing to pings).
		/// </summary>
		Dead,
		/// <summary>
		/// Host is alive (it is responsing to pings).
		/// </summary>
		Alive,
		/// <summary>
		/// IP address cannot be obtained using DNS server.
		/// </summary>
		DnsError,
		/// <summary>
		/// Host did not respond to any ping since pinging is started
		/// but number of unresponded pings are not enough to declare host as dead.
		/// </summary>
		Unknown
	};

	#endregion

	#region HostPinger Events' Delegates

	public delegate void OnPingDelegate(HostPinger host);

	public delegate void OnHostStatusChangeDelegate(HostPinger host,
		HostStatus oldStatus, HostStatus newStatus);

	public delegate void OnHostPingerCommandDelegate(HostPinger host);

	public delegate void OnHostNameChangedDelegate(HostPinger host);

	#endregion

	#region IPingLogger

	/// <summary>
	/// Logs commands issued to host pinger and status changes.
	/// </summary>
	public interface IPingLogger
	{
		/// <summary>
		/// Logs command that starts pinging.
		/// </summary>
		/// <param name="host">host pinger that was started.</param>
		void LogStart(HostPinger host);

		/// <summary>
		/// Logs command that stops pinging.
		/// </summary>
		/// <param name="host">host pinger that was stopped.</param>
		void LogStop(HostPinger host);

		/// <summary>
		/// Logs status change of the host.
		/// </summary>
		/// <param name="host">host whose status has been changed.</param>
		/// <param name="oldStatus">old status of the host.</param>
		/// <param name="newStatus">new status of the host.</param>
		void LogStatusChange(HostPinger host, HostStatus oldStatus, HostStatus newStatus);
	}

	#endregion

	#region PingResultsBuffer

	/// <summary>
	/// Keeps recent history (results of most recent pings).
	/// </summary>
	public class PingResultsBuffer
	{

		#region Entry Class

		/// <summary>
		/// Object of this class represent several combined results that has same values in the recent history buffer.
		/// </summary>
		private class Entry
		{
			/// <summary>
			/// Initializes entry of the recent history buffer.
			/// </summary>
			/// <param name="received">indicates whether the combined results was successfull pings or unresponded pings.</param>
			/// <param name="count">number of combined results.</param>
			public Entry(bool received, int count)
			{
				_received = received;
				_count = count;
			}

			/// <summary>
			/// Indicates whether the combined results was successfull pings or unresponded pings.
			/// </summary>
			public bool _received;

			/// <summary>
			/// Number of combined results.
			/// </summary>
			public int _count;
		}

		#endregion

		#region Private Attributes

		/// <summary>
		/// Entries of recent history buffer
		/// </summary>
		private List<Entry> _buffer = new List<Entry>();

		private object _syncObject = new object();

		#endregion

		#region BufferSize

		/// <summary>
		/// Default number of results stored in the buffer.
		/// </summary>
		public static readonly int DEFAULT_BUFFER_SIZE = 100;

		/// <summary>
		/// Number of results stored in the buffer.
		/// </summary>
		private int _bufferSize = DEFAULT_BUFFER_SIZE;

		/// <summary>
		/// Number of results stored in the buffer. When the new buffer size is smaller then previous 
		/// results that cannot fit the new size are removed from the buffer.
		/// </summary>
		public int BufferSize
		{
			get
			{
				lock (_syncObject)
					return _bufferSize;
			}
			set
			{
				lock (_syncObject)
				{
					if (value < 0)
						value = 0;

					if (value > _bufferSize)
						_bufferSize = value;
					else
					{
						// new buffer size is smaller

						// remove those results that cannot fit new size
						for (int diff = _currentSize - value; diff > 0; )
						{
							Entry e = _buffer[0];

							if (e._count <= diff)
							{
								// entire combined entry cannot fit	new size

								// remove the entry complitely
								_buffer.RemoveAt(0);
								DecCount(e._received, e._count);

								diff -= e._count;
							}
							else
							{
								// only part of the combined entry cannot fit new size

								// remove only some	results from the entry
								e._count -= diff;
								DecCount(e._received, diff);

								diff = 0;
							}
						}

						if (_currentSize > value)
							_currentSize = value;

						_bufferSize = value;
					}
				}
			}
		}

		#endregion

		#region CurrentSize

		/// <summary>
		/// Number of results currently stored in the buffer.
		/// </summary>
		private int _currentSize = 0;

		/// <summary>
		/// Number of results currently stored in the buffer.
		/// </summary>
		public int CurrentSize
		{
			get
			{
				lock (_syncObject)
					return _currentSize;
			}
		}

		#endregion

		#region Statistics

		#region LostCount

		/// <summary>
		/// Number of unresponded pings.
		/// </summary>
		private int _lostCount;

		/// <summary>
		/// Number of unresponded pings.
		/// </summary>
		public int LostCount
		{
			get
			{
				lock (_syncObject)
					return _lostCount;
			}
		}

		#endregion

		#region LostCountPercent

		/// <summary>
		/// Percent of unresponded pings.
		/// </summary>
		public float LostCountPercent
		{
			get
			{
				lock (_syncObject)
					return (float)_lostCount / _currentSize * 100;
			}
		}

		#endregion

		#region ReceivedCount

		/// <summary>
		/// Number of successful pings.
		/// </summary>
		private int _receivedCount;

		/// <summary>
		/// Number of successful pings.
		/// </summary>
		public int ReceivedCount
		{
			get
			{
				lock (_syncObject)
					return _receivedCount;
			}
		}

		#endregion

		#region ReceivedCountPercent

		/// <summary>
		/// Percent of successful pings.
		/// </summary>
		public float ReceivedCountPercent
		{
			get
			{
				lock (_syncObject)
					return (float)_receivedCount / _currentSize * 100;
			}
		}

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes recent history buffer with specified size.
		/// </summary>
		/// <param name="size">Number of results of most recent pings that are stored in the buffer.</param>
		public PingResultsBuffer(int size)
		{
			_bufferSize = size;
		}

		public PingResultsBuffer() { }

		#endregion

		#region Counting

		/// <summary>
		/// Increments number of results od specific type.
		/// </summary>
		/// <param name="received">whether to increment number of successful or unsuccessful ping results.</param>
		private void IncCount(bool received)
		{
			if (received)
				_receivedCount++;
			else
				_lostCount++;
		}

		/// <summary>
		/// Decrements number of results od specific type.
		/// </summary>
		/// <param name="received">whether to increment number of successful or unsuccessful ping results.</param>
		/// <param name="count">number by which the number of results are decremented.</param>
		private void DecCount(bool received, int count)
		{
			if (received)
				_receivedCount -= count;
			else
				_lostCount -= count;
		}

		#endregion

		#region AddPingResult

		/// <summary>
		/// Inserts ping results into recent history buffer.
		/// </summary>
		/// <param name="received">whether the ping was successful or not.</param>
		public void AddPingResult(bool received)
		{
			if (_bufferSize < 1)
				return;

			lock (_syncObject)
			{
				// buffer is not full yet?
				if (_currentSize < _bufferSize)
				{
					if (_currentSize == 0)
					{
						// empty buffer create first entry

						_buffer.Add(new Entry(received, 1));
						IncCount(received);
						_currentSize++;
						return;
					}

					_currentSize++;
				}
				else
				{
					// buffer is full

					// remove the oldes result
					Entry first = _buffer[0];
					first._count--;
					DecCount(first._received, 1);

					// if the last result was occpied entire combined entry
					/// remove that entry
					if (first._count == 0)
						_buffer.RemoveAt(0);
				}

				// insert new result
				Entry last = _buffer[_buffer.Count - 1];
				if (last._received == received)
					// the newest result can be combined with newest combined entry
					last._count++;
				else
					// the newest result cannot be combined with newest combined entry 
					// create new entry and add it tho the buffer
					_buffer.Add(new Entry(received, 1));

				// increment number of results os specific type
				IncCount(received);
			}
		}

		#endregion

		#region Clear

		/// <summary>
		/// Clears results of most recent pings.
		/// </summary>
		public void Clear()
		{
			lock (_syncObject)
			{
				_currentSize = 0;
				_lostCount = 0;
				_receivedCount = 0;
				_buffer.Clear();
			}
		}

		#endregion

	}

	#endregion

	#region HostPinger

	/// <summary>
	/// Stores information about host and ping options, performs pinging, statistical calculations and stors statistics.
	/// </summary>
	public class HostPinger
	{

		#region Public Constants

		/// <summary>
		/// Number of defined host statuses.
		/// </summary>
		public const int NUMBER_OF_STATUSES = 4;

		/// <summary>
		/// Name of XML element that contains data of the host.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_HOST = "host";

		#endregion

		#region ID

		/// <summary>
		/// Name of XML element that stores IP address of the host.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_ID = "id";

		/// <summary>
		/// ID of the host that is automatically assigned.
		/// </summary>
		private int _id;

		/// <summary>
		/// ID of the host that is automatically assigned.
		/// </summary>
		public int ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Protects ID assigning process from concurent access.
		/// </summary>
		private static object _idLock = new object();

		/// <summary>
		/// ID that will be assigned to new host.
		/// </summary>
		private static int _nextID;

		/// <summary>
		/// Assigns ID to the host automatically.
		/// </summary>
		private void AssignID()
		{
			lock (_idLock)
				_id = _nextID++;
		}

		/// <summary>
		/// Updates ID tracker with ID of loaded host.
		/// </summary>
		/// <param name="id">ID of loaded host.</param>
		private static void UpdateIDTrack(int id)
		{
			lock (_idLock)
			{
				if (_nextID <= id)
					_nextID = id + 1;
			}
		}

		#endregion

		#region Host Infromation

		#region IPAddres

		/// <summary>
		/// Name of XML element that stores IP address of the host.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_HOST_IP = "ip";

		/// <summary>
		/// Host's IP address.
		/// </summary>
		private IPAddress _hostIP;

		/// <summary>
		/// Host's IP address.
		/// </summary>
		public IPAddress HostIP
		{
			get
			{
				lock (_syncObject)
					return _hostIP != null ? _hostIP : new IPAddress(0);
			}
			set
			{
				lock (_syncObject)
				{
					_hostIP = value;

					// successfully obtained IP address from previously unavailable DNS
					if (_status == HostStatus.DnsError)
						Status = HostStatus.Unknown;
				}
			}
		}

		#endregion

		#region HostName

		/// <summary>
		/// Name of XML element that stores name of the host.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_HOST_NAME = "name";

		/// <summary>
		/// Name of the host.
		/// </summary>
		private string _hostName = string.Empty;

		/// <summary>
		/// Name of the host.
		/// </summary>
		public string HostName
		{
			get
			{
				lock (_syncObject)
					return _hostName;
			}
			set
			{
				lock (_syncObject)
					_hostName = value;
			}
		}

		#endregion

		#region HostDescription

		/// <summary>
		/// Name of XML element that stores description of the host.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_HOST_DESCRIPTION = "description";

		/// <summary>
		/// Description of the host.
		/// </summary>
		private string _hostDescription = string.Empty;

		/// <summary>
		/// Description of the host.
		/// </summary>
		public string HostDescription
		{
			get
			{
				lock (_syncObject)
					return _hostDescription;
			}
			set
			{
				lock (_syncObject)
					_hostDescription = value;
			}
		}

		#endregion

		#endregion

		#region Status

		/// <summary>
		/// Current status of the host.
		/// </summary>
		private HostStatus _status = HostStatus.Unknown;

		/// <summary>
		/// Current status of the host.
		/// </summary>
		public HostStatus Status
		{
			get
			{
				lock (_syncObject)
					return _isRunning ? _status : HostStatus.Unknown;
			}

			private set
			{
				// no change to the status?
				if (_status == value && _status != HostStatus.Unknown)
					// no need to perform calculation about status durations
					return;

				DateTime now = DateTime.Now;

				// duration of the old status
				TimeSpan duration = now - _statusReachedAt;

				// duration of the old status is valid only if the pinging was active during that time
				if (_isRunning)
					_statusDurations[(int)_status] += duration;

				// save time when the new status is reached
				_statusReachedAt = now;

				HostStatus old = _status;
				_status = value;

				// notify listeners that status of the host is changed
				ThreadPool.QueueUserWorkItem(new WaitCallback(RaiseOnStatusChange),
					new OnHostStatusChangeParams(old, _status));
			}
		}

		/// <summary>
		/// Name of current state of the host;
		/// </summary>
		public string StatusName
		{
			get
			{
				HostStatus status = Status;
				switch (status)
				{
					case HostStatus.Dead:
						return "Dead";
					case HostStatus.Alive:
						return "Alive";
					case HostStatus.DnsError:
						return "Dns Error";
				}

				return "Unknown";
			}
		}

		#endregion

		#region Statistics

		#region Counting Statistics

		private int _continousPacketLost = 0;

		#region SentPakets

		private int _sentPackets;

		public int SentPackets
		{
			get
			{
				lock (_syncObject)
					return _sentPackets;
			}
		}

		#endregion

		#region ReceivedPackets

		private int _receivedPackets;

		public int ReceivedPackets
		{
			get
			{
				lock (_syncObject)
					return _receivedPackets;
			}
		}

		#endregion

		#region ReceivedPacketsPercent

		public float ReceivedPacketsPercent
		{
			get
			{
				lock (_syncObject)
					return (float)_receivedPackets / _sentPackets * 100;
			}
		}

		#endregion

		#region LostPackets

		private int _lostPackets;

		public int LostPackets
		{
			get
			{
				lock (_syncObject)
					return _lostPackets;
			}
		}

		#endregion

		#region LostPacketsPercent

		public float LostPacketsPercent
		{
			get
			{
				lock (_syncObject)
					return (float)_lostPackets / _sentPackets * 100;
			}
		}

		#endregion

		#region LastPacketLost

		private bool _lastPacketLost = false;

		public bool LastPacketLost
		{
			get
			{
				lock (_syncObject)
					return _lastPacketLost;
			}
		}

		#endregion

		#region ConsecutivePacketsLost

		private int _consecutivePacketsLost;

		public int ConsecutivePacketsLost
		{
			get
			{
				lock (_syncObject)
					return _consecutivePacketsLost;
			}
		}

		#endregion

		#region MaxConsecutivePacketsLost

		private int _maxConsecutivePacketsLost = 0;

		public int MaxConsecutivePacketsLost
		{
			get
			{
				lock (_syncObject)
					return _maxConsecutivePacketsLost;
			}
		}

		#endregion

		#region Recent History

		#region RecentlyReceivedPackets

		public int RecentlyReceivedPackets
		{
			get
			{
				lock (_syncObject)
					return _recentHistory.ReceivedCount;
			}
		}

		#endregion

		#region RecentlyReceivedPacketsPercent

		public float RecentlyReceivedPacketsPercent
		{
			get
			{
				lock (_syncObject)
					return _recentHistory.ReceivedCountPercent;
			}
		}

		#endregion

		#region RecentlyLostPackets

		public int RecentlyLostPackets
		{
			get
			{
				lock (_syncObject)
					return _recentHistory.LostCount;
			}
		}

		#endregion

		#region RecentlyLostPacketsPercent

		public float RecentlyLostPacketsPercent
		{
			get
			{
				lock (_syncObject)
					return _recentHistory.LostCountPercent;
			}
		}

		#endregion

		#endregion

		#endregion

		#region Time Statistics

		#region CurrentResponseTime

		/// <summary>
		/// Response time of the last successfull ping.
		/// </summary>
		private long _currentResponseTime;

		/// <summary>
		/// Response time of the last successfull ping.
		/// </summary>
		public long CurrentResponseTime
		{
			get
			{
				lock (_syncObject)
					return _currentResponseTime;
			}
		}

		#endregion

		#region AverageResponseTime

		/// <summary>
		/// Sum of response times of all successfull pings.
		/// </summary>
		private long _totalResponseTime = 0;

		/// <summary>
		/// Calculates and returns average response time of the host.
		/// </summary>
		public float AverageResponseTime
		{
			get
			{
				lock (_syncObject)
					return _receivedPackets != 0 ? (float)_totalResponseTime / _receivedPackets : 0;
			}
		}

		#endregion

		#region MinResponseTime

		private long _minResponseTime = long.MaxValue;

		public long MinResponseTime
		{
			get
			{
				lock (_syncObject)
					return _minResponseTime;
			}
		}

		#endregion

		#region MaxResponseTime

		private long _maxResponseTime = 0;

		public long MaxResponseTime
		{
			get
			{
				lock (_syncObject)
					return _maxResponseTime;
			}
		}

		#endregion

		#region CurrentStatusDuration

		/// <summary>
		/// Time when the current status is reached.
		/// </summary>
		private DateTime _statusReachedAt = DateTime.Now;

		/// <summary>
		/// Duration of the current status. 
		/// </summary>
		public TimeSpan CurrentStatusDuration
		{
			get
			{
				lock (_syncObject)
					return DateTime.Now.Subtract(_statusReachedAt);
			}
		}

		#endregion

		#region StatusDurations

		/// <summary>
		/// Array of the durations of each status.
		/// </summary>
		private TimeSpan[] _statusDurations = new TimeSpan[NUMBER_OF_STATUSES];

		/// <summary>
		/// Method returns duration of specific status.
		/// </summary>
		/// <param name="status">status whose duration is queried.</param>
		/// <returns>Returns duration of specific status.</returns>
		public TimeSpan GetStatusDuration(HostStatus status)
		{
			lock (_syncObject)
			{
				TimeSpan duration = _statusDurations[(int)status];
				if (_status == status && _isRunning)
					duration += DateTime.Now - _statusReachedAt;

				return duration;
			}
		}

		#endregion

		#region HostAvailability

		/// <summary>
		/// Calculates and returns availability of the host in percents.
		/// </summary>
		public float HostAvailability
		{
			get
			{
				lock (_syncObject)
				{
					// total test time
					long total = _totalTestDuration.Ticks;

					// total available time
					long available = _statusDurations[(int)HostStatus.Alive].Ticks;

					// include current status duration
					if (_isRunning)
					{
						DateTime now = DateTime.Now;

						// current status duration
						total += now.Subtract(_startTime).Ticks;

						// include to total available time if the current state is alive
						if (_status == HostStatus.Alive)
							available += (now - _statusReachedAt).Ticks;
					}

					// calculate and returns availability in parecents
					return (float)((double)available / total * 100);
				}
			}
		}

		#endregion

		#region Test Durations

		/// <summary>
		/// Time when the pinging was started.
		/// </summary>
		private DateTime _startTime = DateTime.Now;

		#region CurrentTestDuration

		/// <summary>
		/// Time that has passed since the current pinging is started.
		/// </summary>
		public TimeSpan CurrentTestDuration
		{
			get
			{
				lock (_syncObject)
				{
					return _isRunning ? DateTime.Now.Subtract(_startTime) : new TimeSpan(0);
				}
			}
		}

		#endregion

		#region TotalTestDuration

		/// <summary>
		/// Total duration of the pingign since the program was started.
		/// </summary>
		private TimeSpan _totalTestDuration;

		/// <summary>
		/// Total duration of the pingign since the program was started.
		/// </summary>
		public TimeSpan TotalTestDuration
		{
			get
			{
				lock (_syncObject)
				{
					return _isRunning
						? _totalTestDuration + DateTime.Now.Subtract(_startTime)
						: _totalTestDuration;
				}
			}
		}

		#endregion

		#endregion

		#endregion

		#region Counting

		/// <summary>
		/// Increments number of lost packets and change status of the host if enough successive packets are lost.
		/// </summary>
		private void IncLost()
		{
			_sentPackets++;
			_lostPackets++;
			_consecutivePacketsLost++;
			_lastPacketLost = true;

			if (_consecutivePacketsLost > _maxConsecutivePacketsLost)
				_maxConsecutivePacketsLost = _consecutivePacketsLost;

			_recentHistory.AddPingResult(false);

			// enough packets has been lost so we can assume that the host is dead 
			if (++_continousPacketLost > _pingsBeforeDead && _status != HostStatus.Dead)
				Status = HostStatus.Dead;
		}

		/// <summary>
		/// Increments number of responded pings and sets status of the host to <see cref="Alive"/>.
		/// </summary>
		/// <param name="time">response time.</param>
		private void IncReceived(long time)
		{
			_sentPackets++;
			_receivedPackets++;
			_consecutivePacketsLost = 0;
			_lastPacketLost = false;

			_recentHistory.AddPingResult(true);

			_totalResponseTime += time;
			_currentResponseTime = time;

			if (time > _maxResponseTime)
				_maxResponseTime = time;

			if (time < _minResponseTime)
				_minResponseTime = time;

			// restarts counter
			_continousPacketLost = 0;

			if (_status != HostStatus.Alive)
				Status = HostStatus.Alive;
		}

		#endregion

		#region ClearStatistics

		/// <summary>
		/// Clears statistics.
		/// </summary>
		/// <param name="clearTimes">if this parameter is set to <c>true</c> this method also clears time statistics
		/// such as durations of specific statuses and test durations.</param>
		public void ClearStatistics(bool clearTimes)
		{
			lock (_syncObject)
			{
				_sentPackets = 0;
				_receivedPackets = 0;
				_lostPackets = 0;
				_currentResponseTime = 0;
				_totalResponseTime = 0;
				_minResponseTime = long.MaxValue;
				_maxResponseTime = 0;
				_continousPacketLost = 0;
				_consecutivePacketsLost = 0;
				_maxConsecutivePacketsLost = 0;
				_lastPacketLost = false;

				_recentHistory.Clear();

				if (clearTimes)
				{
					_statusReachedAt = DateTime.Now;
					_startTime = DateTime.Now;

					for (int i = _statusDurations.Length - 1; i >= 0; i--)
						_statusDurations[i] = new TimeSpan(0);
				}
			}
		}

		#endregion

		#endregion

		#region Options

		#region TTL

		/// <summary>
		/// Name of XML element that stores TTL parameter.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_TTL = "ttl";

		/// <summary>
		/// Default Time-To-Live.
		/// </summary>
		public static readonly int DEFAULT_TTL = 32;

		/// <summary>
		/// Time-to-live for echo message.
		/// </summary>
		private int _ttl = DEFAULT_TTL;

		/// <summary>
		/// Time-to-live for echo message.
		/// </summary>
		public int TTL
		{
			get
			{
				lock (_syncObject)
					return _ttl;
			}
			set
			{
				lock (_syncObject)
				{
					if (value > 0 && value != _ttl)
					{
						_ttl = value;
						_pingerOptions.Ttl = value;
					}
				}
			}
		}

		#endregion

		#region DontFragment

		/// <summary>
		/// Name of XML element that stores state of 'do not fragment' flag.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_FRAGMENT = "dontfragment";

		/// <summary>
		/// Default state of 'do not fragment' flag.
		/// </summary>
		public static readonly bool DEFALUT_FRAGMENT = false;

		/// <summary>
		/// Stat of 'do not fragment' flag of IP header.
		/// </summary>
		private bool _dontFragment;

		/// <summary>
		/// Stat of 'do not fragment' flag of IP header.
		/// </summary>
		public bool DontFragment
		{
			get
			{
				lock (_syncObject)
					return _dontFragment;
			}
			set
			{
				lock (_syncObject)
				{
					if (value != _dontFragment)
					{
						_dontFragment = value;
						_pingerOptions.DontFragment = value;
					}
				}
			}
		}

		#endregion

		#region BufferSize

		/// <summary>
		/// Name of XML element that stores size of data sent in a single echo message.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_BUFFER_SIZE = "buffersize";

		/// <summary>
		/// Default size of data sent in a single echo message (in bytes).
		/// </summary>
		public static readonly int DEFAULT_BUFFER_SIZE = 32;

		/// <summary>
		/// Size of data sent in a single echo message (in bytes).
		/// </summary>
		private int _bufferSize = DEFAULT_BUFFER_SIZE;

		/// <summary>
		/// Size of data sent in a single echo message (in bytes).
		/// </summary>
		public int BufferSize
		{
			get
			{
				lock (_syncObject)
					return _bufferSize;
			}
			set
			{
				lock (_syncObject)
				{
					if (value > 0)
					{
						_bufferSize = value;
						_buffer = new byte[value];
					}
				}
			}
		}

		#endregion

		#region Timeout

		/// <summary>
		/// Name of XML element that stores timeout period for echo message.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_TIMEOUT = "timeout";

		/// <summary>
		/// Default timeout period for the echo message.
		/// </summary>
		public static readonly int DEFAULT_TIMEOUT = 2000;

		/// <summary>
		/// Timeout period for the message.
		/// </summary>
		private int _timeout = DEFAULT_TIMEOUT;

		/// <summary>
		/// Timeout period for the message.
		/// </summary>
		public int Timeout
		{
			get
			{
				lock (_syncObject)
					return _timeout;
			}
			set
			{
				lock (_syncObject)
					_timeout = value;
			}
		}


		#endregion

		#region PingInterval

		/// <summary>
		/// Name of XML element that stores interval duration between end of processing
		/// of the previous message and sending of new echo message.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_PING_INTERVAL = "interval";

		/// <summary>
		/// Default duration interval between end of processing of the previous message
		/// and sending of new echo message (in milliseconds).
		/// </summary>
		public static readonly int DEFAULT_PING_INTERVAL = 1000;

		/// <summary>
		/// Duration interval between end of processing of the previous message and sending of new echo message (in milliseconds).
		/// </summary>
		private int _pingInterval = DEFAULT_PING_INTERVAL;

		/// <summary>
		/// Duration interval between end of processing of the previous message and sending of new echo message (in milliseconds).
		/// </summary>
		public int PingInterval
		{
			get
			{
				lock (_syncObject)
					return _pingInterval;
			}
			set
			{
				lock (_syncObject)
					_pingInterval = value;
			}
		}

		#endregion

		#region DnsQueryInterval

		/// <summary>
		/// Name of XML element that stores interval duration between DNS quiries while pinger tries to obtain IP address.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_DNS_QUERY_INTERVAL = "dnsinterval";

		/// <summary>
		/// Default duration interval between DNS quiries 
		/// while pinger tries to obtain IP address (in milliseconds).
		/// </summary>
		public static readonly int DEFAULT_DNS_QUERY_INTERVAL = 60000;

		/// <summary>
		/// Interval duration between DNS quiries while pinger tries to obtain IP address (in milliseconds).
		/// </summary>
		private int _dnsQueryInterval = DEFAULT_DNS_QUERY_INTERVAL;

		/// <summary>
		/// Interval duration between DNS quiries while pinger tries to obtain IP address (in milliseconds).
		/// </summary>
		public int DnsQueryInterval
		{
			get
			{
				lock (_syncObject)
					return _dnsQueryInterval;
			}

			set
			{
				lock (_syncObject)
					_dnsQueryInterval = value;
			}
		}

		#endregion

		#region PingsBeforeDead

		/// <summary>
		/// Name of XML element that stores number of packets that should be lost successivly to declare host as dead. 
		/// </summary>
		public readonly string XML_ELEMENT_NAME_PINGS_BEFORE_DEAD = "pingsbeforedead";

		/// <summary>
		/// Default number of packets that should be lost successivly to declare host as dead.
		/// </summary>
		public static readonly int DEFALUT_PINGS_BEFORE_DEAD = 10;

		/// <summary>
		/// Number of packets that should be lost successivly to declare host as dead.
		/// </summary>
		private int _pingsBeforeDead = DEFALUT_PINGS_BEFORE_DEAD;

		/// <summary>
		/// Number of packets that should be lost successivly to declare host as dead.
		/// </summary>
		public int PingsBeforeDead
		{
			get
			{
				lock (_syncObject)
					return _pingsBeforeDead;
			}
			set
			{
				lock (_syncObject)
					_pingsBeforeDead = value;
			}
		}

		#endregion

		#region RecentHisoryDepth

		/// <summary>
		/// Name of XML element that stores depth of recent history.
		/// </summary>
		public readonly string XML_ELEMENT_NAME_RECENT_HISTORY_DEPTH = "recenthistorydepth";

		/// <summary>
		/// Depth of recent history (number of results of previous pings that are stored in history buffer).
		/// </summary>
		public int RecentHisoryDepth
		{
			get
			{
				lock (_syncObject)
					return _recentHistory.BufferSize;
			}

			set
			{
				lock (_syncObject)
					_recentHistory.BufferSize = value;
			}
		}

		#endregion

		#endregion

		#region Private Attributes

		/// <summary>
		/// Buffer that is sent as echo message.
		/// </summary>
		byte[] _buffer = new byte[DEFAULT_BUFFER_SIZE];

		/// <summary>
		/// Timer used to initiate ping.
		/// </summary>
		System.Timers.Timer _timer = new System.Timers.Timer();

		/// <summary>
		/// Ping object used for pinging the host.
		/// </summary>
		Ping _pinger = new Ping();

		/// <summary>
		/// Stores options of ping object (options of the echo message).
		/// </summary>
		PingOptions _pingerOptions = new PingOptions(DEFAULT_TTL, DEFALUT_FRAGMENT);

		/// <summary>
		/// Object that stores only the recent history of pinging.
		/// </summary>
		private PingResultsBuffer _recentHistory = new PingResultsBuffer();

		#region Synchronization Object

		object _syncObject = new object();

		#endregion

		#endregion

		#region Logger

		/// <summary>
		/// Logger used to log host changes and commands issued to this host pinger.
		/// </summary>
		private IPingLogger _logger = null;

		/// <summary>
		/// Logger used to log host changes and commands issued to this host pinger.
		/// </summary>
		public IPingLogger Logger
		{
			get
			{
				lock (_syncObject)
					return _logger;
			}
			set
			{
				lock (_syncObject)
					_logger = value;
			}
		}

		#endregion

		#region Events

		#region OnPing

		/// <summary>
		/// This event is raised when the single ping is completed.
		/// </summary>
		public event OnPingDelegate OnPing;

		/// <summary>
		/// Raise <see cref="OnPing"/> event if there is subscribed listeners.
		/// </summary>
		private void RaiseOnPing()
		{
			if (OnPing != null)
				OnPing(this);
		}

		#endregion

		#region OnStatusChange

		#region OnHostStatusChangeParams

		/// <summary>
		/// This class stores data that are passed to listeners of <see cref="OnStatusChange"/> event.
		/// </summary>
		private class OnHostStatusChangeParams
		{
			/// <summary>
			/// Previous status of the host.
			/// </summary>
			public HostStatus _oldState;

			/// <summary>
			/// New status of the host.
			/// </summary>
			public HostStatus _newState;

			/// <summary>
			/// Initialize parameters.
			/// </summary>
			/// <param name="oldStatus">previous status of the host.</param>
			/// <param name="newStatus">new status of the host.</param>
			public OnHostStatusChangeParams(HostStatus oldStatus, HostStatus newStatus)
			{
				_oldState = oldStatus;
				_newState = newStatus;
			}
		}

		#endregion

		/// <summary>
		/// This event is raised when the status of the host is changed.
		/// </summary>
		public event OnHostStatusChangeDelegate OnStatusChange;

		/// <summary>
		/// Raise <see cref="OnStatusChange"/> event if there is subscribed listeners.
		/// </summary>
		/// <param name="param">data that are passed to listeners of <see cref="OnStatusChange"/>event (old and new statuses).</param>
		private void RaiseOnStatusChange(object param)
		{
			OnHostStatusChangeParams p = (OnHostStatusChangeParams)param;

			// log status change
			if (_logger != null)
				_logger.LogStatusChange(this, p._oldState, p._newState);

			if (OnStatusChange != null)
				OnStatusChange(this, p._oldState, p._newState);
		}

		#endregion

		#region OnStartPinging

		/// <summary>
		/// This event is raised when user starts pinging.
		/// </summary>
		public event OnHostPingerCommandDelegate OnStartPinging;

		/// <summary>
		/// Raise <see cref="OnStartPinging"/> event if there is subscribed listeners.
		/// </summary>
		private void RaiseOnStartPinging()
		{
			// log start command
			if (_logger != null)
				_logger.LogStart(this);

			if (OnStartPinging != null)
				OnStartPinging(this);
		}

		#endregion

		#region OnStopPinging

		/// <summary>
		/// This event is raised when user stops pinging.
		/// </summary>
		public event OnHostPingerCommandDelegate OnStopPinging;

		/// <summary>
		/// Raise <see cref="OnStopPinging"/> event if there is subscribed listeners.
		/// </summary>
		private void RaiseOnStopPinging()
		{
			// log stop command
			if (_logger != null)
				_logger.LogStop(this);

			if (OnStopPinging != null)
				OnStopPinging(this);
		}

		#endregion

		#region OnHostNameChanged

		/// <summary>
		/// This event is raised when user changes host's name.
		/// </summary>
		public event OnHostNameChangedDelegate OnHostNameChanged;

		/// <summary>
		/// Raise <see cref="OnHostNameChanged"/> event if there is subscribed listeners.
		/// </summary>
		private void RaiseOnHostNameChanged()
		{
			if (OnHostNameChanged != null)
				OnHostNameChanged(this);
		}

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes pinger that pings localhost (127.0.0.1)
		/// </summary>
		public HostPinger()
		{
			AssignID();

			_hostIP = new IPAddress(new byte[] { 127, 0, 0, 1 });
			_hostName = "localhost";
		}

		/// <summary>
		/// Initializes host pinger form an XML file. 
		/// </summary>
		/// <param name="xmlNode">XML node that stores informations about host and ping options.</param>
		public HostPinger(XmlNode xmlNode)
		{
			if (xmlNode[XML_ELEMENT_NAME_ID] != null)
			{
				_id = int.Parse(xmlNode[XML_ELEMENT_NAME_ID].InnerText);
				UpdateIDTrack(_id);
			}
			else
				AssignID();

			if (xmlNode[XML_ELEMENT_NAME_HOST_NAME] != null)
				HostName = xmlNode[XML_ELEMENT_NAME_HOST_NAME].InnerText;
			else
				HostName = "No Name";

			if (xmlNode[XML_ELEMENT_NAME_HOST_IP] != null)
				HostIP = IPAddress.Parse(xmlNode[XML_ELEMENT_NAME_HOST_IP].InnerText);
			else
			{
				try
				{
					_hostIP = GetHostIpByName(_hostName);
				}
				catch
				{
					Status = HostStatus.DnsError;
				}
			}

			if (xmlNode[XML_ELEMENT_NAME_HOST_DESCRIPTION] != null)
				HostDescription = xmlNode[XML_ELEMENT_NAME_HOST_DESCRIPTION].InnerText;

			if (xmlNode[XML_ELEMENT_NAME_TIMEOUT] != null)
				Timeout = int.Parse(xmlNode[XML_ELEMENT_NAME_TIMEOUT].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_PING_INTERVAL] != null)
				PingInterval = int.Parse(xmlNode[XML_ELEMENT_NAME_PING_INTERVAL].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_DNS_QUERY_INTERVAL] != null)
				DnsQueryInterval = int.Parse(xmlNode[XML_ELEMENT_NAME_DNS_QUERY_INTERVAL].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_PINGS_BEFORE_DEAD] != null)
				PingsBeforeDead = int.Parse(xmlNode[XML_ELEMENT_NAME_PINGS_BEFORE_DEAD].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_BUFFER_SIZE] != null)
				BufferSize = int.Parse(xmlNode[XML_ELEMENT_NAME_BUFFER_SIZE].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_RECENT_HISTORY_DEPTH] != null)
				RecentHisoryDepth = int.Parse(xmlNode[XML_ELEMENT_NAME_RECENT_HISTORY_DEPTH].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_TTL] != null)
				TTL = int.Parse(xmlNode[XML_ELEMENT_NAME_TTL].InnerText);

			if (xmlNode[XML_ELEMENT_NAME_FRAGMENT] != null)
				DontFragment = bool.Parse(xmlNode[XML_ELEMENT_NAME_FRAGMENT].InnerText);

			InitTimer();
		}

		/// <summary>
		/// Initialize host pinger with host name. This constructor sends DNS query to obtain IP address.
		/// </summary>
		/// <param name="hostName">name of the host.</param>
		public HostPinger(string hostName)
		{
			AssignID();

			_hostName = hostName;

			try
			{
				_hostIP = GetHostIpByName(_hostName);
			}
			catch
			{
				Status = HostStatus.DnsError;
			}

			InitTimer();
		}

		/// <summary>
		/// Initializes host pinger with IP address of the host.
		/// </summary>
		/// <param name="address">IP address of the host.</param>
		public HostPinger(IPAddress address)
		{
			AssignID();

			_hostName = "No Name";
			_hostIP = address;

			InitTimer();
		}

		/// <summary>
		/// Initializes host pinger with host name and IP address. Host name is only used for displaying.
		/// </summary>
		/// <param name="hostName">display name of the host.</param>
		/// <param name="address">IP address of the host.</param>
		public HostPinger(string hostName, IPAddress address)
		{
			AssignID();

			_hostName = hostName;
			_hostIP = address;

			InitTimer();
		}

		/// <summary>
		/// This method sends DNS query to obtain IP address of the host with specified name.
		/// </summary>
		/// <param name="name">host name.</param>
		/// <returns>Method returns IP address of the host.</returns>
		private IPAddress GetHostIpByName(string name)
		{
			IPHostEntry dnse;
			try
			{
				dnse = Dns.GetHostEntry(_hostName);
			}
			catch (Exception ex)
			{
				throw new Exception("Error connecting DNS for " + _hostName + " host", ex);
			}

			if (dnse != null)
				return dnse.AddressList[0];
			else
				throw new Exception("Cannot resolve host \"" + _hostName + "\" IP by its name.");
		}

		#endregion

		#region Save

		/// <summary>
		/// Delegate for methods that stores additional data to host configuration.
		/// </summary>
		/// <param name="writer">XML writer used to write host information and ping options.</param>
		public delegate void AdditionalSettingsSave(XmlWriter writer);

		/// <summary>
		/// Saves current host information and ping options to provided file.
		/// </summary>
		/// <param name="writer">XML writer used to write host information and ping options.</param>
		/// <param name="additionalSettings">method that stores additional data to host configuration. This parameter can be <c>null</c>.</param>
		public void Save(XmlWriter writer, AdditionalSettingsSave additionalSettings)
		{
			writer.WriteStartElement(XML_ELEMENT_NAME_HOST);

			lock (_syncObject)
			{
				writer.WriteElementString(XML_ELEMENT_NAME_HOST_NAME, _hostName);
				if (_hostIP != null)
					writer.WriteElementString(XML_ELEMENT_NAME_HOST_IP, _hostIP.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_HOST_DESCRIPTION, _hostDescription);
				writer.WriteElementString(XML_ELEMENT_NAME_TIMEOUT, _timeout.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_PING_INTERVAL, _pingInterval.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_DNS_QUERY_INTERVAL, _dnsQueryInterval.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_PINGS_BEFORE_DEAD, _pingsBeforeDead.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_RECENT_HISTORY_DEPTH, _recentHistory.BufferSize.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_BUFFER_SIZE, _bufferSize.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_TTL, _ttl.ToString());
				writer.WriteElementString(XML_ELEMENT_NAME_FRAGMENT, _dontFragment.ToString());

				if (additionalSettings != null)
					additionalSettings(writer);
			}

			writer.WriteEndElement();
		}

		/// <summary>
		/// Saves current host information and ping options to provided file.
		/// </summary>
		/// <param name="writer">XML writer used to write host information and ping options.</param>
		public void Save(XmlWriter writer) { Save(writer, null); }

		#endregion

		#region Timer

		/// <summary>
		/// Initializes timer.
		/// </summary>
		void InitTimer()
		{
			_timer.AutoReset = false;
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
		}

		/// <summary>
		/// Handle even raised by the timer when the time elapses
		/// </summary>
		/// <param name="sender">ignored.</param>
		/// <param name="e">ignored.</param>
		void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Pinger();
		}

		#endregion

		#region Control

		#region IsRunning

		/// <summary>
		/// Inidicate whether the pinging is active.
		/// </summary>
		private bool _isRunning;

		/// <summary>
		/// Inidicate whether the pinging is active.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				lock (_syncObject)
					return _isRunning;
			}

			set
			{
				if (value)
					Start();
				else
					Stop();
			}
		}

		#endregion

		/// <summary>
		/// Starts pinging the host. If the pinging is already active this method performs no actions.
		/// </summary>
		public void Start()
		{
			bool started = false;
			Monitor.Enter(_syncObject);
			if (!_isRunning)
			{
				// save the time when the pinging is started
				_startTime = DateTime.Now;

				if (_status != HostStatus.DnsError)
					Status = HostStatus.Unknown;

				started = _isRunning = true;

				// schedule ping if it is not already
				if (!_pingScheduled)
				{
					_pingScheduled = true;
					_timer.Interval = _pingInterval;
					_timer.Start();
				}
			}
			Monitor.Exit(_syncObject);

			if (started)
				RaiseOnStartPinging();
		}

		/// <summary>
		/// Stops pinging the host. If the pinging is not active this method performs no actions.
		/// </summary>
		public void Stop()
		{
			bool stopped = false;

			lock (_syncObject)
			{
				if (_isRunning)
				{
					_continousPacketLost = 0;

					if (_status != HostStatus.DnsError)
						Status = HostStatus.Unknown;

					_totalTestDuration += DateTime.Now - _startTime;

					_isRunning = false;
					stopped = true;
				}
			}

			if (stopped)
				RaiseOnStopPinging();
		}

		#endregion

		#region Pinging

		/// <summary>
		/// Indicates that next ping is scheduled.
		/// </summary>
		private bool _pingScheduled = false;

		/// <summary>
		/// Send ping echo message, waits for the response, performs statistical calculations based on the response and schedule next ping.
		/// </summary>
		private void Pinger()
		{
			bool ping;

			lock (_syncObject)
				// did pinger obtain IP address
				ping = _status != HostStatus.DnsError;

			// pinger has not yet obtained IP address
			if (!ping)
			{
				try
				{
					// send DNS query
					IPAddress addr = GetHostIpByName(HostName);

					lock (_syncObject)
					{
						// IP address obrained sucessfully change host status
						if (_status == HostStatus.DnsError && _isRunning)
						{
							_hostIP = addr;
							Status = HostStatus.Unknown;
						}
					}
				}
				catch
				{
					// DNS query failed and IP address was not obtained
					lock (_syncObject)
					{
						if (_isRunning)
						{
							// schedule next DNS query
							_pingScheduled = true;
							_timer.Interval = _dnsQueryInterval;
							_timer.Start();
						}
						else
							_pingScheduled = false;
					}

					RaiseOnPing();
					return;
				}
			}

			PingReply reply;

			IPAddress ip;
			int timeout;
			byte[] buffer;
			PingOptions options;

			lock (_syncObject)
			{
				// copy ping options
				ip = _hostIP;
				timeout = _timeout;
				buffer = _buffer;
				options = _pingerOptions;
			}

			// send ping message
			reply = _pinger.Send(ip, timeout, buffer, options);

			lock (_syncObject)
			{
				ping = false;

				if (_isRunning)
				{
					if (ip == _hostIP)
					{
						switch (reply.Status)
						{
							#region Checking Reply Status

							case IPStatus.BadDestination:
							case IPStatus.BadHeader:
							case IPStatus.BadOption:
							case IPStatus.BadRoute:
							case IPStatus.UnrecognizedNextHeader:
							case IPStatus.PacketTooBig:
							case IPStatus.ParameterProblem:
								// wrong message format
								IncLost();
								break;

							case IPStatus.DestinationScopeMismatch:
							case IPStatus.Unknown:
							case IPStatus.HardwareError:
							case IPStatus.IcmpError:
							case IPStatus.NoResources:
							case IPStatus.SourceQuench:
								// error
								IncLost();
								break;

							case IPStatus.DestinationHostUnreachable:
							case IPStatus.DestinationNetworkUnreachable:
							case IPStatus.DestinationPortUnreachable:
							case IPStatus.DestinationProhibited:
							case IPStatus.DestinationUnreachable:
								// unreachability of the remote host
								IncLost();
								break;

							case IPStatus.TimeExceeded:
							case IPStatus.TimedOut:
							case IPStatus.TtlExpired:
							case IPStatus.TtlReassemblyTimeExceeded:
								// time outs
								IncLost();
								break;

							case IPStatus.Success:
								// success
								IncReceived(reply.RoundtripTime);
								break;

							default:
								// something went wrong
								IncLost();
								break;

							#endregion
						}

						ping = true;
					}

					// schedule next ping
					_pingScheduled = true;
					_timer.Interval = _pingInterval;
					_timer.Start();
				}
				else
					_pingScheduled = false;
			}

			if (ping)
				RaiseOnPing();
		}

		#endregion

	}

	#endregion

}
