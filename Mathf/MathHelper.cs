using UnityEngine;

namespace AI.Mathf
{
    public class MathHelper
    {
        /// <summary>
        /// // dot (x1,y1,z1).(x2,y2,z2)=(x1x2 + y1y2 + z1z2)
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Dot(Vector3 v1, Vector3 v2)
        {
            var dot = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
            return dot;
        }

        /// <summary>
        /// // cross (x1,y1,z1)X(x2,y2,z2)=(y1z2-y2z1, z1x2-z2x1, x1y2-x2y1)
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            var x = v1.y * v2.z - v2.y * v1.z;
            var y = v1.z * v2.x - v2.z * v1.x;
            var z = v1.x * v2.y - v2.x * v1.y;
            return new Vector3(x, y, z);
        }
    }
}