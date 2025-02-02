using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WTDawson.PersistentID
{
    internal static class Utils
    {
        internal static char[] ASCIIUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        internal static char[] ASCIILower = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        internal static char[] ASCIINumerical = "1234567890".ToCharArray();
        internal static char[] ASCIISpecial = "`¬¦!\"£$%^&*()-=_+\\|[]{};'#:@~,./<>?".ToCharArray();

        /// <summary>
        /// Casts an array of objects to an array of strings.
        /// </summary>
        /// <param name="objects">The array of objects</param>
        /// <returns>The array of strings</returns>
        public static string[] CastObjsToStr(object[] objects)
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is DateTime)
                {
                    strings.Add(((DateTime)objects[i]).ToString());
                } else if (objects[i] is int)
                {
                    strings.Add(((int)objects[i]).ToString());
                } else
                {
                    strings.Add((string)objects[i]);
                }
            }
            return strings.ToArray();
        }

        /// <summary>
        /// Builds an array of characters based off of a set of bools.
        /// </summary>
        /// <param name="upper">If it should contain upper case characters</param>
        /// <param name="lower">If it should contain lower case characters</param>
        /// <param name="numerical">If it should contain numerical characters</param>
        /// <param name="special">If it should contain special characters</param>
        /// <returns></returns>
        public static char[] BuildCharArray(bool upper = true, bool lower = true, bool numerical = true, bool special = false)
        {
            List<char> Characters = new List<char>();
            if (upper) Characters.AddRange(ASCIIUpper);
            if (lower) Characters.AddRange(ASCIILower);
            if (numerical) Characters.AddRange(ASCIINumerical);
            if (special) Characters.AddRange(ASCIISpecial);
            return Characters.ToArray();
        }

        /// <summary>
        /// Removes duplicates from an array of characters.
        /// </summary>
        /// <param name="characters">The array of characters</param>
        /// <returns>The duplicate-free array of characters</returns>
        public static char[] RemoveDuplicates(char[] characters)
        {
            List<char> Characters = new List<char>();
            for (int i = 0; i < characters.Length; i++)
            {
                if (!Characters.Contains(characters[i])) Characters.Add(characters[i]);
            }
            return Characters.ToArray();
        }
    }

    public static class PublicUtils
    {
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^a - zA - Z0 - 9\s]", "");
        }

        public static string RemoveUpperCase(string str)
        {
            return Regex.Replace(str, @"[A-Z]", "");
        }

        public static string RemoveLowerCase(string str)
        {
            return Regex.Replace(str, @"[a-z]", "");
        }

        public static string RemoveNumbericalCharacters(string str)
        {
            return Regex.Replace(str, @"\d", "");
        }

        public static string RemoveSpaces(string str)
        {
            return str.Replace(" ", "");
        }
    }
}
