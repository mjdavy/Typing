using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Typing.Model;

namespace Typing.Tests
{
    /// <summary>
    ///This is a test class for TypingStatsTest and is intended
    ///to contain all TypingStatsTest Unit Tests
    ///</summary>
    [TestClass]
    public class TypingStatsTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        /// <summary>
        ///A test for TypingStats Constructor
        ///</summary>
        [TestMethod]
        public void TypingStatsConstructorTest()
        {
            var target = new TypingStats_Accessor();
            Assert.IsNotNull(target);
            Assert.IsNotNull(target.stopWatch);
        }

        /// <summary>
        ///A test for GetAccuracy
        ///</summary>
        [TestMethod]
        public void GetAcuracyTest1()
        {
            var target = new TypingStats_Accessor {ErrorCount = 50, KeyCount = 100};
            const int expected = 50;
            int actual = target.GetAccuracy();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAccuracy
        ///</summary>
        [TestMethod]
        public void GetAcuracyTest2()
        {
            var target = new TypingStats_Accessor {ErrorCount = 0, KeyCount = 12};
            const int expected = 100;
            int actual = target.GetAccuracy();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAccuracy
        ///</summary>
        [TestMethod]
        public void GetAcuracyTest3()
        {
            var target = new TypingStats_Accessor {ErrorCount = 2, KeyCount = 99};
            const int expected = 97;
            int actual = target.GetAccuracy();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetWpm
        ///</summary>
        [TestMethod]
        public void GetAverageWpmTest()
        {
            var target = new TypingStats_Accessor();

            // Simulate 5 words per second == 300 wpm

            target.Start();
            target.KeyCount = 25;
            Thread.Sleep(1000);
            target.Stop();

            const int expected = 300;
            int actual = target.GetAverageWpm();
            Assert.IsTrue((expected - actual) < 5);
        }

        /// <summary>
        ///A test for Start
        ///</summary>
        [TestMethod]
        public void StartTest()
        {
            var target = new TypingStats_Accessor();
            target.Start();
            Assert.AreEqual(0, target.KeyCount);
            Assert.AreEqual(0, target.ErrorCount);
            Assert.AreEqual(100, target.GetAccuracy());
            Assert.AreEqual(0, target.GetAverageWpm());
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod]
        public void StopTest()
        {
            var target = new TypingStats_Accessor();
            target.Start();
            target.Stop();
            const bool expected = false;
            bool actual = target.stopWatch.IsRunning;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ErrorCount
        ///</summary>
        [TestMethod]
        public void ErrorCountTest()
        {
            var target = new TypingStats();
            const int expected = 100;
            target.ErrorCount = expected;
            int actual = target.ErrorCount;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for KeyCount
        ///</summary>
        [TestMethod]
        public void KeyCountTest()
        {
            var target = new TypingStats();
            const int expected = 100;
            target.KeyCount = expected;
            int actual = target.KeyCount;
            Assert.AreEqual(expected, actual);
        }
    }
}