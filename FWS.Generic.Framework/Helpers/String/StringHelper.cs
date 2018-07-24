using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FWS.Generic.Framework.Helpers.String
{
    public static class StringHelper
    {
        public static bool EqualIgnoreCase(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        public static bool StartsWithIgnoreCase(this string str, string strToFind)
        {
            return str.StartsWith(strToFind, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// If a string contains numeric characters only ignoring whitespace optionally
        /// </summary>
        public static bool IsNumericOnly(this string str, bool ignoreWhitespace = false)
        {
            if (ignoreWhitespace)
                str = RemoveWhitespace(str);
            
            int n;
            return int.TryParse(str, out n);
        }

        /// <summary>
        /// If a string contains numeric characters only ignoring whitespace optionally
        /// </summary>
        public static string RemoveWhitespace(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        /// <summary>
        /// Takes a string and removes all characters that are not alphanumeric
        /// </summary>
        public static string RemoveSpecialCharactersKeepSpace(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9\\s]", "", RegexOptions.Compiled);
        }
        /// <summary>
        /// Takes a string and removes all characters that are not alphanumeric
        /// </summary>
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        /// <summary>
        /// If a string contains numeric characters only ignoring whitespace optionally
        /// </summary>
        public static bool ContainsUppercase(this string str)
        {
            return str.Any(char.IsUpper);
        }

        /// <summary>
        /// If a string contains numeric characters only ignoring whitespace optionally
        /// </summary>
        public static bool ContainsSpace(this string str)
        {
            return str.Any(c => c == ' ');
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static bool IsEmailFormat(this string emailString)
        {
            return Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static string OrEmpty(this string s)
        {
            return s ?? "";
        }
    }
}