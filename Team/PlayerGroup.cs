using System.Collections.Generic;
using System.Linq;

namespace Bnyx.AI
{
    public class PlayerGroup : ITeamable
    {
        private List<TeamEntity> mPlayers = new List<TeamEntity>();
        private string NAME = "PlayerGroup1";
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
            var result = mPlayers.Exists(unit => unit.Ids == entity.Ids);
            if (result == true) 
            {
                throw new BnyxTeamException("当前加入的玩家目标已经存在");
            }
            else
            {
                mPlayers.Add(entity);
            }
        }

        public bool Remove(TeamEntity entity)
        {
            var result = mPlayers.Exists(unit => unit.Ids == entity.Ids);
            if (result == true) 
            {
                return mPlayers.Remove(entity);
            }
            else
            {
                throw new BnyxTeamException("玩家队伍中的目标不存在");
            }
        }

        public IList<TeamEntity> Units()
        {
            return mPlayers;
        }
    }
}