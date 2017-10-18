using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnakeBeauty.Tests
{
    [TestClass()]
    public class DirectionTests
    {
        Direction dir1 = new Direction(1);
        Direction dir2 = new Direction(1);
        Direction dir3 = new Direction(2);

        
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(dir1.Equals(dir1));
            Assert.IsTrue(dir2.Equals(dir1));
            Assert.IsTrue(dir1.Equals(dir1));
            Assert.IsTrue(dir2.Equals(dir2));
            Assert.IsFalse(dir1.Equals(dir3));
        }

    }
}