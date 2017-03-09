namespace T3_Source
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Program
    {
        private static void Main()
        {
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();

            Regex regexPattern = ExtractRegexPattern(pattern);

            int occurrence = 0;

            string displacements = CheckText(regexPattern, pattern, text, out occurrence);

            if (occurrence >= 0)
            {
                Console.WriteLine(occurrence);
                Console.WriteLine(displacements);
            }
        }

        private static string CheckText(
            Regex regexPattern, string originalPattern, string text, out int occurrence)
        {
            occurrence = 0;

            var displacement = new List<int>();
            var matches = regexPattern.Matches(text);

            foreach (Match match in matches)
            {
                if (CapitalsMatch(originalPattern, match.Value, regexPattern))
                {
                    if (OtherLettersMatch(originalPattern, match.Value))
                    {
                        displacement.Add(match.Index + 1);

                        occurrence++;
                    }
                }
            }

            return string.Join(" ", displacement);
        }

        private static bool OtherLettersMatch(string originalPattern, string substring)
        {
            for (int i = 0; i < originalPattern.Length; i++)
            {
                if (char.IsUpper(originalPattern[i]))
                {
                    continue;
                }

                if (!ConfirmOtherCharactersMatch(i, originalPattern, substring))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ConfirmOtherCharactersMatch(int characterIndex, string originalPattern, string substring)
        {
            char original = originalPattern[characterIndex];
            char substringChar = substring[characterIndex];

            int guideIndex = characterIndex;

            while (guideIndex != -1)
            {
                guideIndex++;

                int indexA = originalPattern.IndexOf(original, guideIndex);
                int indexB = substring.IndexOf(substringChar, guideIndex);

                if (indexA != indexB)
                {
                    return false;
                }

                guideIndex = indexA;
            }

            return true;
        }

        private static bool CapitalsMatch(string pattern, string substring, Regex regexPattern)
        {
            var substringMatch = regexPattern.Match(substring);
            var originalMatch = regexPattern.Match(pattern);

            for (int i = 1; i < substringMatch.Groups.Count; i++)
            {
                var subValue = substringMatch.Groups[i].Value;
                var expectedValue = originalMatch.Groups[i].Value;
                if (!subValue.Equals(expectedValue, StringComparison.Ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        private static Regex ExtractRegexPattern(string pattern)
        {
            string capitalLttersPatten = "([A-Z]{{{0}}})",
                otherThanCapitalPattern = "[^A-Z]{{{0}}}";
            var sb = new StringBuilder();

            int currentCount = 0,
                i = 0;

            while (i < pattern.Length)
            {
                while (i < pattern.Length && char.IsUpper(pattern[i]))
                {
                    i++;
                    currentCount++;
                }

                if (currentCount > 0)
                {
                    sb.AppendFormat(capitalLttersPatten, currentCount);
                    currentCount = 0;
                }

                while (i < pattern.Length && !char.IsUpper(pattern[i]))
                {
                    i++;
                    currentCount++;
                }

                if (currentCount > 0)
                {
                    sb.AppendFormat(otherThanCapitalPattern, currentCount);
                    currentCount = 0;
                }
            }

            var regex = new Regex(sb.ToString());

            return regex;
        }
    }
}