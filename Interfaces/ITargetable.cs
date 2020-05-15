using System;

namespace Bnyx.AI
{
    public interface ITargetable
    {
        /// <summary>
        /// 通过一定的条件获取一个实体对象，
        /// 比如找最近的目标，最远的目标，找一定范围内的目标等等
        ///
        /// 作用在实体身上（GameObject）
        /// 每个AI对象都可以进行查询实体
        /// 谁来查询，怎么查
        /// 
        /// </summary>
        /// <param name="predicate">筛选器</param>
        /// <returns>筛选到的结果</returns>
        void SearchEntity(Predicate<TeamEntity> predicate);
    }
}