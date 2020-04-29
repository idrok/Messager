using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bnyx.AI
{
    public class EnemyGroup : ITeamable
    {
        private List<TeamEntity> mEnemies = new List<TeamEntity>();
        private string NAME = "EnemyGroup1";
        private bool VAILD = true;

        public string Name 
        { 
            get => NAME; 
            set => NAME = value; 
        }
        
        public bool Vaild 
        { 
            get => VAILD;
            set => VAILD = value; 
        }

        public void Add(TeamEntity entity)
        {
            var result = mEnemies.Exists(unit => unit.Ids == entity.Ids);
            if (result == true) 
            {
                throw new BnyxTeamException("当前加入的敌对目标已经存在");
            }
            else
            {
                mEnemies.Add(entity);
            }
        }

        public bool Remove(TeamEntity entity)
        {
            var result = mEnemies.Exists(unit => unit.Ids == entity.Ids);
            if (result == true) 
            {
                return mEnemies.Remove(entity);
            }
            else
            {
                throw new BnyxTeamException("敌对队伍中的目标不存在");
            }
        }

        public IList<TeamEntity> Units()
        {
            return mEnemies;
        }
    }
}