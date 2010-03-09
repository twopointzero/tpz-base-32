using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32
{
    /// <summary>
    /// The Normalizer class provides easy-to-use methods for normalizing
    /// encoded strings to correct errors
    /// that commonly occur during the human transcription of numbers
    /// and characters. 0 (the number zero) becomes o (the lowercase
    /// letter O), l (the lowercase letter L) becomes 1 (the number one),
    /// 2 (the number two) becomes z (the lowercase letter Z), and v (the
    /// lowercase letter V) becomes u (the lowercase letter U), as these
    /// are all common transcription errors. All uppercase letters become
    /// lowercase, as the encoding is lowercase in nature.
    /// </summary>
    public static class Normalizer
    {
        /// <summary>
        /// Normalize processes the provided input string, correcting errors
        /// that commonly occur during the human transcription of numbers
        /// and characters. 0 (the number zero) becomes o (the lowercase
        /// letter O), l (the lowercase letter L) becomes 1 (the number one),
        /// 2 (the number two) becomes z (the lowercase letter Z), and v (the
        /// lowercase letter V) becomes u (the lowercase letter U), as these
        /// are all common transcription errors. All uppercase letters become
        /// lowercase, as the encoding is lowercase in nature.
        /// </summary>
        /// <param name="input">A non-null string containing zero or more
        /// characters that are expected to represent an encoded value.</param>
        /// <returns>A new string </returns>
        /// <remarks>This normalization process assumes that the entire input
        /// string is to be processed as an encoded value, and is not intended
        /// It will accept a string containing arbitrary text just a portion
        /// of which is a value in the encoded representation, and can be used
        /// to assist in locating encoded values within other larger pieces of
        /// text, but as a result it may modify other portions of the input
        /// string in unexpected ways.</remarks>
        public static string Normalize(string input)
        {
            return input.NormalizeToTpzBase32Alphabet();
        }

        /// <summary>
        /// Normalize processes the provided input char, correcting errors
        /// that commonly occur during the human transcription of numbers
        /// and characters. 0 (the number zero) becomes o (the lowercase
        /// letter O), l (the lowercase letter L) becomes 1 (the number one),
        /// 2 (the number two) becomes z (the lowercase letter Z), and v (the
        /// lowercase letter V) becomes u (the lowercase letter U), as these
        /// are all common transcription errors. All uppercase letters become
        /// lowercase, as the encoding is lowercase in nature.
        /// </summary>
        /// <param name="input">A char containing a 
        /// character that is expected to represent an encoded value.</param>
        /// <returns>The input character, normalized if it was required.</returns>
        public static char Normalize(char input)
        {
            return input.NormalizeToTpzBase32Alphabet();
        }
    }
}