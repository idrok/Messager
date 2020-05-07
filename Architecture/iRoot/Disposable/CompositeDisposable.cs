using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Architecture.iRoot
{
    /// <summary>
    /// 自定义集合数据结构内嵌功能可以这样使用
    /// </summary>
    public class CompositeDisposable : ICollection<IDisposable>, IDisposable
    {
        readonly LinkedList<IDisposable> _list = new LinkedList<IDisposable>();
        public bool IsDisposed { get; private set; }
        
        /// <summary>
        /// 接口带泛型，必定调用这个方法
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IDisposable> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void Add(IDisposable item)
        {
            if (IsDisposed == false)
            {
                _list.AddLast(item);
            }
        }

        public void Clear()
        {
            if (IsDisposed == false)
            {
                foreach (var item in _list)
                {
                    item.Dispose();
                }
                _list.Clear();
            }
        }

        public bool Contains(IDisposable item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(IDisposable item)
        {
            return _list.Remove(item);
        }

        public int Count { get => _list.Count; }
        public bool IsReadOnly { get => false; }
        public void Dispose()
        {
            if (IsDisposed == false)
            {
                IsDisposed = true;
                Clear();
            }
        }
    }
}