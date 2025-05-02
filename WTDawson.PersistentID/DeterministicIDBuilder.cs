using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTDawson.PersistentID
{
    /// <summary>
    /// Instead of using the Random algorithm, it uses my algorithm for generating unique IDs
    /// This is like the 2nd version of WTDawson.PersistentID
    /// </summary>
    public class DeterministicIDBuilder : IDisposable
    {
        internal static class Algorithm
        {
            /// <summary>
            /// Generates the number using the algorithm.
            /// </summary>
            /// <param name="iteration">The iteration</param>
            /// <param name="magic">The "magic"</param>
            /// <param name="numChars">The total number of available characters</param>
            /// <param name="charsTotal">All of the available characters casted to an integer and then added together</param>
            /// <returns>The number</returns>
            public static int GenerateNumber(int iteration, int magic, int numChars, int charsTotal)
            {
                long prime = Math.Abs(magic * (numChars + charsTotal)) + 1;
                while (!Utils.IsPrime(prime)) prime += 2;
                return (int)(((iteration * prime + magic) ^ (iteration + magic * prime)) % numChars);
            }
        }

        public enum Version
        {
            V1 = 0
        }

        // Internal
        internal List<object> Objects;
        internal string ID = string.Empty;
        internal int Magic = 0;
        // Public
        public int Length { get { return ID.Length; } }
        public int Count { get { return Objects.Count; } }
        public object[] Items { get { return Objects.ToArray(); } }
        public Version AlgorithmVersion = Version.V1;
        // Private
        private bool Upper, Lower, Numerical, Special;
        private int IDLength = 16; // 16-32

        // Constructors
        /// <summary>
        /// IDBuilder constructor.
        /// </summary>
        public DeterministicIDBuilder()
        {
            Objects = new List<object>();
            Build();
        }
        /// <summary>
        /// IDBuilder constructor with a list of items.
        /// </summary>
        /// <param name="Items">The list of items</param>
        public DeterministicIDBuilder(params object[] Items)
        {
            Objects = new List<object>();
            AddItems(Items);
            Build();
        }
        /// <summary>
        /// IDBuilder constructor that sets the magic.
        /// </summary>
        /// <param name="magic">The magic</param>
        public DeterministicIDBuilder(int magic = 0)
        {
            Objects = new List<object>();
            SetMagic(0);
            Build();
        }
        /// <summary>
        /// IDBuilder constructor that sets the magic and list of items.
        /// </summary>
        /// <param name="magic">The magic</param>
        /// <param name="Items">The list of items</param>
        public DeterministicIDBuilder(int magic = 0, params object[] Items)
        {
            Objects = new List<object>();
            SetMagic(magic);
            AddItems(Items);
            Build();
        }
        /// <summary>
        /// IDBuilder constructor that sets the magic and length.
        /// </summary>
        /// <param name="magic">The magic</param>
        /// <param name="length">The length of the ID</param>
        public DeterministicIDBuilder(int magic = 0, int length = 16)
        {
            Objects = new List<object>();
            SetMagic(0);
            SetLength(length);
            Build();
        }
        /// <summary>
        /// IDBuilder constructor that sets the magic, length, and list of items.
        /// </summary>
        /// <param name="magic">The magic</param>
        /// <param name="length">The length of the ID</param>
        /// <param name="Items">The list of items</param>
        public DeterministicIDBuilder(int magic = 0, int length = 16, params object[] Items)
        {
            Objects = new List<object>();
            SetMagic(magic);
            SetLength(length);
            AddItems(Items);
            Build();
        }

        /// <summary>
        /// Sets the rules (This does not apply to the items added (Use PublicUtils to remove them instead).
        /// </summary>
        /// <param name="upper">If it should contain upper case characters</param>
        /// <param name="lower">If it should contain lower case characters</param>
        /// <param name="numerical">If it should contain numerical characters</param>
        /// <param name="special">If it should contain special characters</param>
        public void SetRules(bool upper = true, bool lower = true, bool numerical = true, bool special = false)
        {
            Upper = upper;
            Lower = lower;
            Numerical = numerical;
            Special = special;
        }

        /// <summary>
        /// Sets the magic of the PersistentID
        /// This is the "key" to the ID, you can set this to something like the number of users at the time when the user was created.
        /// If this and the items are not the same, it will generate a different ID.
        /// </summary>
        /// <param name="magic">The magic</param>
        public void SetMagic(int magic = 0)
        {
            Magic = magic;
        }

        /// <summary>
        /// The length of the ID
        /// </summary>
        /// <param name="length">The length (Recommended: 16-32, Default: 16)</param>
        public void SetLength(int length = 16)
        {
            IDLength = length;
        }

        /// <summary>
        /// Dispose the current IDBuilder instance.
        /// </summary>
        public void Dispose()
        {
            Objects.Clear();
            GC.Collect();
        }
        /// <summary>
        /// IDBuilder deconstructor.
        /// </summary>
        ~DeterministicIDBuilder()
        {
            Dispose();
        }

        /// <summary>
        /// Add items to the list to be used when generating the ID.
        /// </summary>
        /// <param name="Items">A list of items to be added (Has to be a string, integer or DateTime object)</param>
        public void AddItems(params object[] Items)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is not string && Items[i] is not int && Items[i] is not DateTime)
                {
                    throw new Exceptions.InvalidItemException();
                }

                Objects.Add(Items[i]);
            }
        }

        /// <summary>
        /// Gets a specific character of the ID
        /// </summary>
        /// <param name="index">The ID of the character</param>
        /// <returns></returns>
        public char this[int index]
        {
            get { Build(); return ID[index]; }
        }

        /// <summary>
        /// Converts the IDBuilder instance to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Build();
            return ID;
        }

        /// <summary>
        /// Builds the PersistentID.
        /// </summary>
        internal void Build()
        {
            List<char> chars = new List<char>();
            chars.AddRange(string.Join("", Utils.CastObjsToStr(Objects.ToArray())).ToCharArray());

            int charCount = chars.Count;

            chars.AddRange(Utils.BuildCharArray(Upper, Lower, Numerical, Special));
            //chars = Utils.RemoveDuplicates(chars.ToArray()).ToList(); // Probably isn't needed

            int charsTotal = 0;
            for (int i = 0; i < chars.Count; i++)
            {
                charsTotal += (int)chars[i];
            }

            if (chars.Count == 0)
            {
                return;
            }

            string id = "";

            for (int i = 0; i < IDLength; i++)
            {
                id += chars[Algorithm.GenerateNumber(i, Magic > 1000 ? Magic : Magic * 1000, chars.Count, charsTotal * charCount)];
            }

            ID = id;
        }
    }
}
