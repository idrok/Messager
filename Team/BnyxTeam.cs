using System.Collections.Generic;
using System.Linq;

namespace Bnyx.AI
{
    public class BnyxTeam
    {
        // TODO

        // add remove enemy player query set manage 
        // filter combine [weight]

        // 物体伤害 对 精灵组 造成伤害提升 n%
        // 对 昏迷 的怪物造成伤害提升 n%
        // 不死族 受到 魔法 伤害减免 n%
        
        private List<ITeamable> mTeams = new List<ITeamable>();
        private List<PlayerGroup> mPlayers = new List<PlayerGroup>();
        private List<EnemyGroup> mEnemies = new List<EnemyGroup>();
        
        // 默认添加一个玩家组
        private PlayerGroup mDefaultPlayerGroup;
        // 默认添加一个敌对组
        private EnemyGroup mDefaultEnemyGroup;

        // 默认在第一个组里面处理
        private byte mDefaultIndex = 0;
        
        #region [singleton] 单列模式创建
        private static BnyxTeam _singleton = null;
        private static object _lock = new object();

        private BnyxTeam()
        {
            mDefaultPlayerGroup = new PlayerGroup();
            mPlayers.Add(mDefaultPlayerGroup);
            mDefaultEnemyGroup = new EnemyGroup();
            mEnemies.Add(mDefaultEnemyGroup);
        }

        public static BnyxTeam GetSingleton()
        {
            if (_singleton == null)
            {
                lock (_lock)
                {
                    _singleton = new BnyxTeam();
                }
            }
            return _singleton;
        }
        #endregion

        #region [function] 基础功能组 Player

        /// <summary>
        /// 添加一个玩家到 Team
        /// </summary>
        /// <param name="entity">新加入的玩家</param>
        /// <param name="defaultGroup">是否加入到默认组</param>
        /// <param name="index">非默认组的索引</param>
        /// <exception cref="BnyxTeamException">自定义Team异常</exception>
        public void AddNewPlayer(TeamEntity entity, bool defaultGroup = true, byte index = 0)
        {
            if (defaultGroup == true)
            {
                mDefaultPlayerGroup.Add(entity);
            }
            else
            {
                bool result = mPlayers.Exists(group => group.Name == mPlayers[index].Name);
                if (result == true)
                {
                    mPlayers[index].Add(entity);
                }
                else
                {
                    throw new BnyxTeamException("当前需要加入的玩家组不存在");
                }
            }
        }

        public void AddNewPlayerGroup(PlayerGroup playerGroup)
        {
            var exist = mPlayers.Exists(group => group.Name == playerGroup.Name);
            if (exist == true)
            {
                throw new BnyxTeamException($"当前玩家组中已经存在TAG为{playerGroup.Name}的组");
            }
            else
            {
                mPlayers.Add(playerGroup);
            }
        }

        public bool RemovePlayer(TeamEntity entity, bool defalutGroup = true, byte index = 0)
        {
            if (defalutGroup)
            {
                return mDefaultPlayerGroup.Remove(entity);
            }
            else
            {
                var group = mPlayers[index];
                return group.Remove(entity);
            }
        }

        public bool RemovePlayerGroup(PlayerGroup playerGroup)
        {
            var exist = mPlayers.Exists(group => playerGroup.Name == group.Name);
            if (exist)
            {
                return mPlayers.Remove(playerGroup);
            }
            else
            {
                throw new BnyxTeamException($"你当前移除的玩家组{playerGroup.Name}不存在");
            }
        }

        public IList<PlayerGroup> GetPlayerGroup()
        {
            return mPlayers;
        }

        public TeamEntity GetDefaultPlayer()
        {
            return mPlayers[mDefaultIndex].Units()[mDefaultIndex];
        }

        public int GetPlayerGroupCount()
        {
            return mPlayers.Count;
        }
        
        #endregion
        
        #region [function] 基础功能组 Enemy

        public void AddNewEnemy(TeamEntity entity, bool defaultGroup = true, byte index = 0)
        {
            if (defaultGroup == true)
            {
                mDefaultEnemyGroup.Add(entity);
            }
            else
            {
                bool result = mEnemies.Exists(group => group.Name == mEnemies[index].Name);
                if (result == true)
                {
                    mEnemies[index].Add(entity);
                }
                else
                {
                    throw new BnyxTeamException("当前需要加入的敌对组不存在");
                }
            }
        }

        public void AddNewEnemyGroup(EnemyGroup enemyGroup)
        {
            var exist = mEnemies.Exists(group => group.Name == enemyGroup.Name);
            if (exist == true)
            {
                throw new BnyxTeamException($"当前敌对组中已经存在TAG为{enemyGroup.Name}的组");
            }
            else
            {
                mEnemies.Add(enemyGroup);
            }
        }

        public bool RemoveEnemy(TeamEntity entity, bool defalutGroup = true, byte index = 0)
        {
            if (defalutGroup)
            {
                return mDefaultEnemyGroup.Remove(entity);
            }
            else
            {
                var group = mEnemies[index];
                return group.Remove(entity);
            }
        }

        public bool RemoveEnemyGroup(EnemyGroup enemyGroup)
        {
            var exist = mEnemies.Exists(group => enemyGroup.Name == group.Name);
            if (exist)
            {
                return mEnemies.Remove(enemyGroup);
            }
            else
            {
                throw new BnyxTeamException($"你当前移除的敌对组{enemyGroup.Name}不存在");
            }
        }

        public List<EnemyGroup> GetEnemyGroup()
        {
            return mEnemies;
        }

        public TeamEntity GetDefaultEnemy()
        {
            return mEnemies[mDefaultIndex].Units()[mDefaultIndex];
        }

        public int GetEnemyGroupCount()
        {
            return mEnemies.Count;
        }
        
        #endregion
        
        
    }
}