using System.Collections.Generic;
using System.Linq;
using Leng.Exception;

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

        /// <summary>
        /// 获取默认的玩家组，游戏当前默认只有一个组玩家
        /// </summary>
        /// <returns></returns>
        public PlayerGroup GetDefaultPlayerGroup()
        {
            var group = GetPlayerGroup();
            if (group.Count > 0)
            {
                return group[0];
            }
            else
            {
                throw new BnyxTeamException("Fatal：没有默认的玩家组");
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

        /// <summary>
        /// 计算出离我最近的玩家，当前返回第一个进入的玩家
        /// </summary>
        /// <param name="entity">敌方实体</param>
        /// <returns></returns>
        public TeamEntity GetNearestPlayer(TeamEntity entity)
        {
            var playerGroup = mPlayers[mDefaultIndex];
            var players = playerGroup.Units();

            foreach (var player in players)
            {
                // todo calc
            }

            return GetDefaultPlayer();
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
        
        /// <summary>
        /// 获取默认的Enemy组，游戏当前默认只有一个组Enemy
        /// </summary>
        /// <returns></returns>
        public EnemyGroup GetDefaultEnemyGroup()
        {
            var group = GetEnemyGroup();
            if (group.Count > 0)
            {
                return group[0];
            }
            else
            {
                throw new BnyxTeamException("Fatal：没有默认的Enemy组");
            }
        }

        public List<EnemyGroup> GetEnemyGroup()
        {
            return mEnemies;
        }

        public TeamEntity GetDefaultEnemy()
        {
            var group = mEnemies[mDefaultIndex];
            if (group.Units().Count > 0)
            {
                var enemy = group.Units()[mDefaultIndex];
                return enemy;
            }
            else
            {
                throw new BnyxException("get enemy is null.");
            }
        }

        public int GetEnemyGroupCount()
        {
            return mEnemies.Count;
        }
        
        #endregion

        #region 通用函数组

        /// <summary>
        /// 获取所有默认组的 Enemy
        /// </summary>
        /// <returns></returns>
        public IList<TeamEntity> GetAllEnemies()
        {
            return GetDefaultEnemyGroup().Units();
        }

        #endregion
    }
}