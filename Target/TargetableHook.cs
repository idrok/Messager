using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bnyx.AI
{
    // ai 逻辑执行 hook
    public class TargetableHook : HierarchyHook, ITargetable
    {
        private TeamEntity mEntity;
        private BnyxTargetList mTargets;

        private BnyxTeam mTeam;

        public TeamEntity Entity
        {
            get => mEntity;
            set => mEntity = value;
        }

        // todo 统一初始化
        public void OnInitialize()
        {
            mTargets = new BnyxTargetList();
            mTeam = BnyxTeam.GetSingleton();
        }

        private IList<TeamEntity> GetPlayerMatchList()
        {
            var group = mTeam.GetDefaultPlayerGroup();
            var players = group.Units();
            if (players != null && players.Count > 0)
            {
                return players;
            }
            else
            {
                throw new BnyxTeamException("Fatal：没有默认的玩家");
            }
        }

        // todo 统一更新函数
        public void OnUpdate()
        {
            SearchEntity(PredicateCondition);
        }

        private bool PredicateCondition(TeamEntity entity)
        {
            var distance = (mEntity.Body.position - entity.Body.position).magnitude;

            float see = BodyFarToFloat();
            if (distance <= see)
            {
                return true;
            }
            else
            {
                // Debug.LogFormat("没有查询到符合的目标");
                return false;
            }
        }

        private float BodyFarToFloat(float user = default(float))
        {
            float value = default(float);
            switch (_distance)
            {
                case SearchType.BodyFar_05:
                    value = 5;
                    break;
                case SearchType.BodyFar_10:
                    value = 10;
                    break;
                case SearchType.BodyFar_15:
                    value = 15;
                    break;
                case SearchType.BodyFar_User:
                    value = user;
                    break;
                default:
                    value = 5;
                    break;
            }

            return value;
        }

        public void SearchEntity(Predicate<TeamEntity> predicate)
        {
            mTargets.Clear();
            var players = GetPlayerMatchList();
            Debug.LogFormat($"地图拥有的palyers数量：{players.Count}");
            foreach (var player in players)
            {
                var vaild = predicate(player);
                if (vaild == true)
                {
                    if (mTargets.Contains(player) == false)
                    {
                        mTargets.Add(player);
                        Debug.LogFormat($"查找到符合条件的palyer：{player.GameObj}");
                    }
                }
            }
        }

        // todo 统一销毁
        public void OnDestroy1()
        {
            mTargets.Dispose();
            Destroy(mEntity.GameObj);
        }
    }
}