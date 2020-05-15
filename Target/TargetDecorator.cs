using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace Bnyx.AI
{
    // ai manager
    public class TargetDecorator
    {
        private BnyxTeam mTeam;
        private IList<TeamEntity> mEnemies;

        public TargetDecorator()
        {
            mTeam = BnyxTeam.GetSingleton();
            
        }

        /// <summary>
        /// 刷新 AI 数据
        /// </summary>
        public void RefreshEnemies()
        {
            mEnemies = mTeam.GetAllEnemies();
        }

        public void OnInitialize()
        {
            foreach (var entity in mEnemies)
            {
                var hook = entity.Hook;
                if (hook && entity.Valid == true)
                {
                    hook.OnInitialize();
                }
                else
                {
                    throw new BnyxTargetException($"当前的enemy：{entity.GameObj}没有挂载hook脚本");
                }
            }
        }

        public void OnUpdate()
        {
            foreach (var entity in mEnemies)
            {
                var hook = entity.Hook;
                if (hook && entity.Valid == true)
                {
                    hook.OnUpdate();
                }
                else
                {
                    throw new BnyxTargetException($"当前的enemy：{entity.GameObj}没有挂载hook脚本");
                }
            }
        }

        /// <summary>
        /// 当有 AI 死亡的时候调用
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="BnyxTargetException"></exception>
        public void OnDestroy(TeamEntity entity)
        {
            var enemy = mEnemies.ToList().Find(e => e.Ids == entity.Ids);
            if (enemy != null)
            {
                var hook = enemy.Hook;
                if (hook)
                {
                    enemy.Valid = false;
                    hook.OnDestroy1();
                    mTeam.RemoveEnemy(entity); // remove from team 
                }
                else
                {
                    throw new BnyxTargetException($"当前的enemy：{entity.GameObj}没有挂载hook脚本");
                }
            }
            else
            {
                throw new BnyxTargetException($"没有找到当前的enemy数据");
            }
        }
    }
}