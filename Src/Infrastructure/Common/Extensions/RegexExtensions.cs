using System.Text.RegularExpressions;

namespace Prototype.Infrastructure.Common.Extensions
{

    /*Remover los espacios en blanco, de manera eficiente*/
    public static class RegexExtensions
    {
        private static readonly Regex Whitespace = new(@"\s+");

        public static string ReplaceWhitespace(this string input, string replacement)
        {
            return Whitespace.Replace(input, replacement);
        }
    }
}
