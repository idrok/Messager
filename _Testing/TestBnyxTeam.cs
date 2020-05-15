//using NUnit.Framework;

using System;
using UnityEngine;
//using NUnit;

namespace Bnyx.AI
{
    //[TestFixture]
    public class TestBnyxTeam : MonoBehaviour
    {
        private BnyxTeam mTeam;

        //[SetUp]
        void InitEnv()
        {
            
        }

        private void Start()
        {
            TestAddGroup();
        }

        //[Test]
        public void TestAddGroup()
        {
            mTeam = BnyxTeam.GetSingleton();
            TeamEntity player = new TeamEntity();
            player.GameObj = new GameObject("Player");
            player.Ids = player.GetEntityId();
            player.Type = GroupType.PLAYER;
            player.Valid = true;
            player.Weight = -1;
            player.WeightPercent = 1f;
            
            mTeam.AddNewPlayer(player); 
            
            PlayerGroup playerGroup = new PlayerGroup();
            playerGroup.Name = "冲锋小队";
            mTeam.AddNewPlayerGroup(playerGroup);
            mTeam.AddNewPlayer(player, false, 1);
            //mTeam.AddNewPlayer(player, false, 1);
            
            TeamEntity enemy = new TeamEntity();
            enemy.GameObj = new GameObject("Enemy");
            enemy.Ids = enemy.GetEntityId();
            enemy.Type = GroupType.ENEMY;
            enemy.Valid = true;
            enemy.Weight = -1;
            enemy.WeightPercent = 1f;
            mTeam.AddNewEnemy(enemy);
            // mTeam.AddNewEnemy(enemy);
        }
    }
}