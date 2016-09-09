using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Typing.Model;
using Typing.ViewModel;

namespace Typing.Tests
{
    /// <summary>
    ///This is a test class for KeyboardViewModelTest and is intended
    ///to contain all KeyboardViewModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class KeyboardViewModelTest
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
        ///A test for KeyboardViewModel Constructor
        ///</summary>
        [TestMethod()]
        public void KeyboardViewModelConstructorTest()
        {
            var textStream = new TextStreamModel();
            var target = new KeyboardViewModel(textStream);
            Assert.AreNotEqual(null, target.KeyboardRows);
            Assert.AreEqual(target.KeyboardRows.Count, 5);
        }

        /// <summary>
        ///A test for BuildKeyMap
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Typing.exe")]
        public void BuildKeyMapTest()
        {
            var textStream = new TextStreamModel();
            var target = new KeyboardViewModel_Accessor(textStream); 
            target.BuildKeyMap();
            Assert.AreNotEqual(null, target.charMap);
            Assert.AreNotEqual(null, target.keyMap);

            IList<Key> testPercent = target.charMap["%"];
            Assert.AreEqual(2, testPercent.Count);
            Assert.AreEqual(testPercent[0], Key.RightShift);
            Assert.AreEqual(testPercent[1], Key.D5);

            IList<Key> testHat = target.charMap["^"];
            Assert.AreEqual(2, testHat.Count);
            Assert.AreEqual(Key.LeftShift, testHat[0]);
            Assert.AreEqual(Key.D6, testHat[1]);

            IList<Key> testT = target.charMap["T"];
            Assert.AreEqual(2, testT.Count);
            Assert.AreEqual(Key.RightShift, testT[0]);
            Assert.AreEqual(Key.T, testT[1]);

            IList<Key> testY = target.charMap["Y"];
            Assert.AreEqual(2, testY.Count);
            Assert.AreEqual(Key.LeftShift, testY[0]);
            Assert.AreEqual(Key.Y, testY[1]);

            IList<Key> testG = target.charMap["G"];
            Assert.AreEqual(2, testG.Count);
            Assert.AreEqual(Key.RightShift, testG[0]);
            Assert.AreEqual(Key.G, testG[1]);

            IList<Key> testH = target.charMap["H"];
            Assert.AreEqual(2, testH.Count);
            Assert.AreEqual(Key.LeftShift, testH[0]);
            Assert.AreEqual(Key.H, testH[1]);

            IList<Key> testB = target.charMap["B"];
            Assert.AreEqual(2, testB.Count);
            Assert.AreEqual(Key.RightShift, testB[0]);
            Assert.AreEqual(Key.B, testB[1]);

            IList<Key> testN = target.charMap["N"];
            Assert.AreEqual(2, testN.Count);
            Assert.AreEqual(Key.LeftShift, testN[0]);
            Assert.AreEqual(Key.N, testN[1]);

        }

        /// <summary>
        ///A test for LoadKeyboard
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Typing.exe")]
        public void LoadKeyboardTest()
        {
            var textStream = new TextStreamModel();
            var target = new KeyboardViewModel_Accessor(textStream); 
            target.LoadKeyboard();
            int rowcount = target.KeyboardRows.Count;
            Assert.AreEqual(5, rowcount);
            Assert.AreEqual(14, target.KeyboardRows[0].Keys.Count);
            Assert.AreEqual(14, target.KeyboardRows[1].Keys.Count);
            Assert.AreEqual(13, target.KeyboardRows[2].Keys.Count);
            Assert.AreEqual(12, target.KeyboardRows[3].Keys.Count);
            Assert.AreEqual(5, target.KeyboardRows[4].Keys.Count);
        }

        /// <summary>
        ///A test for KeyboardRows
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Typing.exe")]
        public void KeyboardRowsTest()
        {
            var textStream = new TextStreamModel();
            var target = new KeyboardViewModel_Accessor(textStream); 
            var expected = target.KeyboardRows;
            target.KeyboardRows = expected;
            var actual = target.KeyboardRows;
            Assert.AreEqual(expected, actual);
        }
    }
}
