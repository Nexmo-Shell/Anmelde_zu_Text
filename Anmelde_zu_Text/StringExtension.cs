using System;
using System.Collections.Generic;
using System.Text;

namespace Anmelde_zu_Text
{
    public static class StringExtension
    {
        public static string Reverse(this string s)
        {
            char[] c = s.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }
    }
}
