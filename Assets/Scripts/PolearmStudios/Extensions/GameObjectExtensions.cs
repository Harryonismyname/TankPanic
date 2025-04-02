using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolearmStudios.UnityExtensions
{

    public static class GameObjectExtensions
    {
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;
    }
    }
