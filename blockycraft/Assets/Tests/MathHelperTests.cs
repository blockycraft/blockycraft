using System.Collections.Generic;
using Blockycraft.Scripts;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    public sealed class MathHelperTests
    {
        public static IEnumerable<TestCaseData> WrapIterativeDataProvider()
        {
            var size = 8;
            var levels = 2;
            var iterator = new Iterator3D(size);
            for (int level = 0; level < levels; level++)
                foreach (var item in iterator)
                {
                    yield return new TestCaseData(
                        size,
                        item.x + level * size,
                        item.y + level * size,
                        item.z + level * size,
                        item.x,
                        item.y,
                        item.z
                    );

                    yield return new TestCaseData(
                        size,
                        item.x - level * size,
                        item.y - level * size,
                        item.z - level * size,
                        item.x,
                        item.y,
                        item.z
                    );
                }
        }

        [Test, TestCaseSource("WrapIterativeDataProvider")]
        public void WrapIterative(int size, int x, int y, int z, int expectedX, int expectedY, int expectedZ)
        {
            var actual = MathHelper.Wrap(x, y, z, size);
            Assert.AreEqual(expectedX, actual.x);
            Assert.AreEqual(expectedY, actual.y);
            Assert.AreEqual(expectedZ, actual.z);
        }

        [TestCase(8, 1, 2, 3, 1, 2, 3)]
        [TestCase(8, 9, 10, 11, 1, 2, 3)]
        [TestCase(8, -1, -2, -3, 7, 6, 5)]
        [TestCase(8, -9, -10, -11, 7, 6, 5)]
        [TestCase(8, 8, 0, 0, 0, 0, 0)]
        [TestCase(8, 0, 8, 0, 0, 0, 0)]
        [TestCase(8, 0, 0, 8, 0, 0, 0)]
        [TestCase(8, 0, 0, 0, 0, 0, 0)]
        [TestCase(8, -8, 0, 0, 0, 0, 0)]
        [TestCase(8, 0, -8, 0, 0, 0, 0)]
        [TestCase(8, 0, 0, -8, 0, 0, 0)]
        public void Wrap(int size, int x, int y, int z, int expectedX, int expectedY, int expectedZ)
        {
            var actual = MathHelper.Wrap(x, y, z, size);
            Assert.AreEqual(expectedX, actual.x);
            Assert.AreEqual(expectedY, actual.y);
            Assert.AreEqual(expectedZ, actual.z);
        }

        public static IEnumerable<TestCaseData> AnchorIterativeDataProvider()
        {
            var size = 8;
            var levels = 2;
            var iterator = new Iterator3D(size);
            for (int level = 0; level < levels; level++)
                foreach (var item in iterator)
                {
                    yield return new TestCaseData(
                        size,
                        level * size + item.x,
                        level * size + item.y,
                        level * size + item.z,
                        level,
                        level,
                        level
                    );

                    yield return new TestCaseData(
                        size,
                        -level * size + item.x,
                        -level * size + item.y,
                        -level * size + item.z,
                        -level,
                        -level,
                        -level
                    );
                }
        }

        [Test, TestCaseSource("AnchorIterativeDataProvider")]
        public void AnchorIterative(int size, int x, int y, int z, int expectedX, int expectedY, int expectedZ)
        {
            var actual = MathHelper.Anchor(x, y, z, size);
            Assert.AreEqual(expectedX, actual.x);
            Assert.AreEqual(expectedY, actual.y);
            Assert.AreEqual(expectedZ, actual.z);
        }


        [TestCase(8, 0, 0, 0, 0, 0, 0)]
        [TestCase(8, 1, 1, 1, 0, 0, 0)]
        [TestCase(8, -1, 0, 0, -1, 0, 0)]
        [TestCase(8, 0, -1, 0, 0, -1, 0)]
        [TestCase(8, 0, 0, -1, 0, 0, -1)]
        [TestCase(8, -8, 0, 0, -1, 0, 0)]
        [TestCase(8, 0, -8, 0, 0, -1, 0)]
        [TestCase(8, 0, 0, -8, 0, 0, -1)]
        [TestCase(8, -16, 0, 0, -2, 0, 0)]
        [TestCase(8, 0, -16, 0, 0, -2, 0)]
        [TestCase(8, 0, 0, -16, 0, 0, -2)]
        [TestCase(8, -17, 0, 0, -3, 0, 0)]
        [TestCase(8, 0, -17, 0, 0, -3, 0)]
        [TestCase(8, 0, 0, -17, 0, 0, -3)]
        public void Anchor(int size, int x, int y, int z, int expectedX, int expectedY, int expectedZ)
        {
            var actual = MathHelper.Anchor(x, y, z, size);
            Assert.AreEqual(expectedX, actual.x);
            Assert.AreEqual(expectedY, actual.y);
            Assert.AreEqual(expectedZ, actual.z);
        }
    }
}
