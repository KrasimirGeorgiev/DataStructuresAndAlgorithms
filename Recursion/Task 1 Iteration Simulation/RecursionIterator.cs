namespace IterationSimulation
{
    using System;
    using System.Collections.Generic;

    public class RecursionIterator
    {
        public static ICollection<RecursionIterator> IteratorsInstances = new List<RecursionIterator>();

        public RecursionIterator(int iterationsCount)
        {
            this.IterationsLimit = iterationsCount;
            this.CurrentPosition = 1;

            IteratorsInstances.Add(this);
        }

        public int CurrentPosition { get; private set; }

        public int IterationsLimit { get; }

        public void Iterate(Action<int> callback)
        {
            callback(this.CurrentPosition);


            if (this.CurrentPosition == this.IterationsLimit)
            {
                return;
            }

            this.CurrentPosition++;


            this.Iterate(callback);
        }
    }
}
