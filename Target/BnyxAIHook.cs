using System;
using System.Collections;
using System.Collections.Generic;
using Bnyx.AI;
using UnityEngine;

namespace AI.Target
{
    // ai 逻辑执行 hook
    public class BnyxAIHook : MonoBehaviour, ITargetable
    {
        private TeamEntity mEntity;
        private BnyxTargetList mTargetList;

        private SearchMatch mMatch;
        private SearchType mSearchType;

        public TeamEntity Entity
        {
            get => mEntity;
            set => mEntity = value;
        }

        private IEnumerator Awake()
        {
            yield return null;
            Initialzie();
        }

        private void Initialzie()
        {
            mTargetList = new BnyxTargetList();
            mMatch = SearchMatch.BodyFar_50;
            mSearchType = SearchType.BodyFar_10;
        }

        private BnyxTargetList GetEnemyMatchList()
        {
            if (mMatch == SearchMatch.BodyFar_50)
            {
                Collider[] results = default(Collider[]);
                // todo 
                // Physics.sph(mEntity.Body.position, 50f, results);
            }

            return null;
        }

        // todo 统一更新函数
        public void UnitUpdate()
        {
            
        }
        
        // AI1 要查找附近5米内的目标
        // AI2 要查找附近10米内的目标
        // AI3 要查找血量低于30%的目标
        
        // todo 分成二次搜索，第一次模糊查询，第二次精确查找
        public BnyxTargetList SearchEntity(Predicate<TeamEntity> predicate)
        {
            var enemies = GetEnemyMatchList();
            foreach (var enemy in enemies)
            {
                var pred = predicate(enemy);
                if (pred == true)
                {
                    if (mTargetList.Contains(enemy) == false)
                    {
                        mTargetList.Add(enemy);
                    }
                }
                else
                {
                    mTargetList.Remove(enemy);
                }
            }

            return mTargetList;
        }

        private void OnDestroy()
        {
            mTargetList.Dispose();
        }
    }
}