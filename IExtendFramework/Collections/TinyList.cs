/*
 * User: elijah
 * Date: 1/28/2012
 * Time: 3:15 PM
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace IExtendFramework.Collections
{
    /// <summary>
    /// Grows and shrinks as necessary
    /// </summary>
    public class TinyList : IEnumerable
    {
        /// <summary>
        /// The list of objects it contains
        /// </summary>
        private object[] me = new object[1];
        
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
        public void Add(object o)
        {
            GrowOne();
            me[Count - 1] = o;
        }
        
        /// <summary>
        /// Removes the specified item from the list
        /// </summary>
        /// <param name="o"></param>
        public void Remove(object o)
        {
            int index = Array.IndexOf(me, o);
            
            ShrinkOne(index);
        }
        
        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void Clear()
        {
            me = new object[1];
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
        public object this[int index]
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
        public int IndexOf(object o)
        {
            return Array.IndexOf(me, o);
        }
        
        /// <summary>
        /// Grows the array by one
        /// </summary>
        private void GrowOne()
        {
            // add an empty one on the end
            object[] newArray = new object[Count + 1];
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
            object[] newArray = new object[Count - 1];
            int position = 0;
            for (int i = 0; i < me.Length; i++)
            {
                if (i == ignoredIndex)
                    continue;
                
                newArray[position++] = me[i];
            }
            me = newArray;
        }
        
        public object[] ToArray()
        {
            return this.me;
        }
    }
}
