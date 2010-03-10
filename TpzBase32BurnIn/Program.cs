using System;
using System.Diagnostics;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32BurnIn
{
    public class Program
    {
        private static void Main()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (long i = int.MinValue; i <= int.MaxValue; i++)
            {
                if (i % 1000000 == 0)
                {
                    TimeSpan elapsed = stopwatch.Elapsed;
                    var ticks = elapsed.Ticks;
                    const long range = (long)int.MaxValue - int.MinValue;
                    var step = i - int.MinValue;
                    var ticksPerStep = ticks / step;
                    var stepsRemaining = range - step;
                    var ticksRemaining = ticksPerStep * stepsRemaining;
                    TimeSpan eta = TimeSpan.FromTicks(ticksRemaining);
                    Console.WriteLine(string.Format("{0}: {1} (ETA {2} @{3}.)", elapsed, i, eta, DateTime.Now + eta));
                }

                int input = (int)i;

                var int32EncodedWithPadding = Int32Converter.EncodeWithPadding(input);
                var referenceEncodedWithPadding = ReferenceConverter.EncodeWithPadding(input);
                if (int32EncodedWithPadding != referenceEncodedWithPadding)
                {
                    Console.WriteLine("Int32 encoded with padding differs from Reference for original input of " + input);
                    Console.WriteLine("Int32 encoded:     " + int32EncodedWithPadding);
                    Console.WriteLine("Reference encoded: " + referenceEncodedWithPadding);
                    break;
                }

                var int32Encoded = Int32Converter.Encode(input);
                var referenceEncoded = ReferenceConverter.Encode(input);
                if (int32Encoded != referenceEncoded)
                {
                    Console.WriteLine("Int32 encoded differs from Reference for original input of " + input);
                    Console.WriteLine("Int32 encoded:     " + int32Encoded);
                    Console.WriteLine("Reference encoded: " + referenceEncoded);
                    break;
                }

                var int32DecodedFromPadded = Int32Converter.DecodeToInt32(int32EncodedWithPadding);
                var referenceDecodedFromPadded = ReferenceConverter.DecodeToInt32(int32EncodedWithPadding);
                if (int32DecodedFromPadded != referenceDecodedFromPadded)
                {
                    Console.WriteLine("Int32 decoded from padded differs from Reference for original input of " + input);
                    Console.WriteLine("Encoded:           " + int32EncodedWithPadding);
                    Console.WriteLine("Int32 decoded:     " + int32DecodedFromPadded);
                    Console.WriteLine("Reference decoded: " + referenceDecodedFromPadded);
                    break;
                }

                var int32Decoded = Int32Converter.DecodeToInt32(int32Encoded);
                var referenceDecoded = ReferenceConverter.DecodeToInt32(int32Encoded);
                if (int32Decoded != referenceDecoded)
                {
                    Console.WriteLine("Int32 decoded differs from Reference for original input of " + input);
                    Console.WriteLine("Encoded:           " + int32Encoded);
                    Console.WriteLine("Int32 decoded:     " + int32Decoded);
                    Console.WriteLine("Reference decoded: " + referenceDecoded);
                    break;
                }

                if (input != int32DecodedFromPadded)
                {
                    Console.WriteLine("Decoded from padded differs from original input of " + input);
                    Console.WriteLine("Encoded: " + int32EncodedWithPadding);
                    Console.WriteLine("Decoded: " + int32DecodedFromPadded);
                    break;
                }

                if (input != int32Decoded)
                {
                    Console.WriteLine("Decoded differs from original input of " + input);
                    Console.WriteLine("Encoded: " + int32Encoded);
                    Console.WriteLine("Decoded: " + int32Decoded);
                    break;
                }
            }
        }
    }
}