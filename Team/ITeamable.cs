using System.Collections.Generic;
using UnityEngine;

namespace Bnyx.AI {
    public interface ITeamable {
        // 当前队伍有哪些单位
        IList<TeamEntity> Units ();

        // 队伍名字，敌方有多个组，比如 精灵组、恶魔组、精英组
        string Name { get; set; }

        // 队伍是否有效
        bool Vaild { get; set; }

        void Add (TeamEntity entity);
        
        bool Remove (TeamEntity entity);

    }

    public enum GroupType {
        NULL,
        ENEMY, //敌方
        PLAYER, // 玩家
        NEUTRAL //中立
    }
}