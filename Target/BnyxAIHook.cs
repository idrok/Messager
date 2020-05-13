﻿using System;
using System.Collections;
using System.Collections.Generic;
using Bnyx.AI;
using Tang.Controller.Role.Controller;
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

        private Collider[] mColliders;
        private float mBodyFar = 50f;

        private BnyxTeam mTeam;
        
        public TeamEntity Entity
        {
            get => mEntity;
            set => mEntity = value;
        }

        private void Awake()
        {
            Initialzie();
        }

        private void Initialzie()
        {
            mTargetList = new BnyxTargetList();
            mMatch = SearchMatch.BodyFar_50;
            mSearchType = SearchType.BodyFar_10;
            // 单次检测的最大目标数为 32
            mColliders = new Collider[32];
            mTeam = BnyxTeam.GetSingleton();
        }

        private BnyxTargetList GetEnemyMatchList()
        {
            BnyxTargetList list = new BnyxTargetList();
            if (mMatch == SearchMatch.BodyFar_50)
            {
                mBodyFar = 50f;
                var founds = Physics.OverlapSphereNonAlloc(mEntity.Body.position, mBodyFar, mColliders);
                for (int i = 0; i < founds; i++)
                {
                    var collider = mColliders[i];
                    var controller = collider.GetComponent<RoleInputController>();
                    if (controller && controller._Type == Bnyx.AI.Target.Hero)
                    {
                        
                        //list.Add();
                    }
                }
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
            mTargetList.Clear();
            var enemies = GetEnemyMatchList();
            Debug.LogFormat($"第一次搜索到的ememies数量：{enemies.Count}");
            foreach (var enemy in enemies)
            {
                var pred = predicate(enemy);
                if (pred == true)
                {
                    if (mTargetList.Contains(enemy) == false)
                    {
                        mTargetList.Add(enemy);
                        Debug.LogFormat($"查找到符合条件的enemy：{enemy.Entity}");
                    }
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