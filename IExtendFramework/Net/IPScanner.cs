
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
using System.Net.Sockets;

namespace IExtendFramework.Net
{

    #region IPScanRange

    public class IPScanRange
    {

        #region Start

        private IPAddress _start;

        public IPAddress Start { get { return _start; } }

        #endregion

        #region End

        private IPAddress _end;

        public IPAddress End { get { return _end; } }

        #endregion

        #region RangeSize

        public ulong RangeSize { get { return Extract(_end) - Extract(_start); } }

        #endregion

        #region Constructors

        public IPScanRange(IPAddress start, IPAddress end) { SetRange(start, end); }

        public IPScanRange(IPAddress start, int subnet) { SetRange(start, subnet); }

        #endregion

        #region GetNext

        public IPAddress GetNext(IPAddress current)
        {
            if (current != null)
            {
                ulong c = Extract(current);
                if (c == Extract(_end))
                    return null;

                return Pack(current.AddressFamily, c + 1);
            }

            return null;
        }

        #endregion

        #region Compare

        public long Compare(IPAddress addr1, IPAddress addr2) { return (long)Extract(addr1) - (long)Extract(addr2); }

        #endregion

        #region GetDistance

        public ulong GetDistance(IPAddress address) { return Extract(address) - Extract(_start); }

        #endregion

        #region SetRange

        public void SetRange(IPAddress start, IPAddress end)
        {
            if (Extract(start) > Extract(end))
                throw new ArgumentException("end");

            _start = start;
            _end = end;
        }

        public void SetRange(IPAddress start, int subnet)
        {
            int lim = start.AddressFamily == AddressFamily.InterNetwork ? 32 : 64;
            if (subnet < 1 || subnet > lim - 1)
                throw new ArgumentOutOfRangeException("subnet");

            ulong end = Extract(start) | ((1UL << (lim - subnet)) - 1);
            SetRange(start, Pack(start.AddressFamily, end));
        }

        #endregion

        #region Address Conversion

        public static ulong Extract(IPAddress ip)
        {
            byte[] bytes = ip.GetAddressBytes();
            Array.Reverse(bytes);

            return bytes.Length == 8 ? BitConverter.ToUInt64(bytes, 0) : BitConverter.ToUInt32(bytes, 0);
        }

        public static IPAddress Pack(AddressFamily family, ulong ip)
        {
            byte[] bytes = family == AddressFamily.InterNetwork ? BitConverter.GetBytes((uint)ip) : BitConverter.GetBytes(ip);
            Array.Reverse(bytes);

            return new IPAddress(bytes);
        }

        #endregion

    }

    #endregion

    #region IPScanHostState

    public class IPScanHostState
    {

        #region States

        public enum State
        {
            Testing = 1,
            Alive = 2,
            Dead = 4
        };

        #endregion

        private object _lock = new object();

        #region Address

        private IPAddress _address;

        public IPAddress Address { get { return _address; } }

        #endregion

        #region ResponseTimes

        private long[] _responsTimes;

        public long[] ResponseTimes { get { return _responsTimes; } }

        #endregion

        #region PingsDone

        private int _pingsCount = 0;

        public int PingsCount { get { return _pingsCount; } }

        #endregion

        #region LossCount

        private int _lossCount = 0;

        public int LossCount { get { return _lossCount; } }

        #endregion

        private void ClearCachedValues() { _quality = _avgResponseTime = -1.0f; }

        #region AvgResponseTime

        float _avgResponseTime = -1.0f;

        public float AvgResponseTime
        {
            get
            {
                lock (_lock)
                {
                    if (_avgResponseTime < 0)
                    {
                        _avgResponseTime = 0;
                        for (int i = _pingsCount - 1; i >= 0; i--)
                            _avgResponseTime += _responsTimes[i];

                        _avgResponseTime /= _pingsCount;
                    }

                    return _avgResponseTime;
                }
            }
        }

        #endregion

        #region Quality

        private int _timeout = 0;

        private float _quality = -1.0f;

