using System.Collections.Generic;

namespace Bnyx.AI
{
    // 中立组，无需求
    public class NeutralGroup : ITeamable
    {
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool Vaild { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Add(TeamEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(TeamEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IList<TeamEntity> Units()
        {
            throw new System.NotImplementedException();
        }
    }
}