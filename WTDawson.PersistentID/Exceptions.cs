using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTDawson.PersistentID
{
    public static class Exceptions
    {
        /// <summary>
        /// An invalid item exception.
        /// </summary>
        public class InvalidItemException : Exception
        {
            public override string Message => "Invalid item exception";
        }
    }
}
