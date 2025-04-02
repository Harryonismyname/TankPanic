using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolearmStudios.Utils
{
    public static class Utilities
    {
        // ============================== CALCULATING TOOLS ==============================
        public static Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal, float eulerY = 0)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += eulerY;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        public static Vector3 FindShortestDistanceInList(List<Vector3> list, Vector3 origin)
        {
            Vector3 shortest = list[0];
            foreach (Vector3 v in list)
            {
                if (Vector3.Distance(origin, shortest) > Vector3.Distance(origin, v))
                {
                    shortest = v;
                }
            }
            return shortest;
        }
        public static Vector3 FindShortestDistanceInArray(Vector3[] arr, Vector3 origin)
        {
            Vector3 shortest = arr[0];
            foreach (Vector3 v in arr)
            {
                if (Vector3.Distance(origin, shortest) > Vector3.Distance(origin, v))
                {
                    shortest = v;
                }
            }
            return shortest;
        }
        public static void CacheDirs(int degreeArc, int arcStep, out List<Vector3> dirs)
        {
            dirs = new List<Vector3>();
            for (int i = 0; i < degreeArc; i += arcStep)
            {
                dirs.Add(DirFromAngle(i, true));
            }
        }          // BASE FUNCTION
        public static void CacheDirs(int degreeArc, int arcStep, out List<Ray> dirs)
        {
            dirs = new List<Ray>();
            for (int i = 0; i < degreeArc; i += arcStep)
            {
                dirs.Add(new Ray(Vector3.zero, DirFromAngle(i, true)));
            }
        }              // OVERRIDE FOR USE WITH RAYS


        // ============================== CHECKING TOOLS ==============================
        public static bool NotInProximity(Vector3 objectLocation, Vector3 targetLocaton, float zoneSize)
        {
            if (Vector3.Distance(objectLocation, targetLocaton) > zoneSize)
            {
                return true;
            }
            return false;
        }

        static List<int> SortList(List<int> array, int leftIndex, int rightIndex, bool ascending)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (ascending ? array[i] < pivot : array[i] > pivot)
                {
                    i++;
                }

                while (ascending ? array[j] > pivot : array[j] < pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                SortList(array, leftIndex, j, ascending);
            if (i < rightIndex)
                SortList(array, i, rightIndex, ascending);
            return array;
        }

        public static List<int> QuickSort(List<int> list, bool ascending = true)
        {
            return SortList(list, 0, list.Count - 1, ascending);
        }

        // ==============================  MATH TOOLS  ==============================
        public static float SubtractPercentage(float total, float percent)
        {
            float amountToSubtract = total * percent;
            return total - amountToSubtract;
        }
        public static float AddPercentage(float a, float b)
        {
            float amountToAdd = a * b;
            return a + amountToAdd;
        }
    }

}
