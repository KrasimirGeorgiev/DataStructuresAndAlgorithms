namespace SortingHomework.Tests.Sorting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sorters;

    [TestClass]
    public class QuickSorterTests : SortingsTestsBase<double>
    {
        [TestInitialize]
        public override void TestInitialization()
        {
            base.Collection = new[] { 21312.12, -223, 1214, 23423.4, 123, 0, 100 };
            base.sorty = new Quicksorter<double>();
        }
    }
}