﻿using System;
using System.Collections.Generic;

namespace twopointzero.TpzBase32
{
    public static class TpzBase32Converter
    {
        /// <summary>
        /// ConvertToBitEnumerable converts the raw bit representation
        /// of the provided Int32 input argument into an enumerable of Boolean
        /// values with true members of the result representing the high bits
        /// in the input value, from LSb to MSb. This method can be considered
        /// a rough analog to the BitArray that already exists within the framework,
        /// though represented in terms of generics.
        /// </summary>
        /// <param name="input">The Int32 value whose raw bit states will be returned in enumerable form.</param>
        /// <returns>An enumerable of Boolean values representing the bit states of the input argument,
        /// ordered from least-significant bit to most-significant bit.</returns>
        public static IEnumerable<bool> ConvertToBitEnumerable(int input)
        {
            for (int i = 0; i < 32; i++)
            {
                yield return ((uint)input & (uint)Math.Pow(2, i)) != 0;
            }
        }
    }
}