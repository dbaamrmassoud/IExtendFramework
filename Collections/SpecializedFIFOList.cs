using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace IExtendFramework.Collections
{
    /// <summary>
    /// A Simple "Reverse" Queue.
    /// It acts as a First-in-first-out List(Of T)
    /// </summary>
    /// <remarks><see cref="System.Collections.Queue">Based off of Queue</see></remarks>
    public class SpecializedFIFOList<T>
    {
        
        private List<T> _internalCollection = new List<T>();
        
        public int Count {
            get { return _internalCollection.Count; }
        }
        
        public void Clear()
        {
            _internalCollection.Clear();
        }
        
        public bool Contains(T value)
        {
            if (_internalCollection.Contains(value))
                return true;
            
            return false;
        }
        
        public void AddItem(T item)
        {
            if (item == null) {
                throw new ArgumentException("item");
            }
            _internalCollection.Add(item);
        }
        
        public T GetItem()
        {
            T item = _internalCollection[0];
            _internalCollection.RemoveAt(0);
            return item;
        }
    }
}
