using Typing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Typing.Tests
{
    
    
    /// <summary>
    ///This is a test class for StatsEventArgsTest and is intended
    ///to contain all StatsEventArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatsEventArgsTest
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
        ///A test for StatsEventArgs Constructor
        ///</summary>
        [TestMethod()]
        public void StatsEventArgsConstructorTest()
        {
            const int wpm = 40; 
            const int errors = 11; 
            const int accuracy = 80; 
            var target = new StatsEventArgs(wpm, errors, accuracy);
            Assert.AreEqual(wpm, target.WordsPerMinute);
            Assert.AreEqual(errors, target.Errors);
            Assert.AreEqual(accuracy, target.Accuracy);
        }
    }
}