        public float Quality
        {
            get
            {
                lock (_lock)
                {
                    if (_quality < 0)
                    {
                        _quality = 0;
                        float qpp = 1.0f / _pingsCount;
                        for (int i = _pingsCount - 1; i >= 0; i--)
                        {
                            if (_responsTimes[i] > 5)
                                _quality += qpp * (1 - (float)Math.Log(_responsTimes[i] / 5, 5 * _timeout));
                            else if (_responsTimes[i] >= 0)
                                _quality += qpp;
                        }
                    }

                    return _quality;
                }
            }
        }

        #endregion

        #region QualityCategory

        public enum QCategory
        {
            VeryPoor,
            Poor,
            Fair,
            Good,
            VeryGood,
            Excellent,
            Perfect
        }

        public QCategory QualityCategory { get { return (QCategory)((int)(((int)QCategory.Perfect) * Quality)); } }

        #endregion

        #region CurrentState

        private bool _alive = false;

        private State _currentState = State.Testing | State.Dead;

        public State CurrentState { get { return _currentState; } }

        private bool SetState(State state)
        {
            bool raise = !IsAlive() && IsAlive(state) || IsAlive() && IsDead(state) || IsTesting() != IsTesting(state);
            _currentState = state;
            return raise;
        }

        #region State Testing

        private bool IsAlive(State state) { return (state & State.Alive) == State.Alive; }

        public bool IsAlive() { return IsAlive(_currentState); }

        private bool IsDead(State state) { return (state & State.Dead) == State.Dead; }

        public bool IsDead() { return IsDead(_currentState); }

        private bool IsTesting(State state) { return (state & State.Testing) == State.Testing; }

        public bool IsTesting() { return IsTesting(_currentState); }

        #endregion

        #region OnStateChange

        public delegate void StateChangeDelegate(IPScanHostState host, State oldState);

        public event StateChangeDelegate OnStateChange;

        private void RaiseOnStateChange(State oldState)
        {
            if (OnStateChange != null)
                OnStateChange(this, oldState);
        }

        #endregion

        #endregion

        #region HostName

        AsyncHostNameResolver _nameResolver = new AsyncHostNameResolver();

        private string _hostName = string.Empty;

        public string HostName { get { return _hostName; } }

        public void StoreHostName(string name)
        {
            lock (_lock)
                _hostName = name;

            if (OnHostNameAvailable != null)
                OnHostNameAvailable(this);
        }

        public delegate void HostNameAvailableDelegate(IPScanHostState host);

        public event HostNameAvailableDelegate OnHostNameAvailable;

        #endregion

        #region Constructors

        public IPScanHostState(IPAddress address, int pingsRequired, int timeout)
        {
            _address = address;
            _timeout = timeout;
            _responsTimes = new long[pingsRequired];
        }

        #endregion

        #region StorePingResults

        public void Prepare()
        {
            lock (_lock)
            {
                ClearCachedValues();

                _hostName = string.Empty;

                _pingsCount = _lossCount = 0;
                _alive = false;
                _currentState |= State.Testing;
            }
        }

        public void StorePingResult() { StorePingResult(-1); }

        public void StorePingResult(long responseTime)
        {
            bool raiseStateChange = false;
            State oldState = State.Testing;

            lock (_lock)
            {
                if (responseTime >= 0)
                {
                    _alive = true;
                    oldState = _currentState;
                    raiseStateChange = SetState(State.Testing | State.Alive);

                    if (raiseStateChange)
                        _nameResolver.ResolveHostName(_address, new AsyncHostNameResolver.StoreHostNameDelegate(StoreHostName));
                }
                else
                    _lossCount++;

                ClearCachedValues();
                _responsTimes[_pingsCount++] = responseTime;

                if (_pingsCount == _responsTimes.Length)
                {
                    oldState = _currentState;
                    raiseStateChange = SetState(_alive ? State.Alive : State.Dead);
                }
            }

            if (raiseStateChange)
                RaiseOnStateChange(oldState);
        }

        #endregion

    }

    #endregion

    #region IPScanner

    public class IPScanner
    {

        #region Locks

        private object _runControlLock = new object();

        private object _resultsLock = new object();

        #endregion

