namespace ProblemSolvers
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class CombinatoricsGen<T>
    {
        protected int participants;

        protected int total;

        protected IList<T> collection;

        protected IList<IList<T>> result;

        protected IList<T> workList;

        protected CombinatoricsGen(int participants, IList<T> collection)
        {
            this.participants = participants;
            this.collection = collection;
            this.total = collection.Count();

            this.workList = new List<T>(this.participants);
        }

        public virtual IList<IList<T>> Result
        {
            get
            {
                if (this.result == null)
                {
                    this.result = new List<IList<T>>();
                    this.Generate(0);
                }

                return this.result;
            }
        }

        protected int CurrentCount => this.workList.Count;

        protected abstract void Generate(int index);
    }
}