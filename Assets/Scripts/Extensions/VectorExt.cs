using UnityEngine;

namespace Extensions
{
    public static class VectorExt
    {
        public static bool IsInRange(this Vector3 vector1, Vector3 vector2, float range) 
            => (vector1 - vector2).magnitude < range;

        public static bool IsInVerticalRange(this Vector3 vector1, Vector3 vector2, float range)
        {
            vector1 = new Vector3(vector1.x, 0, vector1.z);
            vector2 = new Vector3(vector2.x, 0, vector2.z);
            return (vector1 - vector2).magnitude < range;
        }

        public static void eLerp(this ref Vector3 @from, Vector3 to, float speed) 
            => @from = Vector3.Lerp(@from, to, speed);

        public static void eSlerp(this ref Vector3 @from, Vector3 to, float speed) 
            => from = Vector3.Slerp(a: @from, to, speed);

        public static Vector3 ResetHeight(this Vector3 v) => new(v.x, 0, v.z);
    }
}
