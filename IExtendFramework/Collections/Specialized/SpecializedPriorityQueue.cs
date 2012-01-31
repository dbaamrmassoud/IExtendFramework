/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 5:21 PM
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.Collections.Specialized
{
    /// <summary>
    /// Uses priority to decide what to give next
    /// </summary>
    public class PriorityQueue<T>
    {
        List<System.Tuple<Priority, T>> list = new List<Tuple<Priority, T>>();
        
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
        public void Push(T o)
        {
            Push(o, Priority.Normal);
        }
        
        /// <summary>
        /// Pushes an object to the queue with specified priority
        /// </summary>
        /// <param name="o"></param>
        /// <param name="p"></param>
        public void Push(T o, Priority p)
        {
            list.Add(new Tuple<Priority, T>(p, o));
        }
        
        /// <summary>
        /// Pops the item with the highest priority off the queue
        /// </summary>
        /// <returns></returns>
        public Tuple<Priority, T> Pop()
        {
            // its empty.
            if (list.Count == 0)
                return null;
            
            // find the highest item
            int highest = -1;
            Tuple<Priority, T> thatItem = null;
            foreach (Tuple<Priority, T> t in list)
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
