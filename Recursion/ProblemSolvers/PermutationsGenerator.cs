namespace ProblemSolvers
{
    using System.Collections.Generic;

    public class PermutationsGenerator<T> : CombinatoricsGen<T>
    {
        private readonly bool[] mask;

        public PermutationsGenerator(int subsetSize, IList<T> collection) : base(subsetSize, collection)
        {
            this.mask = new bool[collection.Count];
        }

        protected override void Generate(int index)
        {
            if (index == this.participants)
            {
                this.result.Add(new List<T>(this.workList));
                return;
            }

            for (int i = 0; i < this.collection.Count; i++)
            {
                if (!this.mask[i])
                {
                    this.mask[i] = true;
                    this.workList.Add(this.collection[i]);
                    this.Generate(index + 1);
                    this.workList.RemoveAt(this.workList.Count - 1);
                    this.mask[i] = false;
                }
            }
        }
    }
}