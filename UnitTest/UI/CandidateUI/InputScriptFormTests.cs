using DBI202_Creator.UI.CandidateUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest.UI.CandidateUI
{
    [TestClass]
    public class InputScriptFormTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private InputScriptForm CreateInputScriptForm()
        {
            return new InputScriptForm();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateInputScriptForm();

            // Act
            unitUnderTest.
            // Assert
            Assert.Fail();
        }
    }
}
