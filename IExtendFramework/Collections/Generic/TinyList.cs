﻿/*
 * User: elijah
 * Date: 3/3/2012
 * Time: 5:31 PM
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace IExtendFramework.Collections.Generic
{
    /// <summary>
    /// Grows and shrinks as necessary
    /// </summary>
    public class TinyList<T> : IEnumerable
    {
        /// <summary>
        /// The list of Ts it contains
        /// </summary>
        private T[] me = new T[1];
        
        /// <summary>
        /// Creates a new TinyList
        /// </summary>
        public TinyList()
        {
        }
        
        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="o"></param>
        public void Add(T o)
        {
            GrowOne();
            me[Count - 1] = o;
        }
        
        /// <summary>
        /// Removes the specified item from the list
        /// </summary>
        /// <param name="o"></param>
        public void Remove(T o)
        {
            int index = Array.IndexOf(me, o);
            
            ShrinkOne(index);
        }
        
        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void Clear()
        {
            me = new T[1];
        }
        
        /// <summary>
        /// Removes the item from the specified index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            ShrinkOne(index);
        }
        
        /// <summary>
        /// Get or set the item at the specified index
        /// </summary>
        public T this[int index]
        {
            get
            {
                // handle negatives
                if (index < 0)
                    index = Count - index;
                
                return me[index];
            }
            set
            {
                // handle negatives
                if (index < 0)
                    index = Count - index;
                
                me[index] = value;
            }
        }
        
        /// <summary>
        /// An Enumerator for enumerating through this list
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return me.GetEnumerator();
        }
        
        /// <summary>
        /// The number of items in the list.
        /// </summary>
        public int Count
        {
            get
            {
                return me.Length;
            }
        }
        
        /// <summary>
        /// Returns the index of the specified item
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int IndexOf(T o)
        {
            return Array.IndexOf(me, o);
        }
        
        /// <summary>
        /// Grows the array by one
        /// </summary>
        private void GrowOne()
        {
            // add an empty one on the end
            T[] newArray = new T[Count + 1];
            for (int i = 0; i < me.Length; i++)
                newArray[i] = me[i];
            me = newArray;
        }
        
        /// <summary>
        /// Shrinks the array by 1, skipping the item at the specified index
        /// </summary>
        /// <param name="ignoredIndex"></param>
        private void ShrinkOne(int ignoredIndex)
        {
            T[] newArray = new T[Count - 1];
            int position = 0;
            for (int i = 0; i < me.Length; i++)
            {
                if (i == ignoredIndex)
                    continue;
                
                newArray[position++] = me[i];
            }
            me = newArray;
        }
        
        public T[] ToArray()
        {
            return this.me;
        }
    }
}
