using Typing.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Typing.Model;

namespace Typing.Tests
{
    
    
    /// <summary>
    ///This is a test class for TextStreamViewModelTest and is intended
    ///to contain all TextStreamViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TextStreamViewModelTest
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
        ///A test for ProcessInput
        ///</summary>
        [TestMethod()]
        public void ProcessInputTest()
        {
            var textStream = new TextStreamModel();
            var keyboardViewModel = new KeyboardViewModel(textStream);
            var target = new TextStreamViewModel(textStream) {Text = "BEIRUT"};
            const string inputText = "B"; 
            target.ProcessInput(inputText);
            Assert.AreEqual(1, target.CharacterIndex);
        }

        /// <summary>
        ///A test for TextStreamViewModel Constructor
        ///</summary>
        [TestMethod()]
        public void TextStreamViewModelConstructorTest()
        {
            var textStream = new TextStreamModel();
            var keyboardViewModel = new KeyboardViewModel(textStream);
            var target = new TextStreamViewModel(textStream);
            Assert.AreNotEqual(null, target);
        }

        /// <summary>
        ///A test for UpdateText
        ///</summary>
        [TestMethod()]
        public void UpdateTextTest()
        {
            var textStream = new TextStreamModel();
            var keyboardViewModel = new KeyboardViewModel(textStream);
            var target = new TextStreamViewModel(textStream);
            //Uri source = null; // TODO: Initialize to an appropriate value
            //target.UpdateText(source);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CharacterIndex
        ///</summary>
        [TestMethod()]
        public void CharacterIndexTest()
        {
            var textStream = new TextStreamModel();
            var keyboardViewModel = new KeyboardViewModel(textStream);
            var target = new TextStreamViewModel(textStream);
            const int expected = 6;
            target.CharacterIndex = expected;
            int actual = target.CharacterIndex;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest()
        {
            var textStream = new TextStreamModel();
            var keyboardViewModel = new KeyboardViewModel(textStream);
            var target = new TextStreamViewModel(textStream);
            const string expected = "ABC";
            target.Text = expected;
            string actual = target.Text;
            Assert.AreEqual(expected, actual);
        }
    }
}
