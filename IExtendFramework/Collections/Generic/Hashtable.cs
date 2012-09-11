using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IExtendFramework.Collections.Generic
{
    /// <summary>
    /// Hashtable with types
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Hashtable<TValue> : System.Collections.IEnumerable
    {
        System.Collections.Hashtable tbl = new System.Collections.Hashtable();

        public void Add(object key, TValue val)
        {
            tbl.Add(key, val);
        }

        public void Remove(object key)
        {
            tbl.Remove(key);
        }

        public TValue this[object key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                tbl[key] = value;
            }
        }

        public void Clear()
        {
            tbl.Clear();
        }

        TValue Get(object key)
        {
            return (TValue)tbl[key];
        }

        public System.Collections.ICollection Keys
        {
            get
            {
                return tbl.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> vals = new List<TValue>();
                foreach (TValue val in tbl.Values)
                    vals.Add(val);
                return vals;
            }
        }

        public bool Contains(object key)
        {
            return tbl.Contains(key);
        }

        public bool ContainsKey(object key)
        {
            return tbl.ContainsKey(tbl);
        }

        public bool ContainsValue(TValue val)
        {
            return tbl.ContainsValue(val);
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return tbl.GetEnumerator();
        }
    }

    /// <summary>
    /// Hashtable with types
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Hashtable<TKey, TValue> : System.Collections.IEnumerable
    {
        System.Collections.Hashtable tbl = new System.Collections.Hashtable();

        public void Add(TKey key, TValue val)
        {
            tbl.Add(key, val);
        }

        public void Remove(TKey key)
        {
            tbl.Remove(key);
        }

        public TValue this[TKey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                tbl[key] = value;
            }
        }

        /// <summary>
        /// Gets the key corresponding to the specified value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        //public TKey this[TValue val]
        //{
        //    get
        //    {
        //        foreach (TKey k in Keys)
        //            if (tbl[k].Equals(val))
        //                return k;
        //        return default(TKey);
        //    }
        //}

        public void Clear()
        {
            tbl.Clear();
        }

        TValue Get(TKey key)
        {
            return (TValue)tbl[key];
        }

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                foreach (TKey key in tbl.Keys)
                    keys.Add(key);
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> vals = new List<TValue>();
                foreach (TValue val in tbl.Values)
                    vals.Add(val);
                return vals;
            }
        }

        public bool Contains(TKey key)
        {
            return tbl.Contains(key);
        }

        public bool ContainsKey(TKey key)
        {
            return tbl.ContainsKey(tbl);
        }

        public bool ContainsValue(TValue val)
        {
            return tbl.ContainsValue(val);
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return tbl.GetEnumerator();
        }
    }
}
