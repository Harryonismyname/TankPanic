using System.Collections.Generic;
using UnityEngine;

namespace PolearmStudios.Utils
{
    public static class Randomization
    {
        public static Vector3 RandomLocation(float minX, float maxX, float minZ, float maxZ)
        {
            return new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        }
    }
}