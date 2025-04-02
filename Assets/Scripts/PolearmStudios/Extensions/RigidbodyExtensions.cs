using UnityEngine;

namespace PolearmStudios.UnityExtensions
{
    public static class RigidbodyExtensions
    {
        public static Rigidbody MoveRB(this Rigidbody rb, Vector3 position)
        {
            DeactivateRB(rb);
            rb.position = position;
            return rb;
        }

        public static Rigidbody DeactivateRB(this Rigidbody rb)
        {
            rb.useGravity = false;
            if (!rb.isKinematic) rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            return rb;
        }

        public static Rigidbody ActivateRB(this Rigidbody rb)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            return rb;
        }

        public static Rigidbody ApplyForceTowardOther(this Rigidbody rb, Vector3 otherPosition, float force, ForceMode mode)
        {
            Vector3 velocity = (otherPosition - rb.position).normalized;
            velocity *= force;
            rb.AddForce(velocity, mode);
            return rb;
        }
    }
}