using System;
using Bnyx.AI;
using UnityEngine;

namespace Bnyx.AI
{
    public class HierarchyHook : MonoBehaviour
    {
        [Header("当前目标的类型，怪物为enemy，角色为hero")]
        [SerializeField]
        internal TargetType _type;

        [Header("查找目标的距离控制，5米，10米，15米")]
        [SerializeField]
        internal SearchType _distance;
    }
}