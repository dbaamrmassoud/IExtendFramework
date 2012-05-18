/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 5:21 PM
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.Collections
{
    /// <summary>
    /// Uses priority to decide what to give next
    /// </summary>
    public class PriorityQueue : System.Collections.Queue
    {
        List<System.Tuple<Priority, object>> list = new List<Tuple<Priority, object>>();
        
        /// <summary>
        /// Creates a new PriorityQueue
        /// </summary>
        public PriorityQueue()
        {
        }
        
        /// <summary>
        /// Pushes an object to the queue with normal priority
        /// </summary>
        /// <param name="o"></param>
        public void Push(object o)
        {
            Push(o, Priority.Normal);
        }
        
        /// <summary>
        /// Pushes an object to the queue with specified priority
        /// </summary>
        /// <param name="o"></param>
        /// <param name="p"></param>
        public void Push(object o, Priority p)
        {
            list.Add(new Tuple<Priority, object>(p, o));
        }
        
        /// <summary>
        /// Pops the item with the highest priority off the queue
        /// </summary>
        /// <returns></returns>
        public Tuple<Priority, object> Pop()
        {
            // its empty.
            if (list.Count == 0)
                return null;
            
            // find the highest item
            int highest = -1;
            Tuple<Priority, object> thatItem = null;
            foreach (Tuple<Priority, object> t in list)
            {
                bool b = (int)t.Item1 > highest;
                if (b)
                {
                    highest = (int)t.Item1;
                    thatItem = t;
                }
            }
            
            switch (highest)
            {
                case 5:
                    list.RemoveAt(list.IndexOf(thatItem));
                    return thatItem;
                case 4:
                    list.RemoveAt(list.IndexOf(thatItem));
                    return thatItem;
                case 3:
                    list.RemoveAt(list.IndexOf(thatItem));
                    return thatItem;
                case 2:
                    list.RemoveAt(list.IndexOf(thatItem));
                    return thatItem;
                case 1:
                    list.RemoveAt(list.IndexOf(thatItem));
                    return thatItem;
                default:
                    throw new Exception("Impossible! No Items were found to return!");
            }
        }
        
        /// <summary>
        /// Whether the Queue has an item
        /// </summary>
        public bool HasItem
        {
            get
            {
                return list.Count > 0;
            }
        }
        
        /// <summary>
        /// How many items are in the queue
        /// </summary>
        public override int Count
        {
            get
            {
                return list.Count;
            }
        }
        
        /// <summary>
        /// Clears the Queue.
        /// </summary>
        public override void Clear()
        {
            list.Clear();
        }
        
        public override object Clone()
        {
            throw new InvalidOperationException();
        }
        
        public override bool Contains(object obj)
        {
            throw new InvalidOperationException();
        }
        
        public override void CopyTo(Array array, int index)
        {
            throw new InvalidOperationException();
        }
        
        public override object Dequeue()
        {
            return this.Pop();
        }
        
        public override void Enqueue(object obj)
        {
            this.Push(obj);
        }
        
        public override System.Collections.IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
        
        public override bool IsSynchronized {
            get { return false; }
        }
        
        public override object Peek()
        {
            throw new InvalidOperationException();
        }
        
        public override object SyncRoot {
            get { throw new InvalidOperationException(); }
        }
        
        public override object[] ToArray()
        {
            return list.ToArray();
        }
        
        public override void TrimToSize()
        {
            throw new InvalidOperationException();
        }
        
        public override string ToString()
        {
            // returns the type
            return typeof(PriorityQueue).ToString();
        }

    }
}
