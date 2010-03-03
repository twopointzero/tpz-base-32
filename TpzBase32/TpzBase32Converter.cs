using System;
using System.Collections.Generic;

namespace twopointzero.TpzBase32
{
    public static class TpzBase32Converter
    {
        public static IEnumerable<bool> ConvertInt32ToEnumerableOfBoolean(int input)
        {
            for (int i = 0; i < 32; i++)
            {
                yield return ((uint)input & (uint)Math.Pow(2, i)) != 0;
            }
        }
    }
}