        #region Pingers

        private class PingerEntry
        {

            public Ping _ping = new Ping();

            public IPScanHostState _results = null;

        }

        private PingerEntry[] _pingers = null;

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

        private PingOptions _pingOptions = new PingOptions();

        #region TTL

        public int TTL
        {
            get { return _pingOptions.Ttl; }
            set
            {
                lock (_runControlLock)
                {
                    CheckRunning();
                    _pingOptions.Ttl = value;
                }
            }
        }

        #endregion

        #region DontFragment

        public bool DontFragment
        {
            get { return _pingOptions.DontFragment; }
            set
            {
                lock (_runControlLock)
                {
                    CheckRunning();
                    _pingOptions.DontFragment = value;
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

        #region Scan Settings

        #region ConcurrentPings

        private int _concurrentPings = 1;

        public int ConcurrentPings
        {
            get { return _concurrentPings; }
            set
            {
                lock (_runControlLock)
                {
                    CheckRunning();
                    _concurrentPings = value;
                }
            }
        }

        #endregion

        #region PingsPerScan

        private int _pingsPerScan = 1;

        public int PingsPerScan
        {
            get { return _pingsPerScan; }
            set
            {
                lock (_runControlLock)
                {
                    CheckRunning();
                    _pingsPerScan = value;
                }
            }
        }

        #endregion

        #region ContinuousScan

        private bool _continuousScan = true;

        public bool ContinuousScan
        {
            get { return _continuousScan; }
            set
            {
                lock (_runControlLock)
                {
                    CheckRunning();
                    _continuousScan = value;
                }
            }
        }

        #endregion

        #endregion

        #region Scanner State

        #region Active

        private volatile bool _active = false;

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

        private IPScanRange _range = null;

        private IPAddress _nextToScan = null;

        private volatile int _activePings = 0;

        #endregion

        #region AliveHosts

        private Dictionary<IPAddress, IPScanHostState> _aliveHosts = new Dictionary<IPAddress, IPScanHostState>();

        public Dictionary<IPAddress, IPScanHostState>.ValueCollection AliveHosts { get { return _aliveHosts.Values; } }

        #endregion

        #region Events

        #region OnAliveHostFound

        public delegate void AliveHostFoundDelegate(IPScanner scanner, IPScanHostState host);

        public event AliveHostFoundDelegate OnAliveHostFound;

        private void RaiseOnAliveHostFound(IPScanHostState host)
        {
            if (OnAliveHostFound != null)
                OnAliveHostFound(this, host);
        }

        #endregion

        public delegate void ScanStateChangeDelegate(IPScanner scanner);

        #region OnStartScan

        public event ScanStateChangeDelegate OnStartScan;

        private void RaiseOnStartScan()
        {
            if (OnStartScan != null)
                OnStartScan(this);
        }

        #endregion

        #region OnStopScan

        public event ScanStateChangeDelegate OnStopScan;

        private void RaiseOnStopScan()
        {
            if (OnStopScan != null)
                OnStopScan(this);
        }

        #endregion

        #region OnRestartScan

        public event ScanStateChangeDelegate OnRestartScan;

        private void RaiseOnRestartScan()
        {
            if (OnRestartScan != null)
                OnRestartScan(this);
        }

        #endregion

        #region OnScanProgressUpdate

        public delegate void ScanProgressUpdateDelegate(IPScanner scanner, IPAddress currentAddress, ulong progress, ulong total);

        public event ScanProgressUpdateDelegate OnScanProgressUpdate;

        private ulong _lastUpdate = 0;

        private void RaiseOnScanProgressUpdate(IPAddress currentAddress)
        {
            ulong progress = _range.GetDistance(currentAddress);
            if (progress > _lastUpdate)
            {
                _lastUpdate = progress;

                if (OnScanProgressUpdate != null)
                    OnScanProgressUpdate(this, currentAddress, progress, _range.RangeSize);
            }
        }

        #endregion

        #endregion

        #region Constructors

        public IPScanner() { }

        public IPScanner(int concurrentPings, int pingsPerScan, bool continuousScan)
        {
            _concurrentPings = concurrentPings;
            _pingsPerScan = pingsPerScan;
            _continuousScan = continuousScan;
        }

        public IPScanner(int concurrentPings, int pingsPerScan, bool continuousScan, int timeout) : this(concurrentPings, pingsPerScan, continuousScan) { _timeout = timeout; }

        public IPScanner(int concurrentPings, int pingsPerScan, bool continuousScan, int timeout, int ttl, bool dontFragment, int pingBufferSize)
            : this(concurrentPings, pingsPerScan, continuousScan, timeout)
        {
            TTL = ttl;
            DontFragment = dontFragment;
            PingBufferSize = pingBufferSize;
        }

        #endregion

        #region Scan Control Methods

        public void Start(IPScanRange range)
        {
            lock (_runControlLock)
            {
                CheckRunning();

                _aliveHosts.Clear();

                if (_pingers != null)
                {
                    foreach (PingerEntry p in _pingers)
                        p._ping.PingCompleted -= pinger_PingCompleted;
                }

                _pingers = new PingerEntry[_concurrentPings];
                for (int i = _concurrentPings - 1; i >= 0; i--)
                {
                    _pingers[i] = new PingerEntry();
                    _pingers[i]._ping.PingCompleted += new PingCompletedEventHandler(pinger_PingCompleted);
                }

                _range = range;

                _active = true;
                _stopEvent.Reset();

                RaiseOnStartScan();

                Thread t = new Thread(new ThreadStart(Restart));
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

        private void Restart()
        {
            lock (_resultsLock)
            {
                _lastUpdate = 0;
                _nextToScan = _range.Start;

                foreach (PingerEntry p in _pingers)
                {
                    if (!StartNext(p))
                        break;
                }
            }
        }

        private bool StartNext(PingerEntry pinger)
        {
            if (_nextToScan == null)
                return false;

            if (_aliveHosts.ContainsKey(_nextToScan))
            {
                IPScanHostState results = _aliveHosts[_nextToScan];
                results.Prepare();

                pinger._results = results;
            }
            else
                pinger._results = new IPScanHostState(_nextToScan, _pingsPerScan, _timeout);

            RaiseOnScanProgressUpdate(_nextToScan);

            _nextToScan = _range.GetNext(_nextToScan);

            _activePings++;
            pinger._ping.SendAsync(pinger._results.Address, _timeout, _pingBuffer, _pingOptions, pinger);

            return true;
        }

        private void CheckRunning() { if (_active)throw new InvalidOperationException("Cannot performe this operation while scanner is running!"); }

        #endregion

        #region Ping Handler

        void pinger_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            PingerEntry pinger = (PingerEntry)e.UserState;
            IPScanHostState results = pinger._results;

            bool raiseEvent = false;

            results.StorePingResult(e.Reply.Status == IPStatus.Success ? e.Reply.RoundtripTime : -1);

            lock (_resultsLock)
            {
                if (!results.IsTesting())
                {
                    if (results.IsAlive())
                    {
                        raiseEvent = !_aliveHosts.ContainsKey(results.Address);

                        if (raiseEvent)
                            _aliveHosts.Add(results.Address, results);
                    }
                    else
                        _aliveHosts.Remove(results.Address);

                    --_activePings;

                    if (!_active)
                    {
                        if (_activePings == 0)
                        {
                            _stopEvent.Set();
                            RaiseOnStopScan();
                        }
                    }
                    else
                    {
                        if (_nextToScan == null)
                        {
                            if (_activePings == 0)
                            {
                                if (_continuousScan)
                                {
                                    RaiseOnRestartScan();
                                    Restart();
                                }
                                else
                                {
                                    _active = false;
                                    _stopEvent.Set();

                                    RaiseOnStopScan();
                                }
                            }
                        }
                        else
                            StartNext(pinger);
                    }
                }
                else
                    pinger._ping.SendAsync(pinger._results.Address, _timeout, _pingBuffer, _pingOptions, pinger);
            }

            if (raiseEvent)
                RaiseOnAliveHostFound(results);
        }

        #endregion

    }

    #endregion

}
