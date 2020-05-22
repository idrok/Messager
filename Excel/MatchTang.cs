using Tang;
using UnityEngine;

namespace Bnyx.AI
{
    public class MatchTang
    {
        /// <summary>
        /// 匹配唐建的代码待机行为
        /// </summary> 
        /// <param name="idle"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool MatchIdle(string prefix, string idle, RoleAnimator data)
        {
            var idles = data.idles;
            var fullName = TangStyle.GetStyle(prefix, idle);
            foreach (var idleBehaviourData in idles)
            {
                if (idleBehaviourData.Key == fullName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MatchWalkAndRun(string prefix, string walkAndRun, RoleAnimator data)
        {
            var walkAndRuns = data.walkAndRuns;
            var fullName = TangStyle.GetStyle(prefix, walkAndRun);
            foreach (var walkAndRunData in walkAndRuns)
            {
                if (walkAndRunData.Key == fullName)
                {
                    return true;
                }

            }

            return false;
        }
    }
}