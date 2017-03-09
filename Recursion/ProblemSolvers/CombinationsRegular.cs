namespace ProblemSolvers
{
    using System.Collections.Generic;

    public class CombinatoricsesRegular<T> : CombinatoricsGen<T>
    {
        public CombinatoricsesRegular(int participants, IList<T> collection) : base(participants, collection)
        {
        }

        protected override void Generate(int index)
        {
            if (this.CurrentCount == this.participants)
            {
                this.result.Add(new List<T>(this.workList));
                return;
            }

            for (int i = index; i < this.collection.Count; i++)
            {
                this.workList.Add(this.collection[i]);
                this.Generate(i + 1);
                this.workList.RemoveAt(this.workList.Count - 1);
            }
        }
    }
}