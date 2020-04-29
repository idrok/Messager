using UnityEngine;

namespace Bnyx.AI
{
    public class TeamEntity
    {
        private GroupType mType;

        private GameObject mEntity;

        // 每次有实体对象加入的时候，生成一个ID
        // 用于索引和快速查询
        private int mId;
        private bool mValid;

        // 权重
        private short mWeight;
        // 权重占比 = weight/Sum(weight)
        private float mWeightPercent;

        public GameObject Entity
        {
            get => mEntity;
            set => mEntity = value;
        }

        public int Ids
        {
            get => mId;
            set => mId = value;
        }

        public bool Valid
        {
            get => mValid;
            set => mValid = value;
        }

        public GroupType Type
        {
            get => mType;
            set => mType = value;
        }

        public short Weight
        {
            get => mWeight;
            set => mWeight = value;
        }

        public float WeightPercent
        {
            get => mWeightPercent;
            set => mWeightPercent = value;
        }

        public int GetEntityId()
        {
            return mEntity.GetHashCode();
        }
    }
}