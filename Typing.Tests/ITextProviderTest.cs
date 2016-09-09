using Typing.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Typing.Tests
{
    /// <summary>
    ///This is a test class for ITextProviderTest and is intended
    ///to contain all ITextProviderTest Unit Tests
    ///</summary>
    [TestClass]
    public sealed class TextProviderTest
    {
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

        private ITextProvider CreateITextProvider()
        {
            var meftestHelper = new MefTestHelper();
            meftestHelper.Compose();
            return meftestHelper.TextProviders;
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        [TestMethod]
        public void DescriptionTest()
        {
            ITextProvider target = CreateITextProvider(); 
            const string expected = "The Description";
            target.Description = expected;
            string actual = target.Description;
            Assert.AreEqual(expected, actual);
        }
    }
}
