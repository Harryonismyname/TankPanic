using System;

namespace PolearmStudios.Extensions
{
    public static class ArrayExtensions
    {
        public static T GetRandomItem<T>(this Array array)
        {
            return (T)array.GetValue(UnityEngine.Random.Range(0, array.Length - 1)); 
        }
    }
    }

