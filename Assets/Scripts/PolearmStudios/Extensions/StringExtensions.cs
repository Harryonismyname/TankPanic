using System;
using System.Collections;
using System.Collections.Generic;

namespace PolearmStudios.Extensions
{
    public static class StringExtensions
    {
        public static int ComputeFNV1aHash(this string str)
        {
            uint hash = 2166136261;
            foreach (char c in str)
            {
                hash = (c ^ hash) * 16777619;
            }
            return unchecked((int)hash);

        }
    }
}

