using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IDisposable = AI.Architecture.iRoot.IDisposable;

namespace Bnyx.AI
{
    /// <summary>
    /// 游戏目标
    /// 基础状态包括：有效，死亡，可移动
    /// TODO buff增益效果
    /// </summary>
    public class BnyxTargetList : ICollection<TeamEntity>, IDisposable
    {
        LinkedList<TeamEntity> mEntities = new LinkedList<TeamEntity>();
        private bool IsDisposed = false;
        
        public IEnumerator<TeamEntity> GetEnumerator()
        {
            return mEntities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TeamEntity item)
        {
            if (IsDisposed == false)
            {
                mEntities.AddLast(item);
            }
        }

        public void Clear()
        {
            mEntities.Clear();
        }

        public bool Contains(TeamEntity item)
        {
            return mEntities.Contains(item);
        }

        public void CopyTo(TeamEntity[] array, int arrayIndex)
        {
            mEntities.CopyTo(array, arrayIndex);
        }

        public bool Remove(TeamEntity item)
        {
            return mEntities.Remove(item);
        }

        public int Count
        {
            get => mEntities.Count;
        }
        
        public bool IsReadOnly
        {
            get => false;
        }

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