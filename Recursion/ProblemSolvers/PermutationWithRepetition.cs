namespace ProblemSolvers
{
    using System.Collections.Generic;

    public class PermutationsWithRepetition<T> : CombinatoricsGen<T>
    {
        public PermutationsWithRepetition(int participants, IList<T> collection) : base(participants, collection)
        {
        }

        protected override void Generate(int index)
        {
            if (this.CurrentCount == this.participants)
            {
                this.result.Add(new List<T>(this.workList));
                return;
            }

            for (int i = 0; i < this.collection.Count; i++)
            {
                this.workList.Add(this.collection[i]);
                this.Generate(i);
                this.workList.RemoveAt(this.workList.Count - 1);
            }
        }
    }
}