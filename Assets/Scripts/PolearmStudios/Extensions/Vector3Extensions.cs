using UnityEngine;

namespace PolearmStudios.UnityExtensions
{
    public static class Vector3Extensions
    {

        public static Vector3 RandomPointAround(this Vector3 origin, float distance, bool includeY = false)
        {
            float x = Random.Range(-distance, distance) + origin.x;
            float y = includeY ? Random.Range(-distance, distance) + origin.y : origin.y;
            float z = Random.Range(-distance, distance) + origin.z;
            return new Vector3(x, y, z);
        }
        public static Vector3 RandomPointAround(this Vector3 origin, float minDistance, float maxDistance, bool includeY = false)
        {
            Vector3 point;
            if (includeY)
            {
                point = Random.insideUnitSphere;
                point.x *= Random.Range(minDistance, maxDistance);
                point.y *= Random.Range(minDistance, maxDistance);
                point.z *= Random.Range(minDistance, maxDistance);
                return point;
            }
            point = Random.insideUnitCircle;
            float x = (point.x * Random.Range(minDistance, maxDistance)) + origin.x;
            float z = (point.y * Random.Range(minDistance, maxDistance)) + origin.z;

            return new(x, origin.y, z);
        }
        public static Vector3 RandomLocation(this Vector3 origin, Vector3 oppositeCorner)
        {
            return new Vector3(Random.Range(origin.x, oppositeCorner.x), Random.Range(origin.y, oppositeCorner.y), Random.Range(origin.z, oppositeCorner.z));
        }
        public static Vector3 RandomPointAroundWithLineOfSight(this Vector3 origin, float minDistance, float maxDistance, int attempts, LayerMask obstacleMask, bool includeY = false)
        {
            Vector3 target = origin.RandomPointAround(minDistance, maxDistance, includeY);

            if (Physics.Raycast(origin, (target - origin).normalized, Vector3.Distance(target, origin), obstacleMask))
            {
                if (attempts > 0)
                {
                    return origin.RandomPointAroundWithLineOfSight(minDistance, maxDistance, attempts - 1, obstacleMask, includeY);
                }
                else
                {
                    return origin;
                }
            }
            return target;
        }
        public static bool RandomPointAroundWithLineOfSight(this Vector3 origin, float minDistance, float maxDistance, int attempts, LayerMask obstacleMask, out Vector3 destination)
        {
            origin.RandomPointOnNavMesh(minDistance, maxDistance, out Vector3 target);

            // Check if there is line of sight between the origin and the target
            if (Physics.Raycast(origin, (target - origin).normalized, Vector3.Distance(target, origin), obstacleMask))
            {
                // If there is no line of sight, try again
                if (attempts > 0)
                {
                    return origin.RandomPointAroundWithLineOfSight(minDistance, maxDistance, attempts - 1, obstacleMask, out destination);
                }
                else
                {
                    // If no point is found, return the origin
                    destination = origin;
                    return false;
                }
            }
            // If there is line of sight, return the target
            destination = target;
            return true;
        }
        public static bool RandomPointOnNavMesh(this Vector3 origin, float radius, out Vector3 destination)
        {
            // Generate a random point within a sphere around the center
            Vector3 randomDirection = Random.insideUnitSphere * radius;

            // Add the random direction to the center to get the random point
            randomDirection += origin;

            // Sample the position on the NavMesh
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out UnityEngine.AI.NavMeshHit hit, radius, UnityEngine.AI.NavMesh.AllAreas))
            {
                // Return the valid position on the NavMesh
                destination = hit.position;
                return true;
            }
            destination = origin;
            return false;
        }

        // Overload for using min and max distance
        public static bool RandomPointOnNavMesh(this Vector3 origin, float minRadius, float maxRadius, out Vector3 destination)
        {
            float radius = Random.Range(minRadius, maxRadius);
            // Generate a random point within a sphere around the center
            Vector3 randomDirection = Random.insideUnitSphere * radius;

            // Add the random direction to the center to get the random point
            randomDirection += origin;

            // Sample the position on the NavMesh
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out UnityEngine.AI.NavMeshHit hit, radius, UnityEngine.AI.NavMesh.AllAreas))
            {
                // Return the valid position on the NavMesh
                destination = hit.position;
                return true;
            }
            destination = origin;
            return false;
        }
    }
    }
