namespace Reglang
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ReglangCode = System.Collections.Generic.IEnumerable<(string Pattern, string Replacement)>;

    public static class Program
    {
        private const string UnaryMaths = @"
(?!):Unary Operators
$^:111+111*111^111-111^(111*111)

(1+)\^(1+)1:$1*$1^$2
(1+)\*(1+)1:$1+$1*$2
(1*)1-(1+)1:$1-$2
(1+)\+(1+):$1$2

(1+)\^1:$1
(1+)\*1:$1
(1*)1-1:$1

\((.+?)\):$1
";

        public static void Main(string[] args)
        {
            var code = Parse(UnaryMaths);
            foreach (var line in code.Run(""))
            {
                Console.WriteLine(line);
                Console.WriteLine();
            }
        }

        public static ReglangCode Parse(string text)
        {
            text = text.Replace("\r\n", "\n");
            bool escaped = false;
            bool newline = true;
            int lineStart = 0;
            int colonPosition = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                if (currentChar == ':' && !escaped)
                {
                    colonPosition = i;
                }
                if (currentChar == '\n')
                {
                    if (!newline) yield return (text[lineStart..(colonPosition)], text[(colonPosition + 1)..i]);
                    lineStart = i+1;
                }

                newline = currentChar == '\n';
                escaped = currentChar == '\\';
            }
            if (!newline) yield return (text[lineStart..(colonPosition - 1)], text[(colonPosition + 1)..text.Length]);
        }

        public static IEnumerable<string> Run(this ReglangCode relangCode, string start)
        {
            var code = relangCode.ToArray();

            string state = start;
            yield return state;

            for (int i = 0; i < code.Length; i++)
            {
                var (Pattern, Replacement) = code[i];

                var newState = Regex.Replace(state, Pattern, Replacement);

                if (state != newState)
                {
                    i = 0;
                    state = newState;
                    yield return state;
                }
            }
        }
    }
}