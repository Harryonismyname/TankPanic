using UnityEngine;

namespace PolearmStudios.UnityExtensions
{
    public static class TransformExtensions
    {
        public static void MoveTowards(this Transform transform, Vector3 target, float maxDistanceDelta)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
        }

        public static void ChangeParentAndZero(this Transform transform, Transform parent)
        {
            transform.SetParent(parent, false);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
    }
}