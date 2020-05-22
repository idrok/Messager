using System;

namespace Bnyx.AI
{
    public static class TangStyle
    {
        // boss003_idles1
        /// <summary>
        /// boss003_idles1 = Name + "_" + Action
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string GetStyle(string prefix, string action)
        {
            return $"{prefix}_{action}";
        }

        // [monster id = key = role id]
    }
}