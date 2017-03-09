namespace T4_Palinromize
{
    using System;
    using System.Text;

    class Program
    {
        static void Main()
        {
            string word = Console.ReadLine();

            var sb = new StringBuilder(word);
            int index = 0;

            while (!IsPalindrome(sb.ToString()))
            {
                sb.Insert(sb.Length - index, word[index]);
                index++;
            }

            Console.WriteLine(sb.ToString());
        }

        private static bool IsPalindrome(string word)
        {
            int i = 0,
                j = word.Length - 1;

            while (i < j)
            {
                if (word[i] != word[j])
                {
                    return false;
                }

                i++;
                j--;
            }

            return true;
        }
    }
}