namespace T3_Correct
{
    class Program
    {
        static void Main()
        {
        }
    }

    public class Hash
    {
        private const ulong Base1 = 31;
        private const ulong Mod1 = 1000000009;

        private const ulong Base2 = 0;
        private const ulong Mod2 = 10037;

        private ulong power1;
        private ulong[] power2;

        public ulong Hash1 { get; set; }
        public ulong Hash2 { get; set; }

        public Hash(string str)
        {
            this.Hash1 = 0;
            this.Hash2 = 0;

            this.power1 = 1;
            this.power2 = new ulong[str.Length + 1];
            this.power2[0] = 1;

            for (int i = 0; i < str.Length; i++)
            {
                this.Add(str[i]);
                this.power1 = this.power1 * Base1 % Mod1;
                this.power2[i + 1] = this.power2[i] * Base2 % Mod2;
            }

        }

        public void Add(char c)
        {
            this.Hash1 *= Base1;
            this.Hash2 *= Base2;

            if (char.IsUpper(c))
            {
                this.Hash1 += (ulong)(c - 'A' + 1);
                this.Hash2 += Mod2 - 1;
            }
            else
            {
                this.Hash1 += Mod1 - 1;
                this.Hash2 += Mod2 - 2;
            }
        }
    }
}
