namespace SortingHomework.Tests.Sorting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sorters;

    [TestClass]
    public class SelectionSorterTests : SortingsTestsBase<int>
    {
        [TestInitialize]
        public override void TestInitialization()
        {
            base.sorty = new SelectionSorter<int>();
            base.Collection = new[] { 56, 45, 345, 23, 3466, 23, -54, 67, -213, 1, 1, 56, 0 };
        }
    }
}