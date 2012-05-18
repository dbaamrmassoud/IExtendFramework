/*
 * User: elijah
 * Date: 4/11/2012
 * Time: 2:43 PM
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.Collections.Generic
{
    /// <summary>
    /// Uses a numeric priority system to decide what to give next
    /// </summary>
    public class NumericPriorityQueue<T>
    {
        List<System.Tuple<int, T>> list = new List<Tuple<int, T>>();
        
        /// <summary>
        /// Creates a new NumericPriorityQueue
        /// </summary>
        public NumericPriorityQueue()
        {
        }
        
        /// <summary>
        /// Pushes an object to the queue with normal priority
        /// </summary>
        /// <param name="o"></param>
        //public void Push(T o)
        //{
        //    Push(o, Priority.Normal);
        //}
        
        /// <summary>
        /// Pushes an object to the queue with specified priority
        /// </summary>
        /// <param name="o"></param>
        /// <param name="p"></param>
        public void Push(T o, int p)
        {
            list.Add(new Tuple<int, T>(p, o));
        }
        
        /// <summary>
        /// Pops the item with the highest priority off the queue
        /// </summary>
        /// <returns></returns>
        public Tuple<int, T> Pop()
        {
            // its empty.
            if (list.Count == 0)
                return null;
            
            // find the highest item
            int highest = -1;
            Tuple<int, T> thatItem = null;
            foreach (Tuple<int, T> t in list)
            {
                bool b = t.Item1 > highest;
                if (b)
                {
                    highest = t.Item1;
                    thatItem = t;
                }
            }
            
            if (highest == -1)
                return null;
            
            this.list.RemoveAt(this.list.IndexOf(thatItem));
            return thatItem;
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
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        
        /// <summary>
        /// Clears the Queue.
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }
        
    }
}
