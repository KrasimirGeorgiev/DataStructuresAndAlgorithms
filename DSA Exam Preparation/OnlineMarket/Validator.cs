namespace OnlineMarket
{
    using System;

    public class Validator
    {
        public static string ValidateString(string str, int minLength, int maxLength)
        {
            if (minLength <= str.Length && str.Length <= maxLength)
            {
                return str;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}