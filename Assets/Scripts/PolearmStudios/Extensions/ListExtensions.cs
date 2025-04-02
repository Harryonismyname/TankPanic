using System.Collections.Generic;

namespace PolearmStudios.Extensions
{
    public static class ListExtensions
    {

        public static T GetRandomItem<T>(this List<T> ListToSelectFrom)
        {
            return ListToSelectFrom[UnityEngine.Random.Range(0, ListToSelectFrom.Count)];
        }
    }
    }

