using UnityEngine;

namespace Bnyx.AI
{
    public class TeamEntity
    {
        private GroupType mType;

        private GameObject mGameObj;

        // 每次有实体对象加入的时候，生成一个ID
        // 用于索引和快速查询
        private int mId;
        private bool mValid;

        // 权重
        private short mWeight;
        // 权重占比 = weight/Sum(weight)
        private float mWeightPercent;

        private Transform mDummyCache;
        private TargetableHook mHook;

        public GameObject GameObj
        {
            get => mGameObj;
            set => mGameObj = value;
        }

        public TargetableHook Hook
        {
            get
            {
                if (mHook == null)
                {
                    mHook = mGameObj.GetComponent<TargetableHook>();
                    if (mHook == null)
                    {
                        throw new BnyxTargetException("没有挂载Hook脚本");
                    }
                }

                return mHook;
            }
        }

        public Transform Body
        {
            get
            {
                // advance code, once init would cache everywhere
                if (mDummyCache == null)
                {
                    var dummy = mGameObj.transform.FindChild("PointPrefab");
                    
                    if (dummy != null)
                    {
                        mDummyCache = dummy;
                    }
                    else
                    {
                        mDummyCache = mGameObj.transform;
                    }
                }
                
                return mDummyCache;
            }
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
            return mGameObj.GetHashCode();
        }
    }
}