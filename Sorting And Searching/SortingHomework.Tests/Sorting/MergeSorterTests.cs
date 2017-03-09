namespace SortingHomework.Tests.Sorting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sorters;

    [TestClass]
    public class MergeSorterTests : SortingsTestsBase<string>
    {
        [TestInitialize]
        public override void TestInitialization()
        {
            base.Collection = new[] { "D", "D", "K", "C", "b", "a", "1", "baz", "h", "z", "R" };
            base.sorty = new MergeSorter<string>();
        }
    }
}